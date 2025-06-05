using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.AssessmentSkills;

namespace SchoolManagement.Services.Implementation
{
    public class AssessmentSkillsServiceImpl : IAssessmentSkillService
    {
        private readonly SchoolManagementDb _context;
        public AssessmentSkillsServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public AssessmentSkillsResponseVM CreateAssessmentSkill(AssessmentSkillsRequestVM skill)
        {
            var newAssessmentSkill = AssessmentSkillsRequestVM.ToModel(skill);
            newAssessmentSkill.CreatedOn = DateTime.UtcNow;
            _context.AssessmentSkills.Add(newAssessmentSkill);
            _context.SaveChanges();
            return AssessmentSkillsResponseVM.ToViewModel(newAssessmentSkill);
        }

        public bool DeleteAssessmentSkillByIdAndTenantId(int id, int tenantId)
        {
            var assessmentSkill = _context.AssessmentSkills
                .FirstOrDefault(s => s.Id == id && s.TenantId == tenantId && !s.IsDeleted);
            if (assessmentSkill == null)
            {
                return false;
            }
            assessmentSkill.IsDeleted = true;
            assessmentSkill.UpdatedOn = DateTime.UtcNow;
            _context.AssessmentSkills.Update(assessmentSkill);
            _context.SaveChanges();
            return true;
        }

        public List<AssessmentSkillsResponseVM> GetAllSkills()
        {
            var assessmentSkills = _context.AssessmentSkills
                .Where(s => !s.IsDeleted)
                .ToList();
            return assessmentSkills.Select(AssessmentSkillsResponseVM.ToViewModel).ToList();
        }

        public AssessmentSkillsResponseVM GetSkillById(int id)
        {
            var assessmentSkill = _context.AssessmentSkills
                .FirstOrDefault(s => s.Id == id && !s.IsDeleted);
            if (assessmentSkill == null)
            {
                return null; 
            }
            return AssessmentSkillsResponseVM.ToViewModel(assessmentSkill);
        }

        public AssessmentSkillsResponseVM GetSkillByIdAndTenantId(int id, int tenantId)
        {
            var assessmentSkill = _context.AssessmentSkills
                .FirstOrDefault(s => s.Id == id && s.TenantId == tenantId && !s.IsDeleted);
            if (assessmentSkill == null)
            {
                return null;
            }
            return AssessmentSkillsResponseVM.ToViewModel(assessmentSkill);
        }

        public List<AssessmentSkillsResponseVM> GetSkillsByTenantId(int tenantId)
        {
            var assessmentSkills = _context.AssessmentSkills
                .Where(s => s.TenantId == tenantId && !s.IsDeleted)
                .ToList();
            return assessmentSkills.Select(AssessmentSkillsResponseVM.ToViewModel).ToList();
        }

        public AssessmentSkillsResponseVM UpdateAssessmentSkill(int id, int tenantId, AssessmentSkillsUpdateVM skill)
        {
            var existingAssessmentSkill = _context.AssessmentSkills
                .FirstOrDefault(s => s.Id == id && s.TenantId == tenantId && !s.IsDeleted);
            if (existingAssessmentSkill == null)
            {
                return null; 
            }
            existingAssessmentSkill.Name = skill.Name;
            existingAssessmentSkill.Code = skill.Code;
            existingAssessmentSkill.Description = skill.Description;
            existingAssessmentSkill.SubjectId = skill.SubjectId;
            existingAssessmentSkill.UpdatedBy = skill.UpdatedBy;
            existingAssessmentSkill.UpdatedOn = DateTime.UtcNow;

            
            _context.SaveChanges();
            return AssessmentSkillsResponseVM.ToViewModel(existingAssessmentSkill);
                


        }
    }
}
