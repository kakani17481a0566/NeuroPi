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

            if (result > 0)
            {
                return RoleResponseVM.ToViewModel(roleModel);
            }
            return null; // Or throw an exception depending on how you want to handle this.
        }

        public bool DeleteRoleById(int id)
        {
            var role = _context.Roles.SingleOrDefault(r => r.RoleId == id);
            if (role == null)
            {
                return false; // Role not found
            }

            _context.Roles.Remove(role);
            var result = _context.SaveChanges();
            return result > 0; // Return true if the deletion was successful
        }

        public List<RoleResponseVM> GetAllRoles()
        {
            var roles = _context.Roles.Include(r => r.Tenant).ToList();
            return RoleResponseVM.ToViewModelList(roles);
        }

        public RoleResponseVM GetRoleById(int id)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == id);
            return role == null ? null : RoleResponseVM.ToViewModel(role); // Return null if not found
        }

        public RoleResponseVM UpdateRole(int id, RoleRequestVM roleRequestVM)
        {
            var existingRole = _context.Roles.FirstOrDefault(r => r.RoleId == id);
            if (existingRole == null)
            {
                return null; // Role not found
            }

            existingRole.Name = roleRequestVM.Name;
            existingRole.TenantId = roleRequestVM.TenantId;

            var result = _context.SaveChanges();
            return result > 0 ? RoleResponseVM.ToViewModel(existingRole) : null;
        }
    }
}
