using Microsoft.EntityFrameworkCore;
using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Role;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class RoleServiceImpl : IRoleService
    {
        private readonly NeuroPiDbContext _context;

        public RoleServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        public RoleResponseVM AddRole(RoleRequestVM roleRequestVM)
        {
            var roleModel = RoleRequestVM.ToModel(roleRequestVM);
            _context.Roles.Add(roleModel);
            var result = _context.SaveChanges();

            return result > 0 ? RoleResponseVM.ToViewModel(roleModel) : null;
        }

        public bool DeleteRoleByTenantIdAndId(int tenantId, int id)
        {
            var role = _context.Roles
                .FirstOrDefault(r => r.RoleId == id && r.TenantId == tenantId && !r.IsDeleted);

            if (role == null)
            {
                return false;
            }

            // Mark the role as deleted (soft delete)
            role.IsDeleted = true;
            var result = _context.SaveChanges();
            return result > 0;
        }


        public List<RoleResponseVM> GetAllRoles()
        {
            var roles = _context.Roles.Include(r => r.Tenant).Where(r => !r.IsDeleted).ToList();
            return roles.Any() ? RoleResponseVM.ToViewModelList(roles) : null;
        }

        public List<RoleResponseVM> GetAllRolesByTenantId(int tenantId)
        {
            var roles = _context.Roles.Where(r => !r.IsDeleted && r.TenantId == tenantId).ToList();
            return roles.Any() ? RoleResponseVM.ToViewModelList(roles) : null;
        }

        public RoleResponseVM GetRoleById(int id)
        {
            var role = _context.Roles.FirstOrDefault(r => !r.IsDeleted && r.RoleId == id);
            return role != null ? RoleResponseVM.ToViewModel(role) : null;
        }

        public RoleResponseVM UpdateRoleByTenantIdAndId(int tenantId, int id, RoleRequestVM roleRequestVM)
        {
           
            var existingRole = _context.Roles
                .Where(r => !r.IsDeleted && r.TenantId == tenantId && r.RoleId == id)
                .FirstOrDefault();

            if (existingRole == null)
            {
                return null;
            }

        
            existingRole.Name = roleRequestVM.Name;

          
            existingRole.UpdatedOn = DateTime.UtcNow;

            var result = _context.SaveChanges();
            return result > 0 ? RoleResponseVM.ToViewModel(existingRole) : null;
        }


        public RoleResponseVM GetRoleByTenantIdAndId(int tenantId, int id)
        {
            var role = _context.Roles
                .FirstOrDefault(r => !r.IsDeleted && r.TenantId == tenantId && r.RoleId == id);
            return role != null ? RoleResponseVM.ToViewModel(role) : null;
        }
    }
}
