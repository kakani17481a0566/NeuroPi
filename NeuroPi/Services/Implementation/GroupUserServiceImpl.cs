using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.GroupUser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class GroupUserServiceImpl : IGroupUserService
    {
        private readonly NeuroPiDbContext _context;

        public GroupUserServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        public GroupUserVM createGroupUser(GroupUserInputVM input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var groupUser = new MGroupUser
            {
                GroupId = input.GroupId,
                UserId = input.UserId,
                TenantId = input.TenantId,
                CreatedBy = input.CreatedBy
            
            };
            groupUser.CreatedOn = DateTime.UtcNow;
            _context.GroupUsers.Add(groupUser);
            _context.SaveChanges();

            return GroupUserVM.ToViewModel(groupUser);
        }

        public GroupUserUpdateVM updateGroupUserByIdAndTenantId(int groupUserId, int tenantId, GroupUserUpdateVM input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var groupUser = _context.GroupUsers
                                    .FirstOrDefault(x => x.GroupUserId == groupUserId && x.TenantId == tenantId && !x.IsDeleted);
            if (groupUser == null)
                return null;

            groupUser.GroupId = input.GroupId;
            groupUser.UserId = input.UserId;
            groupUser.UpdatedBy = input.UpdatedBy;
            groupUser.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();

            return new GroupUserUpdateVM
            {
                GroupId = groupUser.GroupId,
                UserId = groupUser.UserId,
                UpdatedBy = groupUser.UpdatedBy ?? input.UpdatedBy ,
              
            };
        }

        public bool deleteGroupUserByGroupUserIdAndTenantId(int groupUserId, int tenantId)
        {
            var groupUser = _context.GroupUsers
                                    .FirstOrDefault(x => x.GroupUserId == groupUserId && x.TenantId == tenantId && !x.IsDeleted);

            if (groupUser != null)
            {
                groupUser.IsDeleted = true;
                groupUser.UpdatedOn = DateTime.UtcNow;
                // optionally track who deleted it via UpdatedBy if needed
                _context.SaveChanges();
                return true;
            }

            return false;
        }


        public List<GroupUserVM> getAllGroupUsers()
        {
            var groupUsers = _context.GroupUsers
                                     .Where(x => !x.IsDeleted)
                                     .ToList();

            return GroupUserVM.ToViewModelList(groupUsers);
        }

        public GroupUserVM getGroupUserByGroupUserId(int groupUserId)
        {
            var groupUser = _context.GroupUsers
                                    .Where(x => x.GroupUserId == groupUserId && !x.IsDeleted)
                                    .FirstOrDefault();

            return groupUser != null ? GroupUserVM.ToViewModel(groupUser) : null;
        }

        public GroupUserVM getGroupUserByGroupUserIdAndTenantId(int groupUserId, int tenantId)
        {
            var groupUser = _context.GroupUsers
                                    .Where(x => x.GroupUserId == groupUserId && x.TenantId == tenantId && !x.IsDeleted)
                                    .FirstOrDefault();

            return groupUser != null ? GroupUserVM.ToViewModel(groupUser) : null;
        }

        public List<GroupUserVM> getGroupUsersByTenantId(int tenantId)
        {
            var groupUsers = _context.GroupUsers
                                     .Where(x => x.TenantId == tenantId && !x.IsDeleted)
                                     .ToList();

            return GroupUserVM.ToViewModelList(groupUsers);
        }
    }
}
