using NeuroPi.Data;
using NeuroPi.Models;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.Group;
using System.Collections.Generic;
using System.Linq;

namespace NeuroPi.Services.Implementation
{
    public class GroupServiceImpl : IGroupService
    {
        private readonly NeuroPiDbContext _context;

        public GroupServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        // Get all groups
        public List<GroupViewModel> GetAll()
        {
            return _context.Groups.Select(g => new GroupViewModel
            {
                GroupId = g.GroupId,
                Name = g.Name,
                TenantId = g.TenantId
            }).ToList();
        }

        // Get group by ID
        public GroupViewModel GetById(int id)
        {
            var group = _context.Groups.FirstOrDefault(g => g.GroupId == id);
            if (group == null) return null;

            return new GroupViewModel
            {
                GroupId = group.GroupId,
                Name = group.Name,
                TenantId = group.TenantId
            };
        }

        // Create a new group
        public GroupViewModel Create(GroupInputModel input)
        {
            var group = new MGroup
            {
                Name = input.Name,
                TenantId = input.TenantId
            };

            _context.Groups.Add(group);
            _context.SaveChanges();

            return new GroupViewModel
            {
                GroupId = group.GroupId,
                Name = group.Name,
                TenantId = group.TenantId
            };
        }

        // Update an existing group
        public GroupViewModel Update(int id, GroupUpdateInputModel input)
        {
            var group = _context.Groups.FirstOrDefault(g => g.GroupId == id);
            if (group == null) return null;

            group.Name = input.Name;
            _context.SaveChanges();

            return new GroupViewModel
            {
                GroupId = group.GroupId,
                Name = group.Name,
                TenantId = group.TenantId
            };
        }

        // Delete a group
        public bool Delete(int id)
        {
            var group = _context.Groups.FirstOrDefault(g => g.GroupId == id);
            if (group == null) return false;

            _context.Groups.Remove(group);
            _context.SaveChanges();
            return true;
        }
    }
}
