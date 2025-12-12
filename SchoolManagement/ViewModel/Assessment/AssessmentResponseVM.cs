using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Assessment
{
    public class AssessmentResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? TopicId { get; set; }
        public string TopicName { get; set; }
        public int AssessmentSkillId { get; set; }
        public string AssessmentSkillName { get; set; }
        public int TenantId { get; set; }

        public static AssessmentResponseVM FromModel(MAssessment model)
        {
            return new AssessmentResponseVM
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                TopicId = model.TopicId,
                TopicName = model.Topic?.Name,
                AssessmentSkillId = model.AssessmentSkillId,
                AssessmentSkillName = model.AssessmentSkill?.Name,
                TenantId = model.TenantId
            };
        }
    }
}
