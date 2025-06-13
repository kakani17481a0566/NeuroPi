using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.DailyAssessment;
using System;
using System.Collections.Generic;
using System.Linq;


// Developed by: Mohith
namespace SchoolManagement.Services.Implementation
{
    public class DailyAssessmentServiceImpl : IDailyAssessmentService
    {
        private readonly SchoolManagementDb _context;

        public DailyAssessmentServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public List<DailyAssessmentResponseVm> GetAll()
        {
            return _context.DailyAssessments
                .Where(x => !x.IsDeleted)
                .Select(DailyAssessmentResponseVm.FromModel)
                .ToList();
        }

        public List<DailyAssessmentResponseVm> GetAllByTenant(int tenantId)
        {
            return _context.DailyAssessments
                .Where(x => !x.IsDeleted && x.TenantId == tenantId)
                .Select(DailyAssessmentResponseVm.FromModel)
                .ToList();
        }

        public DailyAssessmentResponseVm GetById(int id)
        {
            var entity = _context.DailyAssessments.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            return entity != null ? DailyAssessmentResponseVm.FromModel(entity) : null;
        }
        public DailyAssessmentResponseVm GetById(int id, int tenantId)
        {
            var entity = _context.DailyAssessments
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            return entity != null ? DailyAssessmentResponseVm.FromModel(entity) : null;
        }

        public DailyAssessmentResponseVm Update(int id, int tenantId, DailyAssessmentUpdateVm request)
        {
            var entity = _context.DailyAssessments
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null) return null;

            entity.AssessmentDate = request.AssessmentDate;
            entity.TimeTableId = request.TimeTableId;
            entity.WorksheetId = request.WorksheetId;
            entity.GradeId = request.GradeId; 
       
            entity.StudentId = request.StudentId;
            //entity.CourseId = request.CourseId;
            entity.ConductedById = request.ConductedById;
            entity.BranchId = request.BranchId;
            entity.UpdatedBy = request.UpdatedBy;
            entity.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();

            return DailyAssessmentResponseVm.FromModel(entity);
        }

        public DailyAssessmentResponseVm Create(DailyAssessmentRequestVm request)
        {
            var model = request.ToModel();
            _context.DailyAssessments.Add(model);
            _context.SaveChanges();
            return DailyAssessmentResponseVm.FromModel(model);
        }

        public bool Delete(int id, int tenantId)
        {
            var entity = _context.DailyAssessments
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null) return false;

            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return true;
        }
    }
}
