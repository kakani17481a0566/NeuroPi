using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.PostalDeliveries;

namespace SchoolManagement.Services.Implementation
{
    public class PostalDeliveriesServiceImpl : IPostalDeliveriesService
    {
        private readonly SchoolManagementDb _context;
        public PostalDeliveriesServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public PostalDeliveriesResponseVM CreatePostalDelivery(PostalDeliveriesRequestVM pdRequestVM)
        {
            var newPd = new MPostalDeliveries
            {
                FromBranchId = pdRequestVM.FromBranchId,
                ToBranchId = pdRequestVM.ToBranchId,
                ExpectedAt = pdRequestVM.ExpectedAt,
                Title = pdRequestVM.Title,
                Description = pdRequestVM.Description,
                SenderId = pdRequestVM.SenderId,
                ReceiverId = pdRequestVM.ReceiverId,
                ActualReceiver = pdRequestVM.ActualReceiver,
                StatusId = pdRequestVM.StatusId,
                SentAt = pdRequestVM.SentAt,
                DeliveredAt = pdRequestVM.DeliveredAt,
                TenantId = pdRequestVM.TenantId,
                CreatedBy = pdRequestVM.CreatedBy,
                CreatedOn = DateTime.UtcNow

            };
            _context.PostalDeliveries.Add(newPd);
            _context.SaveChanges();
            return PostalDeliveriesResponseVM.ToViewModel(newPd);
        }

        public List<PostalDeliveriesResponseVM> GetAll()
        {
            return _context.PostalDeliveries
                .Select(pd => PostalDeliveriesResponseVM.ToViewModel(pd))
                .ToList();
        }
    }
}
