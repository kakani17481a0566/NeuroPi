using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.PronunciationAssessment;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Audio;
using System.Text.Json;


namespace SchoolManagement.Services.Implementation
{
    public class AudioTranscriptionServiceImpl : IAudioTranscriptionService
    {

        private readonly ApiKeyService apiKeyService;
        private readonly SchoolManagementDb schoolManagementDb;
        public AudioTranscriptionServiceImpl(ApiKeyService _apiKeyService,SchoolManagementDb schoolManagementDb)
        {
            apiKeyService = _apiKeyService;
            this.schoolManagementDb = schoolManagementDb;
        }
        public async Task<byte[]> TranscribeAudioAsync(byte[] audioBytes, string fileExtension, string text)
        {
            string _subscriptionKey = "7TZnnv4r6ijxdVYsHMrDkPYWxVev4XwJBVzuMGWsCXF8Y22SuFnUJQQJ99BGACYeBjFXJ3w3AAAYACOGMuI3";
        string _region = "eastus";

        var config = SpeechConfig.FromSubscription(_subscriptionKey, _region);
            string tempFilePath = Path.GetTempFileName() + fileExtension;
            await File.WriteAllBytesAsync(tempFilePath, audioBytes);

            string referenceText = text;
            var pronunciationConfig = new PronunciationAssessmentConfig(
                referenceText,
                GradingSystem.HundredMark,
                Granularity.Phoneme,
                enableMiscue: true);

            try
            {
                using (var audioInput = AudioConfig.FromWavFileInput(tempFilePath))
                using (var recognizer = new SpeechRecognizer(config, audioInput))
                {
                    pronunciationConfig.ApplyTo(recognizer);
                    var result = await recognizer.RecognizeOnceAsync();

                    if (result.Reason == ResultReason.RecognizedSpeech)
                    {
                        var pronunciationResult = PronunciationAssessmentResult.FromResult(result);

                        if (pronunciationResult.AccuracyScore >= 85.0)
                        {
                            // Return pronunciation confirmation as audio
                            var aiHelper = new AiPronunciationHelper(apiKeyService);
                            return await aiHelper.ConvertTextToSpeechAsync("Great job! You pronounced it correctly.");
                        }
                        else
                        {
                            var aiHelper = new AiPronunciationHelper(apiKeyService);
                            var textHelp = await aiHelper.GetPronunciationHelpFromGoogleAsync(referenceText);
                            return await aiHelper.ConvertTextToSpeechAsync(textHelp);
                        }
                    }
                    else
                    {
                        return await new AiPronunciationHelper(apiKeyService).ConvertTextToSpeechAsync("Sorry, we could not recognize your speech.");
                    }
                }
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                int retries = 3;
                while (retries-- > 0)
                {
                    try
                    {
                        if (File.Exists(tempFilePath))
                            File.Delete(tempFilePath);
                        break;
                    }
                    catch (IOException)
                    {
                        await Task.Delay(200);
                    }
                }
            }
        }
        public PronouncationResponseVM TranscribeAndCompareAsync(byte[] audioBytes, string fileExtension, string[] words)
        {
            return TranscribeAndCompareInternalAsync(audioBytes, fileExtension, words).GetAwaiter().GetResult();
        }

        private async Task<PronouncationResponseVM> TranscribeAndCompareInternalAsync(
     byte[] audioBytes,
     string fileExtension,
     string[] expectedWords,
     CancellationToken ct = default)
        {
            //string _subscriptionKey = apiKeyService.GetAzureApiKey();
            string _subscriptionKey = "7TZnnv4r6ijxdVYsHMrDkPYWxVev4XwJBVzuMGWsCXF8Y22SuFnUJQQJ99BGACYeBjFXJ3w3AAAYACOGMuI3";
            string _region = "eastus";
            var config = SpeechConfig.FromSubscription(_subscriptionKey, _region);
            string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + fileExtension);
            await File.WriteAllBytesAsync(tempFilePath, audioBytes, ct);

            var mispronouncedWords = new List<string>();
            var spokenWordSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var audioClips = new List<byte[]>();

            var referenceText = string.Join(" ", expectedWords);
            var pronunciationConfig = new PronunciationAssessmentConfig(
                referenceText,
                GradingSystem.HundredMark,
                Granularity.Word,
                enableMiscue: true);

            try
            {
                using var audioInput = AudioConfig.FromWavFileInput(tempFilePath);
                using var recognizer = new SpeechRecognizer(config, audioInput);
                pronunciationConfig.ApplyTo(recognizer);
                var result = await recognizer.RecognizeOnceAsync();

                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    var jsonResult = result.Properties.GetProperty(PropertyId.SpeechServiceResponse_JsonResult);
                    using var parsedResult = JsonDocument.Parse(jsonResult);

                    if (parsedResult.RootElement.TryGetProperty("NBest", out var nBest) &&
                        nBest.GetArrayLength() > 0 &&
                        nBest[0].TryGetProperty("Words", out var wordList))
                    {
                        foreach (var word in wordList.EnumerateArray())
                        {
                            if (word.TryGetProperty("Word", out var wordTextElement) &&
                                word.TryGetProperty("PronunciationAssessment", out var assessment) &&
                                assessment.TryGetProperty("AccuracyScore", out var accuracyScoreElement))
                            {
                                string spokenWord = wordTextElement.GetString() ?? string.Empty;
                                double accuracy = accuracyScoreElement.GetDouble();

                                if (!string.IsNullOrWhiteSpace(spokenWord))
                                    spokenWordSet.Add(spokenWord);

                                if (accuracy < 85.0 &&
                                    expectedWords.Contains(spokenWord, StringComparer.OrdinalIgnoreCase))
                                {
                                    mispronouncedWords.Add(spokenWord);
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                try { if (File.Exists(tempFilePath)) File.Delete(tempFilePath); } catch { }
            }

            // Identify missed words (not spoken at all)
            var missedWords = expectedWords
                .Where(w => !spokenWordSet.Contains(w))
                .ToList();

            var allProblemWords = mispronouncedWords
                .Concat(missedWords)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

       
            var aiHelper = new AiPronunciationHelper(apiKeyService);

            foreach (var word in allProblemWords)
            {
                string textToSpeak;

                if (missedWords.Contains(word, StringComparer.OrdinalIgnoreCase))
                {
                    // Missed word: just say the word
                    textToSpeak = $"The  missing word is {word}.";
                }
                else
                {
                    // Mispronounced word: ask for rhyme help
                    try
                    {
                        string rhymeLine = await aiHelper.GetKidFriendlyRhymesAsync(word, ct);
                        textToSpeak = $"{word}. {rhymeLine}";
                    }
                    catch
                    {
                        textToSpeak = $"The word is {word}.";
                    }
                }

                try
                {
                    var audio = await aiHelper.ConvertTextToSpeechAsync(textToSpeak, ct);
                    if (audio != null && audio.Length > 0)
                        audioClips.Add(audio);
                }
                catch
                {
                    // skip audio failure for this word
                }
            }

            // Combine all audio clips into one byte array
            byte[] finalAudio;
            if (audioClips.Count == 1)
            {
                finalAudio = audioClips[0];
            }
            else
            {
                // Combine MP3s — in production use an audio library to merge properly
                // Here: naive concat (some players may skip after first segment)
                finalAudio = audioClips.SelectMany(b => b).ToArray();
            }

            return new PronouncationResponseVM
            {
                misPronouncedWords = allProblemWords,
                rhymses = finalAudio
            };
        }

        
        public string CheckPronounciation(byte[] audioBytes,string text)
        {
            return CheckPronounciationAsync(audioBytes, text).GetAwaiter().GetResult();
        }

        public async  Task<string> CheckPronounciationAsync(byte[] audioBytes, string text)
        {
            //string fileExtension=Path.GetExtension(audioBytes)
            string _subscriptionKey = "7TZnnv4r6ijxdVYsHMrDkPYWxVev4XwJBVzuMGWsCXF8Y22SuFnUJQQJ99BGACYeBjFXJ3w3AAAYACOGMuI3";
            string _region = "eastus";
            var config = SpeechConfig.FromSubscription(_subscriptionKey, _region);
            string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") );
            await File.WriteAllBytesAsync(tempFilePath, audioBytes);
            //near rhymes pronounciation 
            //var pronunciationConfig = new PronunciationAssessmentConfig(
            //   text,
            //   GradingSystem.HundredMark,
            //   Granularity.Word,
            //   enableMiscue: true);
            //using (var audioInput = AudioConfig.FromWavFileInput(tempFilePath))
            //using (var recognizer = new SpeechRecognizer(config, audioInput))
            //{
            //    pronunciationConfig.ApplyTo(recognizer);
            //    var result = await recognizer.RecognizeOnceAsync();
            //    result.Properties.GetProperty(PropertyId.SpeechServiceResponse_JsonResult);
            //    double score=  PronunciationAssessmentResult.FromResult(result).AccuracyScore;
            //    if (score >= 90.0)
            //    {
            //        return " correct word match";

            //    }
            //    else
            //    {
            //        var aiHelper = new AiPronunciationHelper(apiKeyService);
            //        var textHelp = await aiHelper.GetKidFriendlyRhymesAsync(text);
            //        return textHelp;

            //    }

            //}
            var audioConfig=AudioConfig.FromWavFileOutput(tempFilePath);
            using (var recognizer = new SpeechRecognizer(config, audioConfig))
            {
                var result = await recognizer.RecognizeOnceAsync();
                if (result != null)
                {
                    string resultText = result.Text.ToLower();
                    string response = text.ToLower()+".";
                    bool res = response.Equals(resultText, StringComparison.OrdinalIgnoreCase);
                    
                    if(res)
                    {
                        return "correct word";

                    }
                    else
                    {

                        var aiHelper = new AiPronunciationHelper(apiKeyService);
                        var textHelp = await aiHelper.GetKidFriendlyRhymesAsync(text);
                        //Console.WriteLine(textHelp);
                        string[] rhymeWords = textHelp.Split(",");
                        var testResponse=schoolManagementDb.tests.Where(t=>!t.isDeleted && t.name.ToLower()==text.ToLower()).FirstOrDefault();
                        if (testResponse != null)
                        {
                            List<MTestContent> list=new List<MTestContent>();
                            foreach (string word in rhymeWords)
                            {
                                bool isWordPresent = schoolManagementDb.tests.Any(t => t.name.ToLower() == word.ToLower() && !t.isDeleted);
                                if (!isWordPresent)
                                {


                                    MTestContent model = new MTestContent()
                                    {
                                        name = word,
                                        testId = 1,
                                        relationId = testResponse.id,
                                        tenantId = testResponse.tenantId,
                                    };
                                    list.Add(model);
                                }
                                
                            }
                            schoolManagementDb.tests.AddRange(list);
                            schoolManagementDb.SaveChangesAsync();
                           

                        }

                        return textHelp;

                    }
                }
            }
            return "InCorrect word match ";
        }

        public string AddImage(IFormFile file, string text)
        {
            MImage mImage = new MImage()
            {
                url = ConvertIFormFileToBytes(file)
            };
            schoolManagementDb.images.Add(mImage);
            schoolManagementDb.SaveChanges();
            if (mImage != null)
            {
                return "inserted";
            }
            return "Not Inserted";
            
            
        }
        public List<ImageDb> GetImage()
        {
            var result = schoolManagementDb.images.ToList();
            if (result != null)
            {
                List<ImageDb> images = new List<ImageDb>();
                //foreach(var r in result)
                //{
                //    ImageDb image = new ImageDb();

                //    image.url = Convert.ToBase64String(r.url);
                //    image.name = "Apple";
                //    images.Add(image);
                //}
                return images;
            }
            return null;
        }

        public byte[] ConvertIFormFileToBytes(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null; 
            }

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }




}
