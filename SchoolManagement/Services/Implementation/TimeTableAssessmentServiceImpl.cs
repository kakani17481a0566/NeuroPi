using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.TimeTableAssessment;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Services.Implementation
{
    public class TimeTableAssessmentServiceImpl : ITimeTableAssessmentService
    {
        private readonly SchoolManagementDb _context;
        public TimeTableAssessmentServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public TimeTableAssessmentResponseVM AddTimeTableAssessment(TimeTableAssessmentRequestVM timeTableAssessment)
        {
            var newAssessment = TimeTableAssessmentRequestVM.ToModel(timeTableAssessment);
            newAssessment.CreatedOn = DateTime.UtcNow;
            _context.TimeTableAssessments.Add(newAssessment);
            _context.SaveChanges();
            return TimeTableAssessmentResponseVM.ToViewModel(newAssessment);

        }

        public bool DeleteTimeTableAssessment(int id, int tenantId)
        {
            var assessment = _context.TimeTableAssessments
                .FirstOrDefault(a => a.Id == id && a.TenantId == tenantId && !a.IsDeleted);
            if (assessment == null)
            {
                return false;
            }
            assessment.IsDeleted = true;
            assessment.UpdatedOn = DateTime.UtcNow;
            _context.TimeTableAssessments.Update(assessment);
            _context.SaveChanges();
            return true;
        }

        public List<TimeTableAssessmentResponseVM> GetAllTimeTableAssessments()
        {
            var assessments = _context.TimeTableAssessments
                .Include(a => a.Assessment)
                .ThenInclude(a => a.AssessmentSkill)
                .Include(a => a.Assessment)
                .ThenInclude(a => a.Topic)
                .Where(a => !a.IsDeleted)
                .ToList();
            return assessments.Select(TimeTableAssessmentResponseVM.ToViewModel).ToList();
        }

        public TimeTableAssessmentResponseVM GetTimeTableAssessmentById(int id)
        {
            var assessment = _context.TimeTableAssessments
                .Include(a => a.Assessment)
                .ThenInclude(a => a.AssessmentSkill)
                .Include(a => a.Assessment)
                .ThenInclude(a => a.Topic)
                .FirstOrDefault(a => a.Id == id && !a.IsDeleted);
            if (assessment == null)
            {
                return null;
            }
            return TimeTableAssessmentResponseVM.ToViewModel(assessment);

        }

        public TimeTableAssessmentResponseVM GetTimeTableAssessmentByTenantIdAndId(int tenantId, int id)
        {
            var assessment = _context.TimeTableAssessments
                .Include(a => a.Assessment)
                .ThenInclude(a => a.AssessmentSkill)
                .Include(a => a.Assessment)
                .ThenInclude(a => a.Topic)
                .FirstOrDefault(a => a.Id == id && a.TenantId == tenantId && !a.IsDeleted);
            if (assessment == null)
            {
                return null;
            }
            return TimeTableAssessmentResponseVM.ToViewModel(assessment);
        }

        public List<TimeTableAssessmentResponseVM> GetTimeTableAssessmentsByTenantId(int tenantId)
        {
            var assessments = _context.TimeTableAssessments
                .Include(a => a.Assessment)
                .ThenInclude(a => a.AssessmentSkill)
                .Include(a => a.Assessment)
                .ThenInclude(a => a.Topic)
                .Where(a => a.TenantId == tenantId && !a.IsDeleted)
                .ToList();
            return assessments.Select(TimeTableAssessmentResponseVM.ToViewModel).ToList();

        }

        public List<TimeTableAssessmentResponseVM> GetTimeTableAssessmentsByTimeTableId(int timeTableId, int tenantId)
        {
            var assessments = _context.TimeTableAssessments
                .Include(a => a.Assessment)
                .ThenInclude(a => a.AssessmentSkill)
                .Include(a => a.Assessment)
                .ThenInclude(a => a.Topic)
                .Where(a => a.TimeTableId == timeTableId && a.TenantId == tenantId && !a.IsDeleted)
                .ToList();
            return assessments.Select(TimeTableAssessmentResponseVM.ToViewModel).ToList();
        }

        public TimeTableAssessmentResponseVM UpdateTimeTableAssessment(int id, int tenantId, TimeTableAssessmentUpdateVM timeTableAssessment)
        {
            var existingAssessment = _context.TimeTableAssessments
                .FirstOrDefault(a => a.Id == id && a.TenantId == tenantId && !a.IsDeleted);
            if (existingAssessment == null)
            {
                return null;
            }
            existingAssessment.TimeTableId = timeTableAssessment.TimeTableId;
            existingAssessment.AssessmentId = timeTableAssessment.AssessmentId;
            existingAssessment.Status = timeTableAssessment.Status;
            existingAssessment.UpdatedBy = timeTableAssessment.UpdatedBy;
            existingAssessment.UpdatedOn = DateTime.UtcNow;
            _context.TimeTableAssessments.Update(existingAssessment);
            _context.SaveChanges();
            return TimeTableAssessmentResponseVM.ToViewModel(existingAssessment);

        }
    }
}
