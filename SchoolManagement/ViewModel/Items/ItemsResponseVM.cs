using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Items
{
    public class ItemsResponseVM
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int Depth { get; set; }

        public string Name { get; set; }

        public int TenantId { get; set; }
        public int createdBy { get; set; }
        public DateTime createdOn { get; set; }

        public int? updatedBy { get; set; }
        public DateTime? updatedOn { get; set; }


        public static ItemsResponseVM ToViewModel(MItems items)
        {
            return new ItemsResponseVM
            {
                Id = items.Id,
                CategoryId = items.CategoryId,
                Height = items.Height,
                Width = items.Width,
                Depth = items.Depth,
                Name = items.Name,
                TenantId = items.TenantId,
                createdBy = items.CreatedBy,
                createdOn = items.CreatedOn,
                updatedBy = items.UpdatedBy,
                updatedOn = items.UpdatedOn
            };
        }

        public List<ItemsResponseVM> ToViewModelList(List<MItems> itemsList)
        {
            List<ItemsResponseVM> result = new List<ItemsResponseVM>();
            foreach (var items in itemsList)
            {
                result.Add(ToViewModel(items));
            }
            return result;
        }


    }
}
