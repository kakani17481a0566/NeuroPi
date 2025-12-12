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
    public class AssessmentServiceImpl : IAssessmentService
    {
        private readonly SchoolManagementDb _db;

        public AssessmentServiceImpl(SchoolManagementDb db)
        {
            _db = db;
        }

        public List<AssessmentResponseVM> GetAll(int tenantId)
        {
            return _db.Assessments
                .Include(x => x.Topic)
                .Include(x => x.AssessmentSkill)
                .Where(x => x.TenantId == tenantId && !x.IsDeleted)
                .Select(AssessmentResponseVM.FromModel)
                .ToList();
        }

        public AssessmentResponseVM GetById(int id, int tenantId)
        {
            var entity = _db.Assessments
                .Include(x => x.Topic)
                .Include(x => x.AssessmentSkill)
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            return entity != null ? AssessmentResponseVM.FromModel(entity) : null;
        }

        public List<AssessmentResponseVM> GetBySkillId(int skillId, int tenantId)
        {
             return _db.Assessments
                .Include(x => x.Topic)
                .Include(x => x.AssessmentSkill)
                .Where(x => x.AssessmentSkillId == skillId && x.TenantId == tenantId && !x.IsDeleted)
                .Select(AssessmentResponseVM.FromModel)
                .ToList();
        }

        public AssessmentResponseVM Create(AssessmentRequestVM request)
        {
            var model = AssessmentRequestVM.ToModel(request);
            model.CreatedOn = DateTime.UtcNow;
            model.IsDeleted = false;

            _db.Assessments.Add(model);
            _db.SaveChanges();

            return GetById(model.Id, request.TenantId);
        }

        public AssessmentResponseVM Update(int id, int tenantId, AssessmentUpdateVM request)
        {
            var entity = _db.Assessments
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null) return null;

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.TopicId = request.TopicId;
            entity.AssessmentSkillId = request.AssessmentSkillId;
            entity.UpdatedOn = DateTime.UtcNow;

            _db.SaveChanges();

            return GetById(id, tenantId);
        }

        public bool Delete(int id, int tenantId)
        {
            var entity = _db.Assessments
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null) return false;

            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;

            _db.SaveChanges();
            return true;
        }
    }
}
