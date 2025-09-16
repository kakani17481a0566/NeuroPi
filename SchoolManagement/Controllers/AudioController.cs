using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Implementation;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Audio;
using System.Threading.Channels;

//[EnableCors("AllowAll")]
[Route("api/[controller]")]
[ApiController]
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
    [HttpGet("/getImages")]
    public List<ImageDb> GetAllText()
    {
        //List<string> response = new List<string>()
        //{
        //     "Apple",
        //     "Ball"
        //};
        List<ImageDb> images = new List<ImageDb>();
        //Chapel, grapple, dapple, and scrapple.
        //ImageDb img1 = new ImageDb("Chapel", new byte[0]);
        //ImageDb img2 = new ImageDb("grapple", new byte[0]);
        //ImageDb img3 = new ImageDb("dapple", new byte[0]);
        //images.Add(img1);
        //images.Add(img2);
        //images.Add(img3);
        return  images;
        
    }
    [HttpPost("/{text}")]
    public string TestPronounciation(IFormFile audioFile, string text)
    {
        using var ms = new MemoryStream();
        audioFile.CopyTo(ms);
        byte[] audioBytes = ms.ToArray();
        return  _transcriptionService.CheckPronounciation(audioBytes,text);
    }

    [HttpGet("/images/{text}")]
    public List<ImageDb> GetImages(string text)
    {
        if (text.ToLower() == "abc") {
            return _transcriptionService.GetImage();
        }
        else
        {
            List<ImageDb> images = new List<ImageDb>();
            //ImageDb img1 = new ImageDb("call", "");
            //ImageDb img2 = new ImageDb("tall", "");
            //ImageDb img3 = new ImageDb("bell", "");
            //images.Add(img1);
            //images.Add(img2);
            //images.Add(img3);
            return images;
        }
    }
    [HttpGet("/response")]
    public IActionResult GetStudentResult()
    {
        List<StudentResultVM> response= new List<StudentResultVM>();
        StudentResultVM student = new StudentResultVM("APPLE", "correct", "https://drive.google.com/file/d/1ooP_MQe2Q3BLwRijoojtWf77GknLHS_y/preview");
        StudentResultVM student1 = new StudentResultVM("CAT", "InCorrect", "https://drive.google.com/file/d/1FXryj9uhjCGXOaNnOwTXIDKQnxZWo8uN/preview");
        StudentResultVM student3 = new StudentResultVM("CAT", "InCorrect", "https://drive.google.com/file/d/1FXryj9uhjCGXOaNnOwTXIDKQnxZWo8uN/preview");
        response.Add(student1);
        response.Add(student);
        response.Add(student3);
        return Ok( response);

    }
    [HttpPost("/image/{text}")]
    public string AddImage(IFormFile file,string text)
    {
        string result = _transcriptionService.AddImage(file, text);
        if(result != null)
        {
            return result;

        }
        return null;
    }


}
