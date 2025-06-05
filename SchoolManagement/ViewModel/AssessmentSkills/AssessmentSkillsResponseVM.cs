using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.AssessmentSkills
{
    public class AssessmentSkillsResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }
        public string Description { get; set; }
        public int SubjectId { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public static AssessmentSkillsResponseVM ToViewModel(MAssessmentSkill skill)
        {
            return new AssessmentSkillsResponseVM
            {
                Id = skill.Id,
                Name = skill.Name,
                Code = skill.Code,
                Description = skill.Description,
                SubjectId = skill.SubjectId,
                TenantId = skill.TenantId,
                CreatedBy = skill.CreatedBy,
                CreatedOn = skill.CreatedOn,
                UpdatedBy = skill.UpdatedBy,
                UpdatedOn = skill.UpdatedOn,
            };

           
        }
        public static List<AssessmentSkillsResponseVM> ToViewModelList(List<MAssessmentSkill> skillList)
        {
            List<AssessmentSkillsResponseVM> result = new List<AssessmentSkillsResponseVM>();
            foreach(var assessmentSkill in skillList)
            {
                result.Add(ToViewModel(assessmentSkill));

            }
            return result;


        }
    }
}
