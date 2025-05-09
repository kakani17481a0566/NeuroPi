using Microsoft.EntityFrameworkCore;
using NeuroPi.Data;
using NeuroPi.Models;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.Role;

namespace NeuroPi.Services.Implementation
{
    public class RoleServiceImpl : IRoleService

    {
        private readonly NeuroPiDbContext _context;
        public RoleServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        public RoleResponseVM AddRole(RoleRequestVM role)
        {
            MRole roleModel = RoleRequestVM.ToModel(role);
            _context.Roles.Add(roleModel);
             var result=_context.SaveChanges();
            if (result > 0)
            {
                RoleResponseVM response = new RoleResponseVM
                {
                    Id = roleModel.RoleId, // Assuming 'Id' is the primary key property in your MRole entity
                    Name = roleModel.Name,
                    TenantId = roleModel.TenantId
                };
                return response;

            }
            return null;

        }

        public void DeleteRoleById(int id)
        {
            var roleModel = _context.Roles.FirstOrDefault(r=>r.RoleId== id);
            if (roleModel != null)
            {
                _context.Roles.Remove(roleModel);
                _context.SaveChanges();
            }
        }

        public List<RoleResponseVM> GetAllRoles()
        { 
            return RoleResponseVM.ToViewModelList(_context.Roles.Include(r=>r.Tenant).ToList());

        }

        public RoleResponseVM GetRoleById(int id)
        {
            return RoleResponseVM.ToViewModel(_context.Roles.FirstOrDefault(r=>r.RoleId == id));
        }

        public RoleResponseVM UpdateRole(int id,RoleRequestVM role)
        {
            MRole existedRole = _context.Roles.FirstOrDefault(r => r.RoleId == id);
            if (existedRole != null)
            {
                existedRole.Name = role.Name;
                existedRole.TenantId=role.TenantId;
                _context.SaveChanges();

                MRole roleModel = new MRole()
                {
                    RoleId = existedRole.RoleId,
                    Name = role.Name,
                    TenantId = role.TenantId
                };
                return RoleResponseVM.ToViewModel(roleModel);
            }
            return null;
        
        }

    }

}
