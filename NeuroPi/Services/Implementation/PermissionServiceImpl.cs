using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Permissions;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class PermissionServiceImpl : IPermissionService
    {
        private readonly NeuroPiDbContext _context;
        public PermissionServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }
        public PermissionResponseVM AddPermission(PermissionRequestVM permissionRequestVM)
        {
            var PermisssionModel = PermissionRequestVM.ToModel(permissionRequestVM);
            _context.Permissions.Add(PermisssionModel);
            _context.SaveChanges();
            return PermissionResponseVM.ToViewModel(PermisssionModel);
        }

        public MPermission DeletePermission(int id)
        {
            var result = _context.Permissions.Where(p=>!p.IsDeleted).FirstOrDefault(p => p.PermissionId == id);
            if (result != null)
            {
                result.IsDeleted = true;
                _context.SaveChanges();
                return result;
            }
            return null;
        }

        public List<PermissionResponseVM> GetAllPermissionsByTenantId(int tenantId)
        {
            var result=_context.Permissions.Where(p=>p.TenantId==tenantId).Where(p=>!p.IsDeleted).ToList();
            if (result != null && result.Count>0)
            {
                return PermissionResponseVM.ToViewModelList(result);
            }
            return null;

        }

        public PermissionResponseVM GetById(int id)
        {
            var result = _context.Permissions.Where(p=>!p.IsDeleted).FirstOrDefault(p => p.PermissionId == id);
            if (result != null )
            {
                return PermissionResponseVM.ToViewModel(result);
            }
            return null;

        }

        public List<PermissionResponseVM> GetPermissions()
        {
            var result = _context.Permissions.Where(p => !p.IsDeleted).ToList();
            if (result != null && result.Count>0)

            {
                return PermissionResponseVM.ToViewModelList(result);
            }
            return null;
        }

        public PermissionResponseVM UpdatePermission(int id, PermissionRequestVM requestVM)
        {
            var result = _context.Permissions.Where(p => !p.IsDeleted).FirstOrDefault(p => p.PermissionId == id);
            if (result != null)
            {
                result.PermissionId = id;
                result.Name = requestVM.Name;
                result.Description = requestVM.Description;
                result.TenantId = requestVM.TenantId;
                _context.SaveChanges();
                return PermissionResponseVM.ToViewModel(result);
            }
            return null;
        }
    }
}
