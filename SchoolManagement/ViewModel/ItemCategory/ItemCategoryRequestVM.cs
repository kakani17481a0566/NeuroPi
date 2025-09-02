using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.ItemCategory
{
    public class ItemCategoryRequestVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public  int TenantId { get; set; }

        public int createdBy { get; set; }

        public int? updatedBy { get; set; }

        public MItemCategory ToModel()
        {
            return new MItemCategory
            {
                Id = this.Id,
                Name = this.Name,
                TenantId = this.TenantId,
                CreatedBy = this.createdBy,
                CreatedOn = DateTime.UtcNow,
                UpdatedBy = this.updatedBy,
                UpdatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    }
}
