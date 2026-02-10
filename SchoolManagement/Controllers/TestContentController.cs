using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Audio;
using SchoolManagement.ViewModel.TestContent;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestContentController : ControllerBase
    {
        private  TestContentInterface testContent;
        public TestContentController(TestContentInterface testContent)
        {
            this.testContent = testContent;
            
        }
        [HttpPost("test")]
        public string AddImage(TestContentVM testContentVM)
        {
            return testContent.AddDetails(testContentVM);
        }

        [HttpGet("{testId}")]
        public List<ImageDb> GetImages(int testId, int relationId)
        {
            return testContent.GetImages(testId, relationId);

        }
        [HttpPost("updateimage/{text}")]
        public string InsertImage(IFormFile file, string text)
        {
            string result = testContent.InsertImage(file, text);
            if (result != null)
            {
                return result;

            }
            return null;
        }


    }
}
