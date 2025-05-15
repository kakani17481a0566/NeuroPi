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
                CreatedBy = input.CreatedBy,

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

            // Update the Name, Description and CreatedBy fields if they are provided
            if (!string.IsNullOrEmpty(input.Name))
            {
                group.Name = input.Name;
                group.UpdatedBy= input.UpdatedBy;  // Update UpdatedBy if provided
                group.UpdatedOn = DateTime.UtcNow;  // Update UpdatedOn to current time
            }


          
            // Save changes to the database
            _context.SaveChanges();

            // Return the updated Group ViewModel
            return new GroupVM
            {
                GroupId = group.GroupId,
                Name = group.Name,
                TenantId = group.TenantId,

                UpdatedBy =group.UpdatedBy,

                CreatedBy = group.CreatedBy,
                IsDeleted = group.IsDeleted
            };
        }


        // Soft delete a group
        public bool DeleteById(int groupId, int tenantId)
        {
            var group = _context.Groups
                .FirstOrDefault(g => g.GroupId == groupId && g.TenantId == tenantId && !g.IsDeleted);

            if (group == null) return false;

            // Perform the soft delete
            group.IsDeleted = true;
            group.UpdatedOn = DateTime.UtcNow;

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
