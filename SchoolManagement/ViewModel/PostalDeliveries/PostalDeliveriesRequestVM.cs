using SchoolManagement.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.ViewModel.PostalDeliveries
{
    public class PostalDeliveriesRequestVM
    {
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


        public MPostalDeliveries ToModel()
        {
            return new MPostalDeliveries
            {
                FromBranchId = this.FromBranchId,
                ToBranchId = this.ToBranchId,
                ExpectedAt = this.ExpectedAt,
                Title = this.Title,
                Description = this.Description,
                SenderId = this.SenderId,
                ReceiverId = this.ReceiverId,
                ActualReceiver = this.ActualReceiver,
                StatusId = this.StatusId,
                SentAt = this.SentAt,
                DeliveredAt = this.DeliveredAt,
                TenantId = this.TenantId,
                CreatedBy = this.CreatedBy,
                CreatedOn = this.CreatedOn,
                UpdatedBy = this.UpdatedBy,
                UpdatedOn = this.UpdatedOn
            };
        }


    }
}
