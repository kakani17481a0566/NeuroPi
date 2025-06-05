using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.AssessmentSkills
{
    public class AssessmentSkillsRequestVM
    {
        public string Name { get; set; }

        public string Code { get; set; }
        public string Description { get; set; }
        public int SubjectId { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        
        public static MAssessmentSkill ToModel(AssessmentSkillsRequestVM vm)
        {
            return new MAssessmentSkill()
            { 
                Name = vm.Name,
                Code = vm.Code,
                Description = vm.Description,
                SubjectId = vm.SubjectId,
                TenantId = vm.TenantId,
                CreatedBy = vm.CreatedBy,
                CreatedOn = DateTime.UtcNow,

            };

        }


    }
}
