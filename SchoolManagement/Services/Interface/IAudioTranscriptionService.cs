using SchoolManagement.ViewModel.Audio;

namespace SchoolManagement.Services.Interface
{
    public interface IAudioTranscriptionService
    {
        Task<byte[]> TranscribeAudioAsync(byte[] audioBytes, string fileExtension, string text);

        PronouncationResponseVM TranscribeAndCompareAsync(byte[] audioBytes, string fileExtension, string[] words);

        String CheckPronounciation(byte[] audioBytes,string text);

        string AddImage(IFormFile file, string text);
        List<ImageDb> GetImage();

    }
}