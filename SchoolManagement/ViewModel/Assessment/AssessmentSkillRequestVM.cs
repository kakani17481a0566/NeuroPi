using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Assessment
{
    public class AssessmentSkillRequestVM
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int SubjectId { get; set; }
        public int TenantId { get; set; }

        public static MAssessmentSkill ToModel(AssessmentSkillRequestVM request)
        {
            return new MAssessmentSkill
            {
                Name = request.Name,
                Code = request.Code,
                Description = request.Description,
                SubjectId = request.SubjectId,
                TenantId = request.TenantId
            };
        }
    }
}
