using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.ItemLocation
{
    public class ItemLocationResponseVM
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int BranchId { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public static ItemLocationResponseVM ToViewModel(MItemLocation itemLocation)
        {
            return new ItemLocationResponseVM
            {
                Id = itemLocation.Id,
                Name = itemLocation.Name,
                BranchId = itemLocation.BranchId,
                TenantId = itemLocation.TenantId,
                CreatedBy = itemLocation.CreatedBy,
                CreatedOn = itemLocation.CreatedOn,
                UpdatedBy = itemLocation.UpdatedBy,
                UpdatedOn = itemLocation.UpdatedOn
            };
        }

        public List<ItemLocationResponseVM> ToViewModelList(List<MItemLocation> itemLocationList)
        {
            List<ItemLocationResponseVM> result = new List<ItemLocationResponseVM>();
            foreach (var itemLocation in itemLocationList) {
                result.Add(ToViewModel(itemLocation));
    
            };
            return result;
        }
    }
}
