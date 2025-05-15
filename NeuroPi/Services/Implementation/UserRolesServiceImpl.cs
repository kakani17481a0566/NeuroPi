using System;
using System.Collections.Generic;
using System.Linq;
using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.UserRoles;
using Microsoft.EntityFrameworkCore;
using NeuroPi.UserManagment.Data;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class UserRolesServiceImpl : IUserRolesService
    {
        private readonly NeuroPiDbContext _context;

        public UserRolesServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        // Get all non-deleted user roles
        public List<UserRoleVM> GetAll()
        {
            var roles = _context.UserRoles
                .Where(x => !x.IsDeleted)
                .Include(x => x.User) 
                .Include(x => x.Role) 
                .Include(x => x.Tenant) 
                .Select(r => new UserRoleVM
                {
                    UserRoleId = r.UserRoleId,
                    UserId = r.UserId,
                    RoleId = r.RoleId,
                    TenantId = r.TenantId
                }).ToList();

            return roles;
        }

        public UserRoleVM GetUserRoleById(int id)
        {
            var role = _context.UserRoles
                .Where(x => x.UserRoleId == id && !x.IsDeleted)
                .Include(x => x.User)
                .Include(x => x.Role)
                .Include(x => x.Tenant)
                .FirstOrDefault();

            if (role == null) return null;

            return new UserRoleVM
            {
                UserRoleId = role.UserRoleId,
                UserId = role.UserId,
                RoleId = role.RoleId,
                TenantId = role.TenantId
            };
        }

        public UserRoleVM Create(UserRoleCreateVM userRole)
        {
            var entity = new MUserRole
            {
                UserId = userRole.UserId,
                RoleId = userRole.RoleId,
                TenantId = userRole.TenantId,
                CreatedBy = userRole.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.UserRoles.Add(entity);
            _context.SaveChanges();

            return new UserRoleVM
            {
                UserRoleId = entity.UserRoleId,
                UserId = entity.UserId,
                RoleId = entity.RoleId,
                TenantId = entity.TenantId
            };
        }

        public UserRoleVM Update(int id, UserRoleUpdateVM userRole)
        {
            var existingRole = _context.UserRoles
                .Where(x => x.UserRoleId == id && !x.IsDeleted)
                .FirstOrDefault();

            if (existingRole == null)
                return null;

            existingRole.UserId = userRole.UserId;
            existingRole.RoleId = userRole.RoleId;
            existingRole.UpdatedBy = userRole.UpdatedBy;
            existingRole.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();

            return new UserRoleVM
            {
                UserRoleId = existingRole.UserRoleId,
                UserId = existingRole.UserId,
                RoleId = existingRole.RoleId,
                TenantId = existingRole.TenantId
            };
        }

        public bool DeleteByTenantIdAndId(int tenantId, int id)
        {
            var existingRole = _context.UserRoles
                .Where(x => x.UserRoleId == id && x.TenantId == tenantId && !x.IsDeleted)
                .FirstOrDefault();

            if (existingRole == null)
                return false;

            existingRole.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }

        public List<UserRoleVM> GetUserRolesByTenantId(int tenantId)
        {
            var roles = _context.UserRoles
                .Where(x => x.TenantId == tenantId && !x.IsDeleted)
                .Include(x => x.User)
                .Include(x => x.Role)
                .Include(x => x.Tenant)
                .Select(r => new UserRoleVM
                {
                    UserRoleId = r.UserRoleId,
                    UserId = r.UserId,
                    RoleId = r.RoleId,
                    TenantId = r.TenantId
                }).ToList();

            return roles;
        }

        public UserRoleVM GetUserRoleByTenantIdAndId(int tenantId, int id)
        {
            var role = _context.UserRoles
                .Where(x => x.UserRoleId == id && x.TenantId == tenantId && !x.IsDeleted)
                .Include(x => x.User)
                .Include(x => x.Role)
                .Include(x => x.Tenant)
                .FirstOrDefault();

            if (role == null)
                return null;

            return new UserRoleVM
            {
                UserRoleId = role.UserRoleId,
                UserId = role.UserId,
                RoleId = role.RoleId,
                TenantId = role.TenantId
            };
        }
    }
}
