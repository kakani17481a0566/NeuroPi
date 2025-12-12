using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Assessment
{
    public class AssessmentSkillResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int TenantId { get; set; }

        public static AssessmentSkillResponseVM FromModel(MAssessmentSkill model)
        {
            return new AssessmentSkillResponseVM
            {
                Id = model.Id,
                Name = model.Name,
                Code = model.Code,
                Description = model.Description,
                SubjectId = model.SubjectId,
                SubjectName = model.Subject?.Name,
                TenantId = model.TenantId
            };
        }
    }
}
