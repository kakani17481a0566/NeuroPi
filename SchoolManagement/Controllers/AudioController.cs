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
    public async Task<IActionResult> UploadAudio(IFormFile audioFile,String text)
    {
        if (audioFile == null || audioFile.Length == 0)
            return BadRequest("Invalid audio file.");

        using var ms = new MemoryStream();
        await audioFile.CopyToAsync(ms);
        byte[] audioBytes = ms.ToArray();

        var transcriptionService = new AudioTranscriptionServiceImpl();
        byte[] audioResponse = await transcriptionService.TranscribeAudioAsync(audioBytes, Path.GetExtension(audioFile.FileName),text);

        return File(audioResponse, "audio/mpeg", "pronunciation_feedback.mp3");
    }

}
