﻿using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.PronunciationAssessment;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Audio;
using System.Text.Json;


namespace SchoolManagement.Services.Implementation
{
    public class AudioTranscriptionServiceImpl : IAudioTranscriptionService
    {

        private readonly ApiKeyService apiKeyService;
        public AudioTranscriptionServiceImpl(ApiKeyService _apiKeyService)
        {
            apiKeyService = _apiKeyService;
        }


        //string azureApiKey= apiKeyService.getAzureApiKey();


            /// private readonly string _subscriptionKey = "7ZHQnjdH8f35SZypT2rYaoM1r7tfXWL2OoIafMUeqLzVFqDLTGE8JQQJ99BGACYeBjFXJ3w3AAAYACOGVCzT";




        public async Task<byte[]> TranscribeAudioAsync(byte[] audioBytes, string fileExtension, string text)
        {
        string _subscriptionKey = apiKeyService.GetAzureApiKey();
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
            string _subscriptionKey = apiKeyService.GetAzureApiKey();
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
    }




}
