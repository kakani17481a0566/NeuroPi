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

        // Get all groups (excluding soft-deleted)
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

        // Get group by ID (excluding soft-deleted)
        public GroupVM GetById(int id)
        {
            var group = _context.Groups.FirstOrDefault(g => g.GroupId == id && !g.IsDeleted);
            if (group == null) return null;

            return new GroupVM
            {
                GroupId = group.GroupId,
                Name = group.Name,
                TenantId = group.TenantId
            };
        }

        // Create a new group
        public GroupVM Create(GroupInputVM input)
        {
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

        // Update an existing group
        public GroupVM Update(int id, GroupUpdateInputVM input)
        {
            var group = _context.Groups.FirstOrDefault(g => g.GroupId == id && !g.IsDeleted);
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

        // Soft delete a group
        public bool Delete(int id)
        {
            var group = _context.Groups.FirstOrDefault(g => g.GroupId == id && !g.IsDeleted);
            if (group == null) return false;

            group.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }
    }
}
