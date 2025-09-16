using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Audio;
using SchoolManagement.ViewModel.TestContent;

namespace SchoolManagement.Services.Implementation
{

    public class TestContentServiceImpl : TestContentInterface
    {
        private readonly SchoolManagementDb _context;
        public TestContentServiceImpl(SchoolManagementDb context)
        {
            _context = context;
            
        }
        public string AddDetails(TestContentVM testContentVM )
        {
            MTestContent model=new MTestContent();
            model.name =testContentVM.name;
            model.testId = testContentVM.testId != 0 ?testContentVM.testId : 0;
            model.relationId = testContentVM.relationId;
            model.url= testContentVM!=null ?ConvertIFormFileToBytes(testContentVM.file):new byte[0];
            model.tenantId=testContentVM.tenantId;

            _context.TestContent.Add(model);
            _context.SaveChanges();
            if (model != null)
            {
                return "inserted";
            }
            return "not inserted";
            

        }
        public List<ImageDb> GetImages(int testId,int relationId)
        {
            var result = _context.TestContent.Where(t => !t.isDeleted && t.relationId == relationId && t.testId == testId).Take(5).ToList();
            List<ImageDb> images = new List<ImageDb>();
            if(result!=null)
            {
                foreach (var image in result) {
                    ImageDb imageDb = new ImageDb(image.name, image.url,image.testId,image.relationId,image.id);
                    images.Add(imageDb);
                }
                return images;
                
            }
            return null;
        }
        public byte[] ConvertIFormFileToBytes(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public string InsertImage(IFormFile file, string text)
        {
            var response=_context.TestContent.Where(t=>t.name.ToLower()==text.ToLower()&& !t.isDeleted).First();
            if(response.url == null)
            {
                response.url = ConvertIFormFileToBytes(file);
                _context.SaveChanges();
                return "inserted";
            }
            return "No update needed";
            

        }
    }
}
