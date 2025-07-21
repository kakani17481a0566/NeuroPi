using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.PronunciationAssessment;
using OpenAI_API;
using SchoolManagement.Services.Interface;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SchoolManagement.Services.Implementation
{
    public class AudioTranscriptionServiceImpl : IAudioTranscriptionService
    {
        private readonly string _subscriptionKey = "7ZHQnjdH8f35SZypT2rYaoM1r7tfXWL2OoIafMUeqLzVFqDLTGE8JQQJ99BGACYeBjFXJ3w3AAAYACOGVCzT";
        private readonly string _region = "eastus";

        public async Task<byte[]> TranscribeAudioAsync(byte[] audioBytes, string fileExtension,string text)
        {
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
                            var aiHelper = new AiPronunciationHelper();
                            return await aiHelper.ConvertTextToSpeechAsync("Great job! You pronounced it correctly.");
                        }
                        else
                        {
                            var aiHelper = new AiPronunciationHelper();
                            var textHelp = await aiHelper.GetPronunciationHelpFromGoogleAsync(referenceText);
                            return await aiHelper.ConvertTextToSpeechAsync(textHelp);
                        }
                    }
                    else
                    {
                        return await new AiPronunciationHelper().ConvertTextToSpeechAsync("Sorry, we could not recognize your speech.");
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




        public class AiPronunciationHelper
    {
            private readonly string _googleApiKey = "";

            public async Task<string> GetPronunciationHelpFromGoogleAsync(string word)
            {
                string apiKey = "AIzaSyADRBX3vm2b-p2VRGkPeDd7ilViG3i6sD4"; // Replace with your actual Gemini API key
                string endpoint = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";

                using var client = new HttpClient();

                var requestBody = new
                {
                    contents = new[]
                    {
            new
            {
                parts = new[]
                {
                    new { text = $"Can you give me a short and simple sentence using the word\"{word}\" for kids?" }
                }
            }
        }
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(endpoint, content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new ApplicationException($"Gemini API error {response.StatusCode}: {responseString}");

                try
                {
                    using var doc = JsonDocument.Parse(responseString);
                    var root = doc.RootElement;

                    var candidates = root.GetProperty("candidates");
                    if (candidates.GetArrayLength() == 0)
                        return "No response candidates from Gemini.";

                    var parts = candidates[0]
                                    .GetProperty("content")
                                    .GetProperty("parts");

                    if (parts.GetArrayLength() == 0)
                        return "No response parts from Gemini.";

                    var text = parts[0].GetProperty("text").GetString();
                    return text?.Trim() ?? "No explanation returned.";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing Gemini response: {ex.Message}");
                    Console.WriteLine($"Raw response: {responseString}");
                    return "Unable to parse response from Gemini API.";
                }
            }


            public async Task<byte[]> ConvertTextToSpeechAsync(string text)
            {
                var speechConfig = SpeechConfig.FromSubscription("7ZHQnjdH8f35SZypT2rYaoM1r7tfXWL2OoIafMUeqLzVFqDLTGE8JQQJ99BGACYeBjFXJ3w3AAAYACOGVCzT", "eastus");
                speechConfig.SpeechSynthesisVoiceName = "en-US-JennyNeural"; // or any preferred voice
                speechConfig.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Audio16Khz32KBitRateMonoMp3);

                using var synthesizer = new SpeechSynthesizer(speechConfig, null);
                using var result = await synthesizer.SpeakTextAsync(text);

                if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                {
                    return result.AudioData;
                }
                else if (result.Reason == ResultReason.Canceled)
                {
                    var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                    throw new Exception($"Synthesis canceled: {cancellation.Reason}. Error: {cancellation.ErrorDetails}");
                }

                throw new Exception("Text-to-Speech synthesis failed.");
            }
        }

    }
}
