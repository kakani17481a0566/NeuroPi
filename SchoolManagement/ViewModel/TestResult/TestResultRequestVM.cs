using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.TestResult
{
    public class TestResultRequestVM
    {
        public int studentId { get; set; }

        public int testContentId { get; set; }
        public string result { get; set; }

        public int testId { get; set; }

        public int relationId { get; set; }

        public static MTestResult ToModel(TestResultRequestVM testResultRequest)
        {
            return new MTestResult()
            {
                StudentId = testResultRequest.studentId,
                TestContentId = testResultRequest.testContentId,
                Result = testResultRequest.result,
                TestId = testResultRequest.testId,
                RelationId = testResultRequest.relationId,

            };
        }
    }
}
