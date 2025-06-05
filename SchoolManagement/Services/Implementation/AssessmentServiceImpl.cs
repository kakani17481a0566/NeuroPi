using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Assessment;

namespace SchoolManagement.Services.Implementation
{
    public class AssessmentServiceImpl : IAssessmentService

    {
        private readonly SchoolManagementDb _context;
        public AssessmentServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public AssessmentResponseVM CreateAssessment(AssessmentRequestVM assessment)
        {
            var newAssessment = AssessmentRequestVM.ToModel(assessment);
            newAssessment.CreatedOn = DateTime.UtcNow;
            _context.Assessments.Add(newAssessment);
            _context.SaveChanges();
            return AssessmentResponseVM.ToViewModel(newAssessment);

        }

        public bool DeleteAssessmentByIdAndTenantId(int id, int tenantId)
        {
            var assessment = _context.Assessments
                .FirstOrDefault(a => a.Id == id && a.TenantId == tenantId && !a.IsDeleted);
            if (assessment == null)
            {
                return false;
            }
            assessment.IsDeleted = true;
            assessment.UpdatedOn = DateTime.UtcNow;
            _context.Assessments.Update(assessment);
            _context.SaveChanges();
            return true;
        }

        public List<AssessmentResponseVM> GetAllAssessments()
        {
            var assessments = _context.Assessments.ToList();
            return assessments.Select(AssessmentResponseVM.ToViewModel).ToList();
        }

        public AssessmentResponseVM GetAssessmentById(int id)
        {
            var assessment = _context.Assessments.FirstOrDefault(a => a.Id == id && !a.IsDeleted);
            if (assessment == null)
            {
                return null;
            }
            return AssessmentResponseVM.ToViewModel(assessment);
        }

        public AssessmentResponseVM GetAssessmentByIdAndTenantId(int id, int tenantId)
        {
            var assessment = _context.Assessments
                .FirstOrDefault(a => a.Id == id && a.TenantId == tenantId && !a.IsDeleted);
            if (assessment == null)
            {
                return null;
            }
            return AssessmentResponseVM.ToViewModel(assessment);

        }

        public List<AssessmentResponseVM> GetAssessmentsByTenantId(int tenantId)
        {
            var assessments = _context.Assessments
                .Where(a => a.TenantId == tenantId && !a.IsDeleted)
                .ToList();
            return assessments.Select(AssessmentResponseVM.ToViewModel).ToList();
        }

        public AssessmentResponseVM UpdateAssessment(int id, int tenantId, AssessmentUpdateVM assessment)
        {
            var existingAssessment = _context.Assessments
                .FirstOrDefault(a => a.Id == id && a.TenantId == tenantId && !a.IsDeleted);
            if (existingAssessment == null)
            {
                return null;
            }
            existingAssessment.Name = assessment.Name;
            existingAssessment.Description = assessment.Description;
            existingAssessment.TopicId = assessment.TopicId;
            existingAssessment.AssessmentSkillId = assessment.AssessmentSkillId;
            existingAssessment.UpdatedBy = assessment.UpdatedBy;
            existingAssessment.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return AssessmentResponseVM.ToViewModel(existingAssessment);

        }
    }
}
