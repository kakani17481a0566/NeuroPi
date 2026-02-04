using System;
using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Term;

namespace SchoolManagement.Services.Implementation
{
    public class TermServiceImpl : ITermService
    {
        private readonly SchoolManagementDb _dbContext;

        public TermServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TermResponseVM> GetAll()
        {
            return _dbContext.Terms
                .Where(t => !t.IsDeleted)
                .Select(TermResponseVM.FromModel)
                .ToList();
        }

        public List<TermResponseVM> GetByTenantId(int tenantId)
        {
            return _dbContext.Terms
                .Where(t => !t.IsDeleted && t.TenantId == tenantId)
                .Select(TermResponseVM.FromModel)
                .ToList();
        }

        public TermResponseVM GetById(int id)
        {
            var term = _dbContext.Terms.FirstOrDefault(t => t.Id == id && !t.IsDeleted);
            return TermResponseVM.FromModel(term);
        }

        public TermResponseVM GetById(int id, int tenantId)
        {
            var term = _dbContext.Terms.FirstOrDefault(t => t.Id == id && t.TenantId == tenantId && !t.IsDeleted);
            return TermResponseVM.FromModel(term);
        }

        public List<TermResponseVM> GetByAcademicYearId(int academicYearId)
        {
            return _dbContext.Terms
                .Where(t => !t.IsDeleted && t.AcademicYearId == academicYearId)
                .Select(TermResponseVM.FromModel)
                .ToList();
        }

        public TermResponseVM Create(TermRequestVM termVM)
        {
            var term = termVM.ToModel();
            term.CreatedOn = DateTime.UtcNow;
            _dbContext.Terms.Add(term);
            _dbContext.SaveChanges();
            return TermResponseVM.FromModel(term);
        }

        public TermResponseVM Update(int id, int tenantId, TermUpdateVM termVM)
        {
            var term = _dbContext.Terms.FirstOrDefault(t => t.Id == id && t.TenantId == tenantId && !t.IsDeleted);
            if (term == null)
                return null;

            term.Name = termVM.Name;
            term.StartDate = termVM.StartDate;
            term.EndDate = termVM.EndDate;
            term.AcademicYearId = termVM.AcademicYearId;
            term.UpdatedOn = DateTime.UtcNow;
            term.UpdatedBy = termVM.UpdatedBy;

            _dbContext.SaveChanges();
            return TermResponseVM.FromModel(term);
        }

        public bool Delete(int id, int tenantId)
        {
            var term = _dbContext.Terms.FirstOrDefault(t => t.Id == id && t.TenantId == tenantId && !t.IsDeleted);
            if (term == null)
                return false;

            term.IsDeleted = true;
            term.UpdatedOn = DateTime.UtcNow;
            _dbContext.SaveChanges();

            return true;
        }
    }
}
