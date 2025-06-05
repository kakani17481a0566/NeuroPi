using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.CourseSubject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class CourseSubjectServiceImpl : ICourseSubjectService
    {
        private readonly SchoolManagementDb _dbContext;

        public CourseSubjectServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }


        public List<CourseSubjectResponseVM> GetAll()
        {
            return _dbContext.course_subject
                .Where(cs => !cs.IsDeleted)
                .Select(CourseSubjectResponseVM.FromModel)
                .ToList();
        }

        // Get all CourseSubjects by tenant
        public List<CourseSubjectResponseVM> GetAll(int tenantId)
        {
            return _dbContext.course_subject
                .Where(cs => cs.TenantId == tenantId && !cs.IsDeleted)
                .Select(CourseSubjectResponseVM.FromModel)
                .ToList();
        }

        // Get CourseSubject by ID and TenantId
        public CourseSubjectResponseVM GetById(int id, int tenantId)
        {
            var cs = _dbContext.course_subject
                .FirstOrDefault(c => c.Id == id && c.TenantId == tenantId && !c.IsDeleted);
            return CourseSubjectResponseVM.FromModel(cs);
        }

        // Get CourseSubject by ID only
        public CourseSubjectResponseVM GetById(int id)
        {
            var cs = _dbContext.course_subject
                .FirstOrDefault(c => c.Id == id && !c.IsDeleted);
            return CourseSubjectResponseVM.FromModel(cs);
        }

        // Create a new CourseSubject record
        public CourseSubjectResponseVM Create(CourseSubjectRequestVM request)
        {
            var model = request.ToModel();
            model.CreatedOn = DateTime.UtcNow;
            _dbContext.course_subject.Add(model);
            _dbContext.SaveChanges();
            return CourseSubjectResponseVM.FromModel(model);
        }

        // Update an existing CourseSubject record
        public CourseSubjectResponseVM Update(int id, int tenantId, CourseSubjectUpdateVM request)
        {
            var cs = _dbContext.course_subject
                .FirstOrDefault(c => c.Id == id && c.TenantId == tenantId && !c.IsDeleted);

            if (cs == null)
                return null;

            cs.CourseId = request.CourseId;
            cs.SubjectId = request.SubjectId;
            cs.UpdatedOn = DateTime.UtcNow;
            cs.UpdatedBy = request.UpdatedBy;

            _dbContext.SaveChanges();
            return CourseSubjectResponseVM.FromModel(cs);
        }

        // Soft delete a CourseSubject record
        public bool Delete(int id, int tenantId)
        {
            var cs = _dbContext.course_subject
                .FirstOrDefault(c => c.Id == id && c.TenantId == tenantId && !c.IsDeleted);

            if (cs == null)
                return false;

            cs.IsDeleted = true;
            cs.UpdatedOn = DateTime.UtcNow;
            _dbContext.SaveChanges();
            return true;
        }
    }
}
