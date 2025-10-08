using SchoolManagement.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.ViewModel.PostalDeliveries
{
    public class PostalDeliveriesResponseVM
    {
        public int Id { get; set; }

        public int FromBranchId { get; set; }

        public int ToBranchId { get; set; }

        public DateTime ExpectedAt { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string ActualReceiver { get; set; }

        public int StatusId { get; set; }

        public DateTime SentAt { get; set; }

        public DateTime DeliveredAt { get; set; }

        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }


        public static PostalDeliveriesResponseVM ToViewModel(MPostalDeliveries pd)
        {

            return new PostalDeliveriesResponseVM
            {
                Id = pd.Id,
                FromBranchId = pd.FromBranchId,
                ToBranchId = pd.ToBranchId,
                ExpectedAt = pd.ExpectedAt,
                Title = pd.Title,
                Description = pd.Description,
                SenderId = pd.SenderId,
                ReceiverId = pd.ReceiverId,
                ActualReceiver = pd.ActualReceiver,
                StatusId = pd.StatusId,
                SentAt = pd.SentAt,
                DeliveredAt = pd.DeliveredAt,
                TenantId = pd.TenantId,
                CreatedOn = pd.CreatedOn,
                CreatedBy = pd.CreatedBy,
                UpdatedOn = pd.UpdatedOn,
                UpdatedBy = pd.UpdatedBy
            };


        }

        public List<PostalDeliveriesResponseVM> ToViewModelList(List<MPostalDeliveries> pdList)
        {
            List<PostalDeliveriesResponseVM> result = new List<PostalDeliveriesResponseVM>();

            foreach (var pd in pdList) 
            {
                result.Add(ToViewModel(pd));
            
            }
            return result;

        }

    }
}
