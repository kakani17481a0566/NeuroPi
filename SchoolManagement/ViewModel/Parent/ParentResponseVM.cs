using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Parent
{
    public class ParentResponseVM
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }


        public static ParentResponseVM ToViewModel(MParent parent)
        {
            return new ParentResponseVM()
            {
                Id = parent.Id,
                UserId = parent.UserId,
                TenantId = parent.TenantId,
                CreatedBy = parent.CreatedBy,
                CreatedOn = parent.CreatedOn,
                UpdatedBy = parent.UpdatedBy,
                UpdatedOn = parent.UpdatedOn
            };
        }

        public static List<ParentResponseVM> ToViewModelList(List<MParent> parentList)
        {
            List<ParentResponseVM> result = new List<ParentResponseVM>();
            foreach (var parent in parentList)
            {
                result.Add(ToViewModel(parent));
            }
            return result;

        }
    }
}