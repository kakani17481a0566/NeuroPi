using SchoolManagement.ViewModel.Audio;
using SchoolManagement.ViewModel.TestContent;

namespace SchoolManagement.Services.Interface
{
    public interface TestContentInterface
    {
        string AddDetails(TestContentVM testContentVM);

        List<ImageDb> GetImages(int testId, int relationId);

        string InsertImage(IFormFile file, string text);
    }
}
