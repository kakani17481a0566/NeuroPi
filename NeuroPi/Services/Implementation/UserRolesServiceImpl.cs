using System;
using System.Collections.Generic;
using System.Linq;
using NeuroPi.Data;
using NeuroPi.Models;
using NeuroPi.Services.Interface;

namespace NeuroPi.Services.Implementation
{
    public class UserRolesServiceImpl : IUserRolesService
    {
        private readonly NeuroPiDbContext _context;

        public UserRolesServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        public List<MUserRole> GetAll()
        {
            return _context.UserRoles.Where(x => x.DeletedAt == null).ToList();
        }

        public MUserRole GetById(int id)
        {
            return _context.UserRoles.FirstOrDefault(x => x.UserRoleId == id && x.DeletedAt == null);
        }

        public MUserRole Create(MUserRole userRole)
        {
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();
            return userRole;
        }

        public MUserRole Update(int id, MUserRole userRole)
        {
            var existing = _context.UserRoles.FirstOrDefault(x => x.UserRoleId == id && x.DeletedAt == null);
            if (existing == null) return null;

            existing.UserId = userRole.UserId;
            existing.RoleId = userRole.RoleId;
            existing.TenantId = userRole.TenantId;
            existing.UpdatedBy = userRole.UpdatedBy;
            existing.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var existing = _context.UserRoles.FirstOrDefault(x => x.UserRoleId == id && x.DeletedAt == null);
            if (existing == null) return false;

            existing.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return true;
        }
    }
}
