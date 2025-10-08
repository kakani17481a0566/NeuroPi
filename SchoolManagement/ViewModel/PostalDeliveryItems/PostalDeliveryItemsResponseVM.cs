using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.PostalDeliveryItems
{
    public class PostalDeliveryItemsResponseVM
    {
        public int Id { get; set; }
        public int DeliveryId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public int TenantId { get; set; }

        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }


        public static PostalDeliveryItemsResponseVM ToViewModel(MPostalDeliveryItems model)
        {
            return new PostalDeliveryItemsResponseVM
            {
                Id = model.Id,
                DeliveryId = model.DeliveryId,
                ItemId = model.Items.Id,
                ItemName = model.Items.Name,
                Quantity = model.Quantity,
                UnitCost = model.UnitCost,
                TenantId = model.TenantId,
                CreatedOn = model.CreatedOn,
                CreatedBy = model.CreatedBy,
                UpdatedOn = model.UpdatedOn,
                UpdatedBy = model.UpdatedBy
            };
        }

        public List<PostalDeliveryItemsResponseVM> ToViewModelList(List<MPostalDeliveryItems> models)
        {
            List<PostalDeliveryItemsResponseVM> viewModels = new List<PostalDeliveryItemsResponseVM>();
            foreach (var model in models)
            {
                viewModels.Add(ToViewModel(model));
            }
            return viewModels;
        }
    }
}
