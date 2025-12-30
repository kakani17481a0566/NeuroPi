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
            // 1. Check for EXACT match (User, Tenant, Role) - ignoring IsDeleted status to handle soft-deletes
            var exactMatch = _context.UserRoles.FirstOrDefault(ur => 
                ur.UserId == userRole.UserId && 
                ur.TenantId == userRole.TenantId && 
                ur.RoleId == userRole.RoleId);

            if (exactMatch != null)
            {
                // Reactivate if deleted
                if (exactMatch.IsDeleted)
                {
                    exactMatch.IsDeleted = false;
                    exactMatch.UpdatedBy = userRole.CreatedBy;
                    exactMatch.UpdatedOn = DateTime.UtcNow;
                }

                // Ensure no OTHER active roles exist for this tenant (enforce One Role Per Tenant)
                var otherActiveRoles = _context.UserRoles.Where(ur => 
                    ur.UserId == userRole.UserId && 
                    ur.TenantId == userRole.TenantId && 
                    ur.RoleId != userRole.RoleId && 
                    !ur.IsDeleted).ToList();

                foreach (var role in otherActiveRoles)
                {
                    role.IsDeleted = true;
                    role.UpdatedBy = userRole.CreatedBy;
                    role.UpdatedOn = DateTime.UtcNow;
                }

                _context.SaveChanges();

                // Check "TEACHER" logic
                CheckAndAssignTeacherDepartment(exactMatch.UserId, exactMatch.RoleId, exactMatch.TenantId, userRole.CreatedBy);

                return new UserRoleVM
                {
                    UserRoleId = exactMatch.UserRoleId,
                    UserId = exactMatch.UserId,
                    RoleId = exactMatch.RoleId,
                    TenantId = exactMatch.TenantId
                };
            }

            // 2. If no exact match, check if there is ANY active role we need to replace
            var existingActive = _context.UserRoles.FirstOrDefault(ur => 
                ur.UserId == userRole.UserId && 
                ur.TenantId == userRole.TenantId && 
                !ur.IsDeleted);

            if (existingActive != null)
            {
                // Soft delete the existing role
                existingActive.IsDeleted = true;
                existingActive.UpdatedBy = userRole.CreatedBy;
                existingActive.UpdatedOn = DateTime.UtcNow;
                // No SaveChanges here, we'll save together with the new one
            }

            // 3. Create new role
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

            // Check "TEACHER" logic
            CheckAndAssignTeacherDepartment(entity.UserId, entity.RoleId, entity.TenantId, userRole.CreatedBy);

            return new UserRoleVM
            {
                UserRoleId = entity.UserRoleId,
                UserId = entity.UserId,
                RoleId = entity.RoleId,
                TenantId = entity.TenantId
            };
        }

        private void CheckAndAssignTeacherDepartment(int userId, int roleId, int tenantId, int createdBy)
        {
            var role = _context.Roles.FirstOrDefault(r=> r.RoleId == roleId);
            if (role != null && role.Name?.ToUpper() == "TEACHER")
            {
                var existingDept = _context.UserDepartments.FirstOrDefault(ud => ud.UserId == userId && ud.DepartmentId == 1 && ud.TenantId == tenantId && !ud.IsDeleted);
                if (existingDept == null)
                {
                    var userDept = new MUserDepartment
                    {
                        UserId = userId,
                        DepartmentId = 1, // Hardcoded
                        TenantId = tenantId,
                        CreatedBy = createdBy,
                        CreatedOn = DateTime.UtcNow
                    };
                    _context.UserDepartments.Add(userDept);
                    _context.SaveChanges();
                }
            }
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
