using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.GroupUser;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class GroupUserServiceImpl : IGroupUserService
    {
        private readonly NeuroPiDbContext _context;

        public GroupUserServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        // Create new GroupUser
        public GroupUserVM createGroupUser(GroupUserInputVM input)
        {
            // Basic validation check
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var groupUser = new MGroupUser
            {
                GroupId = input.GroupId,
                UserId = input.UserId,
                TenantId = input.TenantId,
                CreatedBy = input.CreatedBy,
                CreatedOn = DateTime.UtcNow // Always set CreatedOn
            };

            _context.GroupUsers.Add(groupUser);
            _context.SaveChanges();

            // Returning the newly created GroupUserVM
            return new GroupUserVM
            {
                GroupId = input.GroupId,
                UserId = input.UserId,
                TenantId = input.TenantId
            };
        }

        // Update GroupUser by ID
        public GroupUserUpdateVM updateGroupUserById(int groupUserId, GroupUserUpdateVM input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var groupUser = _context.GroupUsers.FirstOrDefault(x => x.GroupUserId == groupUserId && !x.IsDeleted);
            if (groupUser == null)
                return null; // If the groupUser doesn't exist, return null

            groupUser.GroupId = input.GroupId;
            groupUser.UserId = input.UserId;
            groupUser.TenantId = input.TenantId;
            groupUser.UpdatedBy = input.UpdatedBy;
            groupUser.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return input; // Return the updated GroupUser
        }

        // Soft delete a GroupUser by GroupId
        public bool deleteGroupUserById(int groupUserId)
        {
            var groupUser = _context.GroupUsers.FirstOrDefault(x => x.GroupUserId == groupUserId && !x.IsDeleted);
            if (groupUser == null)
            {
                return false; // If the groupUser doesn't exist, return false
            }

            // Mark as deleted instead of actually removing
            groupUser.IsDeleted = true;
            _context.SaveChanges();

            return true;
        }

        // Get all GroupUsers (non-deleted)
        public List<GroupUserVM> getAllGroupUsers()
        {
            var result = _context.GroupUsers
                                 .Where(x => !x.IsDeleted)  // Ensure you're only fetching non-deleted records
                                 .ToList();

            return GroupUserVM.ToViewModelList(result);
        }

        // Get GroupUser by GroupId (non-deleted)
        public GroupUserVM getGroupUserById(int groupId)
        {
            var groupUser = _context.GroupUsers
                                    .Where(x => x.GroupId == groupId && !x.IsDeleted)  // Exclude soft-deleted
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
