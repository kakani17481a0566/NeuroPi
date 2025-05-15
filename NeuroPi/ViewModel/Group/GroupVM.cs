namespace NeuroPi.UserManagment.ViewModel.Group
{
    public class GroupVM
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int TenantId { get; set; }
        public int? CreatedBy { get; set; }

        public bool IsDeleted { get; set; }

    }
}
