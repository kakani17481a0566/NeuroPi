using Microsoft.EntityFrameworkCore;
using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.RolePermission;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class RolePermissionServiceImpl : IRolePermissionService
    {
        private readonly NeuroPiDbContext _context;

        public RolePermissionServiceImpl(NeuroPiDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public RolePermissionResponseVM AddRolePermission(RolePermissionRequestVM rolePermission)
        {
            if (rolePermission == null) throw new ArgumentNullException(nameof(rolePermission));

            var newPermission = RolePermissionRequestVM.ToModel(rolePermission);
            _context.RolePermissions.Add(newPermission);
            _context.SaveChanges();

            return RolePermissionResponseVM.ToViewModel(newPermission);
        }

        public RolePermissionResponseVM UpdateRolePermissionById(int id, RolePermissionVM rolePermission)
        {
            if (rolePermission == null) throw new ArgumentNullException(nameof(rolePermission));

            var existingPermission = _context.RolePermissions
                .FirstOrDefault(rp => rp.RolePermissionId == id);

            if (existingPermission == null) return null;

            existingPermission.RoleId = rolePermission.RoleId;
            existingPermission.PermissionId = rolePermission.PermissionId;
            existingPermission.TenantId = rolePermission.TenantId;
            existingPermission.CanCreate = rolePermission.CanCreate;
            existingPermission.CanRead = rolePermission.CanRead;
            existingPermission.CanUpdate = rolePermission.CanUpdate;
            existingPermission.CanDelete = rolePermission.CanDelete;

            _context.SaveChanges();
            return RolePermissionResponseVM.ToViewModel(existingPermission);
        }

        public bool DeleteById(int id)
        {
            var rolePermission = _context.RolePermissions
                .FirstOrDefault(r => r.RolePermissionId == id);

            if (rolePermission == null) return false;

            rolePermission.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }

        public List<RolePermissionResponseVM> GetAllRolePermissions()
        {
            var rolePermissions = _context.RolePermissions.Where(r=>!r.IsDeleted).ToList();
            return rolePermissions?.Count > 0
                ? RolePermissionResponseVM.ToViewModelList(rolePermissions)
                : new List<RolePermissionResponseVM>();
        }

        public RolePermissionResponseVM GetRolePermissionById(int id)
        {
            var rolePermission = _context.RolePermissions
                .FirstOrDefault(rp => rp.RolePermissionId == id);

            return rolePermission != null
                ? RolePermissionResponseVM.ToViewModel(rolePermission)
                : null;
        }
    }
}
