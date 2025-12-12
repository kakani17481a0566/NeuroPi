using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Assessment
{
    public class AssessmentRequestVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? TopicId { get; set; }
        public int AssessmentSkillId { get; set; }
        public int TenantId { get; set; }

        public static MAssessment ToModel(AssessmentRequestVM request)
        {
            return new MAssessment
            {
                Name = request.Name,
                Description = request.Description,
                TopicId = request.TopicId,
                AssessmentSkillId = request.AssessmentSkillId,
                TenantId = request.TenantId
            };
        }
    }
}
