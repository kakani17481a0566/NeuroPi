using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Group;
using System.Linq;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class GroupServiceImpl : IGroupService
    {
        private readonly NeuroPiDbContext _context;

        public GroupServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        public List<GroupVM> GetAll()
        {
            return _context.Groups
                .Where(g => !g.IsDeleted)
                .Select(g => new GroupVM
                {
                    GroupId = g.GroupId,
                    Name = g.Name,
                    TenantId = g.TenantId
                }).ToList();
        }

        public GroupVM GetByGroupId(int groupId)
        {
            var group = _context.Groups.FirstOrDefault(g => g.GroupId == groupId && !g.IsDeleted);
            if (group == null) return null;

            return new GroupVM
            {
                GroupId = group.GroupId,
                Name = group.Name,
                TenantId = group.TenantId
            };
        }

        public GroupVM Create(GroupInputVM input)
        {
            var tenantExists = _context.Tenants.Any(t => t.TenantId == input.TenantId);
            if (!tenantExists)
            {
                return null;
            }

            var group = new MGroup
            {
                Name = input.Name,
                TenantId = input.TenantId,
                IsDeleted = false
            };

            _context.Groups.Add(group);
            _context.SaveChanges();

            return new GroupVM
            {
                GroupId = group.GroupId,
                Name = group.Name,
                TenantId = group.TenantId
            };
        }

        public GroupVM Update(int groupId, GroupUpdateInputVM input)
        {
            var group = _context.Groups.FirstOrDefault(g => g.GroupId == groupId && !g.IsDeleted);
            if (group == null) return null;

            group.Name = input.Name;
            _context.SaveChanges();

            return new GroupVM
            {
                GroupId = group.GroupId,
                Name = group.Name,
                TenantId = group.TenantId
            };
        }

        public bool Delete(int groupId)
        {
            var group = _context.Groups.FirstOrDefault(g => g.GroupId == groupId && !g.IsDeleted);
            if (group == null) return false;

            group.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }

        public List<GroupVM> GetByTenantId(int tenantId)
        {
            return _context.Groups
                .Where(g => !g.IsDeleted && g.TenantId == tenantId)
                .Select(g => new GroupVM
                {
                    GroupId = g.GroupId,
                    Name = g.Name,
                    TenantId = g.TenantId
                }).ToList();
        }

        public GroupVM GetByTenantAndGroupId(int tenantId, int groupId)
        {
            var group = _context.Groups
                .FirstOrDefault(g => g.GroupId == groupId && g.TenantId == tenantId && !g.IsDeleted);
            if (group == null) return null;

            return new GroupVM
            {
                GroupId = group.GroupId,
                Name = group.Name,
                TenantId = group.TenantId
            };
        }
    }
}
