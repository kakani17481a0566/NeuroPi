using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

using SchoolManagement.Data;

namespace SchoolManagement.Services.Implementation
{
    public class RolesServiceImpl : IRolesService
    {
        private readonly SchoolManagementDb _context;

        public RolesServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public List<RolesResponseVM> GetAll(int tenantId)
        {
            var roles = _context.Roles.Where(x => x.TenantId == tenantId && !x.IsDeleted).ToList();
            return RolesResponseVM.ToViewModelList(roles);
        }

        public RolesResponseVM GetById(int id)
        {
            var role = _context.Roles.FirstOrDefault(x => x.RoleId == id && !x.IsDeleted);
            return role != null ? RolesResponseVM.ToViewModel(role) : null;
        }

        public RolesResponseVM Create(RolesRequestVM request)
        {
            var model = RolesRequestVM.ToModel(request);
            _context.Roles.Add(model);
            _context.SaveChanges();
            return RolesResponseVM.ToViewModel(model);
        }

        public RolesResponseVM Update(int id, RolesUpdateVM request)
        {
            var existing = _context.Roles.FirstOrDefault(x => x.RoleId == id && !x.IsDeleted);
            if (existing == null) return null;

            existing.Name = request.Name;
            existing.UpdatedOn = DateTime.UtcNow;
            existing.UpdatedBy = request.UpdatedBy;

            _context.SaveChanges();
            return RolesResponseVM.ToViewModel(existing);
        }

        public bool Delete(int id)
        {
            var existing = _context.Roles.FirstOrDefault(x => x.RoleId == id);
            if (existing == null) return false;

            existing.IsDeleted = true;
            existing.UpdatedOn = DateTime.UtcNow;
            
            _context.SaveChanges();
            return true;
        }
    }
}
