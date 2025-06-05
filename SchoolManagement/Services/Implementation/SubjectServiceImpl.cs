using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Subject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class SubjectServiceImpl : ISubjectService
    {
        private readonly SchoolManagementDb _dbContext;

        public SubjectServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }

        public List<SubjectResponseVM> GetAllSubjects()
        {
            return _dbContext.subjects
                .Where(s => !s.IsDeleted)
                .Select(SubjectResponseVM.FromModel)
                .ToList();
        }

        public List<SubjectResponseVM> GetAllSubjects(int tenantId)
        {
            return _dbContext.subjects
                .Where(s => !s.IsDeleted && s.TenantId == tenantId)
                .Select(SubjectResponseVM.FromModel)
                .ToList();
        }

        public SubjectResponseVM GetSubjectById(int id)
        {
            var subject = _dbContext.subjects.FirstOrDefault(s => s.Id == id && !s.IsDeleted);
            return SubjectResponseVM.FromModel(subject);
        }

        public SubjectResponseVM GetSubjectById(int id, int tenantId)
        {
            var subject = _dbContext.subjects.FirstOrDefault(s => s.Id == id && s.TenantId == tenantId && !s.IsDeleted);
            return SubjectResponseVM.FromModel(subject);
        }

        public SubjectResponseVM CreateSubject(SubjectRequestVM subjectVM)
        {
            var subject = subjectVM.ToModel();
            _dbContext.subjects.Add(subject);
             _dbContext.SaveChanges();
            return SubjectResponseVM.FromModel(subject);
        }

        public SubjectResponseVM UpdateSubject(int id, int tenantId, SubjectUpdateVM subjectVM)
        {
            var subject = _dbContext.subjects.FirstOrDefault(s => s.Id == id && s.TenantId == tenantId && !s.IsDeleted);
            if (subject == null)
                return null;

            subject.Name = subjectVM.Name;
            subject.Code = subjectVM.Code;
            subject.Description = subjectVM.Description;
            subject.UpdatedOn = DateTime.UtcNow;
            subject.UpdatedBy = subjectVM.UpdatedBy;

            _dbContext.SaveChanges();

            return SubjectResponseVM.FromModel(subject);
        }

        public bool DeleteSubject(int id, int tenantId)
        {
            var subject = _dbContext.subjects.FirstOrDefault(s => s.Id == id && s.TenantId == tenantId && !s.IsDeleted);
            if (subject == null)
                return false;

            subject.IsDeleted = true;
            subject.UpdatedOn = DateTime.UtcNow;

            _dbContext.SaveChanges();
            return true;
        }
    }
}
