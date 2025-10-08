using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.PostalDeliveryItems;

namespace SchoolManagement.Services.Implementation
{
    public class PostalDeliveryItemsServiceImpl : IPostalDeliveryItemsService
    {
        private readonly SchoolManagementDb _context;

        public PostalDeliveryItemsServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public string CreatepostalDeliveryItems(PostalDeliveryItemsRequestVM pDIRequestVM)
        {
            var postalDeliveryItems = new MPostalDeliveryItems
            {
                DeliveryId = pDIRequestVM.DeliveryId,
                ItemId = pDIRequestVM.ItemId,
                ItemName = pDIRequestVM.ItemName,
                Quantity = pDIRequestVM.Quantity,
                UnitCost = pDIRequestVM.UnitCost,
                TenantId = pDIRequestVM.TenantId,
                CreatedBy = pDIRequestVM.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };
            _context.PostalDeliveryItems.Add(postalDeliveryItems);
            _context.SaveChanges();
            return "created";
        }

        public List<PostalDeliveryItemsResponseVM> GetAll()
        {
           return _context.PostalDeliveryItems.Include(e=>e.Items)
                .Select(pdi => PostalDeliveryItemsResponseVM.ToViewModel(pdi))
                .ToList();
        }
    }
}
