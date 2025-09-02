using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.ItemCategory
{
    public class ItemCategoryResponseVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public static ItemCategoryResponseVM ToViewModel(MItemCategory itemCategory)
        {
            return new ItemCategoryResponseVM
            {
                Id = itemCategory.Id,
                Name = itemCategory.Name,
                TenantId = itemCategory.TenantId,
                CreatedBy = itemCategory.CreatedBy,
                CreatedOn = itemCategory.CreatedOn,
                UpdatedBy = itemCategory.UpdatedBy,
                UpdatedOn = itemCategory.UpdatedOn,
            };

        }

        public List<ItemCategoryResponseVM> ToViewModelList(List<MItemCategory> itemCategoryList)
        {
            List<ItemCategoryResponseVM> result = new List<ItemCategoryResponseVM>();
            foreach (var itemCategory in itemCategoryList)
            {
                result.Add(ToViewModel(itemCategory));
            }
            return result;
        }
    }

   
}

