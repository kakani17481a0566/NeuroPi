using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Items
{
    public class ItemsRequestVM
    {

        public int CategoryId { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int Depth { get; set; }

        public string Name { get; set; }

        public int TenantId { get; set; }
        public int createdBy { get; set; }

        


        public MItems ToModel()
        {
            return new MItems
            {
               
                CategoryId = this.CategoryId,
                Height = this.Height,
                Width = this.Width,
                Depth = this.Depth,
                Name = this.Name,
                TenantId = this.TenantId,
                CreatedBy = this.createdBy
                
            };
        }
    }
}
