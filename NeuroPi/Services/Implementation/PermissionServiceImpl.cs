using Microsoft.EntityFrameworkCore;
using NeuroPi.Data;
using NeuroPi.Models;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.Permissions;

namespace NeuroPi.Services.Implementation
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
           var PermisssionModel= PermissionRequestVM.ToModel(permissionRequestVM);
            _context.Permissions.Add(PermisssionModel);
            _context.SaveChanges();
            return PermissionResponseVM.ToViewModel(PermisssionModel);
        }

        public MPermission DeletePermission(int id)
        {
            var result=GetById(id);
            if (result != null)
            {
                _context.Permissions.Remove(result);
                _context.SaveChanges();
                return result;
            }
            return null;
        }

        public MPermission GetById(int id)
        {
            var result=_context.Permissions.FirstOrDefault(p=>p.PermissionId == id);
            if (result != null)
            {
                return result;
            }
            return null;
            
        }

        public List<PermissionResponseVM> GetPermissions()
        {
            var result = _context.Permissions.ToList();
            if (result != null)
            {
                return PermissionResponseVM.ToViewModelList(result);
            }
            return null;
        }

        public PermissionResponseVM UpdatePermission(int id, PermissionRequestVM requestVM)
        {
            var result=GetById(id);
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
