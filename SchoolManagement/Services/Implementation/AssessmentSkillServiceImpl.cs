using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Assessment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class AssessmentSkillServiceImpl : IAssessmentSkillService
    {
        private readonly SchoolManagementDb _db;

        public AssessmentSkillServiceImpl(SchoolManagementDb db)
        {
            _db = db;
        }

        public List<AssessmentSkillResponseVM> GetAll(int tenantId)
        {
            return _db.AssessmentSkills
                .Include(x => x.Subject)
                .Where(x => x.TenantId == tenantId && !x.IsDeleted)
                .Select(AssessmentSkillResponseVM.FromModel)
                .ToList();
        }

        public AssessmentSkillResponseVM GetById(int id, int tenantId)
        {
            var entity = _db.AssessmentSkills
                .Include(x => x.Subject)
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            return entity != null ? AssessmentSkillResponseVM.FromModel(entity) : null;
        }

        public List<AssessmentSkillResponseVM> GetBySubjectId(int subjectId, int tenantId)
        {
            return _db.AssessmentSkills
                .Include(x => x.Subject)
                .Where(x => x.SubjectId == subjectId && x.TenantId == tenantId && !x.IsDeleted)
                .Select(AssessmentSkillResponseVM.FromModel)
                .ToList();
        }

        public AssessmentSkillResponseVM Create(AssessmentSkillRequestVM request)
        {
            var model = AssessmentSkillRequestVM.ToModel(request);
            model.CreatedOn = DateTime.UtcNow;
            model.IsDeleted = false;

            _db.AssessmentSkills.Add(model);
            _db.SaveChanges();

            return GetById(model.Id, request.TenantId);
        }

        public AssessmentSkillResponseVM Update(int id, int tenantId, AssessmentSkillUpdateVM request)
        {
            var entity = _db.AssessmentSkills
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null) return null;

            entity.Name = request.Name;
            entity.Code = request.Code;
            entity.Description = request.Description;
            entity.SubjectId = request.SubjectId;
            entity.UpdatedOn = DateTime.UtcNow;

            _db.SaveChanges();

            return GetById(id, tenantId);
        }

        public bool Delete(int id, int tenantId)
        {
            var entity = _db.AssessmentSkills
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null) return false;

            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;

            _db.SaveChanges();
            return true;
        }
    }
}
