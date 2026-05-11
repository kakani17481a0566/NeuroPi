using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.QuestionPaperQuestions
{
    public class QuestionPaperQuestionsCreateVM
    {
        public int TenantId { get; set; }
        public int PaperId { get; set; }
        public int QuestionId { get; set; }
        public int Sq { get; set; }
        public int CreatedBy { get; set; }

        public static MQuestionPaperQuestion ToModel(QuestionPaperQuestionsCreateVM vm)
        {
            return new MQuestionPaperQuestion
            {
                TenantId = vm.TenantId,
                PaperId = vm.PaperId,
                QuestionId = vm.QuestionId,
                Sq = vm.Sq,
                CreatedBy = vm.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}
