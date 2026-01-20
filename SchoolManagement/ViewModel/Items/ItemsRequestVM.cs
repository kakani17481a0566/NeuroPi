using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Items
{
    public class ItemsRequestVM
    {
        // 🔹 Core item fields
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int TenantId { get; set; }

        public int? Height { get; set; }
        public int? Width { get; set; }
        public int? Depth { get; set; }

        public string? Description { get; set; }
        public string? ItemCode { get; set; }
        
        // 🔹 File Upload
        public Microsoft.AspNetCore.Http.IFormFile? Image { get; set; }

        // 🔹 Group flag
        public bool IsGroup { get; set; } = false;

        // 🔹 Audit fields
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public bool IsDeleted { get; set; } = false;

        // 🔹 Mapper
        public MItems ToModel()
        {
            return new MItems
            {
                Name = this.Name,
                CategoryId = this.CategoryId,
                TenantId = this.TenantId,
                Height = this.Height ?? 0,
                Width = this.Width ?? 0,
                Depth = this.Depth ?? 0,
                Description = this.Description,
                ItemCode = this.ItemCode,
                IsGroup = this.IsGroup,
                CreatedBy = this.CreatedBy,
                CreatedOn = this.CreatedOn,
                UpdatedBy = this.UpdatedBy,
                UpdatedOn = this.UpdatedOn,
                IsDeleted = this.IsDeleted
            };
        }
    }
}
