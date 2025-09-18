using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Audio;
using SchoolManagement.ViewModel.TestResult;

namespace SchoolManagement.Services.Implementation
{
    public class TestResultServiceImpl : ITestResultService
    {
        private readonly SchoolManagementDb _context;
        public TestResultServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public string AddResult(TestResultRequestVM request)
        {
            bool result = _context.TestResult.Any
                (r => r.StudentId == request.studentId && r.TestContentId == request.testContentId && r.RelationId == request.relationId);
            if (result)
            {
                return "Record already exists";
            }

            MTestResult model=TestResultRequestVM.ToModel(request);
            
            _context.TestResult.Add(model);
            _context.SaveChanges();
            return "saved";
            
        }

        public List<TestResultResponseVM> GetResult(int studentId, int testId, int relationId)
        {
            List<TestResultResponseVM> list=new List<TestResultResponseVM>();
            var result=_context.TestResult.Where(t=>t.TestId==testId && t.RelationId==relationId && t.StudentId==studentId).Include(t=>t.testContent).ToList();
            if (result != null)
            {
                foreach(MTestResult test in result)
                {
                    TestResultResponseVM testVM = new TestResultResponseVM()
                    {
                        testId = test.TestId,
                        relationId = relationId,
                        name=test.testContent.name,
                        testContentId = test.TestContentId,
                        url=test.testContent.url,
                        result=test.Result


                    };
                    list.Add(testVM);

                }
                return list;
            }
            return null;
        }

        public async Task<List<ImageDb>> GetResultImagesAsync(int studentId, int testId)
        {
            List<ImageDb> images = new List<ImageDb>();
            var result = await _context.TestResult.Where(t => t.TestId == testId && t.StudentId == studentId).OrderByDescending(t => t.TestContentId).FirstOrDefaultAsync();
            List<MTestContent> res = null;
            if (result != null)
            {
                var response = _context.TestContent.Where(t => t.testId == testId && t.relationId == result.RelationId).Max();
                if (result.TestContentId == response.id)
                {
                    return [];
                }
                else
                {
                    res=_context.TestContent.Where(t=>t.id>result.TestContentId).ToList();

                }

            }
            else
            {
                res = _context.TestContent.Where(t => t.testId == testId && t.relationId == 0).ToList();

            }
            foreach(MTestContent testContent in res)
            {
                ImageDb imageDb = new ImageDb(testContent.name,testContent.url,testContent.testId,testContent.relationId,testContent.id);
                images.Add(imageDb);

            }
            return images;
            
        }

        List<ImageDb> ITestResultService.GetResultImagesAsync(int studentId, int testId)
        {
            return GetResultImagesAsync(studentId, testId).GetAwaiter().GetResult();
        }
    }
}
