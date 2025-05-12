using Microsoft.EntityFrameworkCore;
using NeuroPi.Data;
using NeuroPi.Models;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.RolePermission;

namespace NeuroPi.Services.Implementation
{
    public class RolePermissionServiceImpl : IRolePermissionService
    {
        private readonly NeuroPiDbContext _context;

        public RolePermissionServiceImpl(NeuroPiDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<RolePermissionResponseVM> AddRolePermissionAsync(RolePermissionRequestVM rolePermission)
        {
            if (rolePermission == null) throw new ArgumentNullException(nameof(rolePermission));

            var newPermission = RolePermissionRequestVM.ToModel(rolePermission);
            await _context.RolePermissions.AddAsync(newPermission);
            await _context.SaveChangesAsync();

            return RolePermissionResponseVM.ToViewModel(newPermission);
        }

        public async Task<RolePermissionResponseVM> UpdateRolePermissionByIdAsync(int id, RolePermissionVM rolePermission)
        {
            if (rolePermission == null) throw new ArgumentNullException(nameof(rolePermission));

            var existingPermission = await _context.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RolePermissionId == id);

            if (existingPermission == null) return null;

            existingPermission.RoleId = rolePermission.RoleId;
            existingPermission.PermissionId = rolePermission.PermissionId;
            existingPermission.TenantId = rolePermission.TenantId;
            existingPermission.CanCreate = rolePermission.CanCreate;
            existingPermission.CanRead = rolePermission.CanRead;
            existingPermission.CanUpdate = rolePermission.CanUpdate;
            existingPermission.CanDelete = rolePermission.CanDelete;

            await _context.SaveChangesAsync();
            return RolePermissionResponseVM.ToViewModel(existingPermission);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var rolePermission = await _context.RolePermissions
                .FirstOrDefaultAsync(r => r.RolePermissionId == id);

            if (rolePermission == null) return false;

            _context.RolePermissions.Remove(rolePermission);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<RolePermissionResponseVM>> GetAllRolePermissionsAsync()
        {
            var rolePermissions = await _context.RolePermissions.ToListAsync();
            return rolePermissions?.Count > 0
                ? RolePermissionResponseVM.ToViewModelList(rolePermissions)
                : new List<RolePermissionResponseVM>();
        }

        public async Task<RolePermissionResponseVM> GetRolePermissionByIdAsync(int id)
        {
            var rolePermission = await _context.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RolePermissionId == id);

            return rolePermission != null
                ? RolePermissionResponseVM.ToViewModel(rolePermission)
                : null;
        }
    }
}