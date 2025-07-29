using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Implementation;
using SchoolManagement.Services.Interface;

public class AudioController : ControllerBase
{
    private readonly IAudioTranscriptionService _transcriptionService;

    public AudioController(IAudioTranscriptionService transcriptionService)
    {
        _transcriptionService = transcriptionService;
    }

    [HttpPost("upload/{text}")]
    public IActionResult UploadAudio(IFormFile audioFile, string text)
    {
        if (audioFile == null || audioFile.Length == 0)
            return BadRequest("Invalid audio file.");
        string[] words = text.Split(' ');
        using var ms = new MemoryStream();
        audioFile.CopyTo(ms); // Synchronous version
        byte[] audioBytes = ms.ToArray();
        if (words.Length != 0 && words.Length == 1)
        {
            byte[] audioResponse = _transcriptionService
                .TranscribeAudioAsync(audioBytes, Path.GetExtension(audioFile.FileName), text)
                .GetAwaiter()
                .GetResult();

            // Convert audio to base64 string with data URI
            string base64Audio = Convert.ToBase64String(audioResponse);
            //string dataUri = $"data:audio/mpeg;base64,{base64Audio}";

            //Return JSON with misPronouncedWords(optional) and rhymses(audio)
            return Ok(new
            {
                misPronouncedWords = new string[] { }, // or logic to detect any
                rhymses = base64Audio
            });
        }

        else
        {
            var result = _transcriptionService
           .TranscribeAndCompareAsync(audioBytes, Path.GetExtension(audioFile.FileName), words);

            return Ok(result);
        }
    }


}
