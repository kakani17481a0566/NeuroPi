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

        // Get all non-deleted user roles
        public List<MUserRole> GetAll()
        {
            return _context.UserRoles.Where(x => !x.IsDeleted).ToList();
        }

        // Get a non-deleted user role by ID
        public MUserRole GetById(int id)
        {
            return _context.UserRoles.FirstOrDefault(x => x.UserRoleId == id && !x.IsDeleted);
        }

        // Create a new user role
        public MUserRole Create(MUserRole userRole)
        {
            userRole.IsDeleted = false; // Default value for IsDeleted
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();
            return userRole;
        }

        // Update an existing user role
        public MUserRole Update(int id, MUserRole userRole)
        {
            var existing = _context.UserRoles.FirstOrDefault(x => x.UserRoleId == id && !x.IsDeleted);
            if (existing == null) return null;

            existing.UserId = userRole.UserId;
            existing.RoleId = userRole.RoleId;
            existing.TenantId = userRole.TenantId;
            existing.UpdatedBy = userRole.UpdatedBy;
            existing.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return existing;
        }

        // Soft delete a user role by setting IsDeleted to true
        public bool Delete(int id)
        {
            var existing = _context.UserRoles.FirstOrDefault(x => x.UserRoleId == id && !x.IsDeleted);
            if (existing == null) return false;

            existing.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }
    }
}
