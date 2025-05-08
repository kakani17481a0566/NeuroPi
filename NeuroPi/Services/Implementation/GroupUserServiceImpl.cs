using System.Text.RegularExpressions;
using NeuroPi.Data;
using NeuroPi.Models;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.GroupUser;

namespace NeuroPi.Services.Implementation
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
            var groupUser = new MGroupUser
            {
                GroupId = input.GroupId,
                UserId = input.UserId,
                TenantId = input.TenantId,
                CreatedBy = input.CreatedBy,

            };
            _context.GroupUsers.Add(groupUser);
            _context.SaveChanges();
            return new GroupUserVM {

                GroupId = input.GroupId,
                UserId = input.UserId,
                TenantId = input.TenantId,
                //CreatedBy = input.CreatedBy,

            };
            

        }

        public GroupUserUpdateVM updateGroupUserById(int GroupUserId, GroupUserUpdateVM input)

        {
            var groupUser = _context.GroupUsers.FirstOrDefault(x => x.GroupUserId == GroupUserId);
            if (groupUser != null)
            {

                groupUser.GroupId = input.GroupId;
                groupUser.UserId = input.UserId;
                groupUser.TenantId = input.TenantId;
                groupUser.UpdatedBy = input.UpdatedBy;
                groupUser.UpdatedOn = DateTime.UtcNow;

                _context.SaveChanges();
            }

            return input;
            
        }

        public bool deleteGroupUserById(int GroupId)
        {
            var groupUser = _context.GroupUsers.Find(GroupId);
            if (groupUser == null)
            {
                return false;
            }
            _context.Remove(groupUser);
            _context.SaveChanges();

            return true;
        }

        public List<GroupUserVM> getAllGroupUsers()
        {
            var result= _context.GroupUsers.ToList();
            return GroupUserVM.ToViewModelList(result);
            
        }

        public GroupUserVM getGroupUserById(int Groupid)
        {
            var groupUser = _context.GroupUsers
                .Where(x => x.GroupId == Groupid)
                .Select(g => new GroupUserVM
                {
                    GroupId = g.GroupId,
                    UserId = g.UserId,
                    TenantId = g.TenantId,
                })
                .FirstOrDefault();
            return groupUser;

        }

        
    }
}
