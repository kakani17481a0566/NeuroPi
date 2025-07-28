using Microsoft.CognitiveServices.Speech;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Audio;
using System.Text;
using System.Text.Json;

namespace SchoolManagement.Services.Implementation
{

    public class AiPronunciationHelper
    {
        //private readonly string _googleApiKey = "";
        //private readonly string 

        private readonly ApiKeyService apiKeyService;
        public AiPronunciationHelper(ApiKeyService _apiKeyService)
        {
            apiKeyService = _apiKeyService;

        }
        //public AiPronunciationHelper()
        //{
            
        //}
        public async Task<string> GetPronunciationHelpFromGoogleAsync(string word)
        {
            string apiKey = apiKeyService.GetGoogleApiKey();
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
            var speechConfig = SpeechConfig.FromSubscription(apiKeyService.GetAzureApiKey(), "eastus");
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
        public async Task<string> GetKidFriendlyRhymesAsync(string word, CancellationToken ct = default)
        {
            string _geminiApiKey = apiKeyService.GetGoogleApiKey();
            // Defensive: empty/crazy inputs
            if (string.IsNullOrWhiteSpace(word))
                return "No word given.";

            // Gemini endpoint (v1beta; update as needed)
            string endpoint =
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={_geminiApiKey}";

            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, endpoint);

            // Prompt per your requirement
            string prompt = $"Give me simple, kid-friendly English words that rhyme with \"{word}\" in one line.";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }   
                    }
                }
            };

            string json = JsonSerializer.Serialize(requestBody);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            using var response = await client.SendAsync(request, ct);
            string responseString = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Gemini API error {response.StatusCode}: {responseString}");

            try
            {
                using var doc = JsonDocument.Parse(responseString);
                var root = doc.RootElement;

                if (!root.TryGetProperty("candidates", out var candidates) || candidates.GetArrayLength() == 0)
                    return "No response candidates from Gemini.";

                var parts = candidates[0].GetProperty("content").GetProperty("parts");
                if (parts.GetArrayLength() == 0)
                    return "No response parts from Gemini.";

                var text = parts[0].GetProperty("text").GetString();
                return text?.Trim() ?? "No rhyme text returned.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing Gemini response: {ex.Message}");
                Console.WriteLine($"Raw response: {responseString}");
                return "Unable to parse rhyme response.";
            }
        }

        /// <summary>
        /// Simple text-to-speech utility.
        /// </summary>
        public async Task<byte[]> ConvertTextToSpeechAsync(string text, CancellationToken ct = default)
        {
            string _azureSpeechKey = apiKeyService.GetAzureApiKey();
              string _azureRegion = "eastus";

                var speechConfig = SpeechConfig.FromSubscription(_azureSpeechKey, _azureRegion);
            speechConfig.SpeechSynthesisVoiceName = "en-US-JennyNeural";
            speechConfig.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Audio16Khz32KBitRateMonoMp3);

            using var synthesizer = new SpeechSynthesizer(speechConfig, audioConfig: null);
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

