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
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }


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
                CreatedBy = items.CreatedBy,
                CreatedOn = items.CreatedOn,
                UpdatedBy = items.UpdatedBy,
                UpdatedOn = items.UpdatedOn
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
