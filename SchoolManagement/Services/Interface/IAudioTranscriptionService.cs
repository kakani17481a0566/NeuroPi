namespace SchoolManagement.Services.Interface
{
    public interface IAudioTranscriptionService
    {
        Task<byte[]> TranscribeAudioAsync(byte[] audioBytes, string fileExtension,string text);

    }
}