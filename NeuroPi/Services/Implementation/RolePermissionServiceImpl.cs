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
            newPermission.CreatedOn = DateTime.UtcNow;
            _context.RolePermissions.Add(newPermission);
            _context.SaveChanges();

            return RolePermissionResponseVM.ToViewModel(newPermission);
        }

        public RolePermissionResponseVM UpdateRolePermissionByIdAndTenantId(int id, int tenantId, RolePermissionVM rolePermission)
        {
            var result = _context.RolePermissions.FirstOrDefault(r => r.RolePermissionId == id && r.TenantId == tenantId);
            if (result == null)
            {
                return null;
            }
            result.RoleId = rolePermission.RoleId;
            result.MenuId = rolePermission.PermissionId;
            result.UpdatedBy = rolePermission.UpdatedBy;
            result.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();

            return RolePermissionResponseVM.ToViewModel(result);
                
        }

        public bool DeleteByIdAndTenantId(int id, int tenantId)
        {
            var rolePermission = _context.RolePermissions.FirstOrDefault(r => r.RolePermissionId == id && r.TenantId == tenantId && !r.IsDeleted);
            if (rolePermission == null)
            {
                return false;
            }
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

        public RolePermissionResponseVM GetRolePermissionByIdAndTenantId(int id, int tenantId)
        {
            var rolePermission = _context.RolePermissions.FirstOrDefault(r => r.RolePermissionId == id &&r.TenantId==tenantId&&!r.IsDeleted);
            if (rolePermission != null)
            {
                return RolePermissionResponseVM.ToViewModel(rolePermission);
            }
            return null;
        }

        public List<RolePermissionResponseVM> GetRolePermissionByTenantId(int tenantId)
        {
            var rolePermission = _context.RolePermissions.Where(r => r.TenantId == tenantId && !r.IsDeleted).ToList();
            if (rolePermission != null)
            {
                return RolePermissionResponseVM.ToViewModelList(rolePermission);

            }
            return null;
        }

        public List<RolePermissionDescVM> GetRolePermissionByRoleIdAndTenantId(int roleId, int tenantId)
        {
            var rolePermission = _context.RolePermissions.Where(r => r.RoleId == roleId && r.TenantId == tenantId && !r.IsDeleted).Include(r=>r.Menu).ToList();
            if (rolePermission != null)
            {
                return RolePermissionDescVM.ToViewModelList(rolePermission);
            }
            return null;
        }
    }
}
