using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Parent
{
    public class ParentRequestVM
    {
        public int UserId { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MParent ToModel(ParentRequestVM vm)
        {
            return new MParent()
            {
                UserId = vm.UserId,
                TenantId = vm.TenantId,
                CreatedBy = vm.CreatedBy,
                CreatedOn = vm.CreatedOn
            };
        }
    }

}
