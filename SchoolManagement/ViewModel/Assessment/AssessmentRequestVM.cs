using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Assessment
{
    public class AssessmentRequestVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TopicId { get; set; }
        public int AssessmentSkillId { get; set; }
        public int TenantId { get; set; }

        public static MAssessment ToModel(AssessmentRequestVM assessmentRequest)
        {
            return new MAssessment()
            {
                Name = assessmentRequest.Name,
                Description = assessmentRequest.Description,
                TopicId = assessmentRequest.TopicId,
                AssessmentSkillId = assessmentRequest.AssessmentSkillId,
                TenantId = assessmentRequest.TenantId

            };
        }
        
            


    }
    
}
