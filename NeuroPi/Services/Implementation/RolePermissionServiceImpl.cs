using NeuroPi.Data;
using NeuroPi.Models;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.RolePermission;

namespace NeuroPi.Services.Implementation
{
    public class RolePermissionServiceImpl : IRolePermisisionService
    {
        private readonly NeuroPiDbContext _context;

        public RolePermissionServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        public RolePermissionResponseVM AddRolePermission(RolePermissionRequestVM rolePermission)
        {
              
            var newPermission = RolePermissionRequestVM.ToModel(rolePermission);
            _context.RolePermissions.Add(newPermission);
            _context.SaveChanges();
            return new RolePermissionResponseVM
            {
                RolePermissionId = newPermission.RolePermissionId,
                RoleId = newPermission.RoleId,
                PermissionId = newPermission.PermissionId,
                TenantId = newPermission.TenantId,
                CanCreate = newPermission.CanCreate,
                CanRead = newPermission.CanRead,
                CanUpdate = newPermission.CanUpdate,
                CanDelete = newPermission.CanDelete,
                CreatedBy = newPermission.CreatedBy
            };



        }
        public RolePermissionResponseVM UpdateRolePermissionById(int id, RolePermissionVM rolePermission)
        {
            MRolePermission existingPermission = _context.RolePermissions.FirstOrDefault(rp => rp.RolePermissionId == id);
            if (existingPermission != null)
            {
                existingPermission.RolePermissionId = id;
                existingPermission.RoleId = rolePermission.RoleId;
                existingPermission.PermissionId = rolePermission.PermissionId;
                existingPermission.TenantId = rolePermission.TenantId;
                existingPermission.CanCreate = rolePermission.CanCreate;
                existingPermission.CanRead = rolePermission.CanRead;
                existingPermission.CanUpdate = rolePermission.CanUpdate;
                existingPermission.CanDelete = rolePermission.CanDelete;

            _context.SaveChanges();
                return new RolePermissionResponseVM
                {
                    RolePermissionId = existingPermission.RolePermissionId,
                    RoleId = existingPermission.RoleId,
                    PermissionId = existingPermission.PermissionId,
                    TenantId = existingPermission.TenantId,
                    CanCreate = existingPermission.CanCreate,
                    CanRead = existingPermission.CanRead,
                    CanUpdate = existingPermission.CanUpdate,
                    CanDelete = existingPermission.CanDelete
                };
            }
            return null;
            
        }
        

        public bool DeleteById(int id)
        {
            var roleResponse = _context.RolePermissions.FirstOrDefault(r => r.RolePermissionId == id);
            if(roleResponse == null)
            {
                return false;

            }
            _context.RolePermissions.Remove(roleResponse);
            _context.SaveChanges();
            return true;
        }

        public List<RolePermissionResponseVM> GetAllRolePermissions()
        {
            var rolePermissions = _context.RolePermissions.ToList();
            if (rolePermissions != null)
            {
                return RolePermissionResponseVM.ToViewModelList(rolePermissions);
            }
            return null;
        }

        public RolePermissionResponseVM GetRolePermissionById(int id)
        {
            var rolePermission = _context.RolePermissions.FirstOrDefault(rp => rp.RolePermissionId == id);
            if (rolePermission != null)
            {
                return RolePermissionResponseVM.ToViewModel(rolePermission);
            }
            return null;
        }


        
    }
}
