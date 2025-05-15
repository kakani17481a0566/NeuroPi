using NeuroPi.UserManagment.Model;

public class GroupUserVM
{
    public int GroupUserId { get; set; }  // Add GroupUserId here
    public int GroupId { get; set; }
    public int UserId { get; set; }
    public int TenantId { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }

    // Mapping function to map MGroupUser to GroupUserVM
    public static GroupUserVM ToViewModel(MGroupUser user)
    {
        return new GroupUserVM
        {
            GroupUserId = user.GroupUserId,  // Map GroupUserId
            GroupId = user.GroupId,
            UserId = user.UserId,
            TenantId = user.TenantId,
            CreatedBy = user.CreatedBy,
            CreatedOn = user.CreatedOn,
            UpdatedOn = user.UpdatedOn
        };
    }

    public static List<GroupUserVM> ToViewModelList(List<MGroupUser> groupUsers)
    {
        List<GroupUserVM> groupUserVMs = new List<GroupUserVM>();
        foreach (MGroupUser user in groupUsers)
        {
            groupUserVMs.Add(ToViewModel(user));
        }
        return groupUserVMs;
    }
}
