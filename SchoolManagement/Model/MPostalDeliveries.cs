using NeuroPi.UserManagment.Model;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table ("postal_deliveries")]
    public class MPostalDeliveries : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("from_branch_id")]
        public int FromBranchId { get; set; }

        [Column("to_branch_id")]
        public int ToBranchId { get; set; }

        [Column("expected_at")]
        public DateTime ExpectedAt { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("sender_id")]
        public int SenderId { get; set; }


        [Column("receiver_id")]
        public int ReceiverId { get; set; }

        [Column("actual_receiver")]
        public string ActualReceiver { get; set; }

        [Column("status_id")]
        public int StatusId { get; set; }

        [Column("sent_at")]
        public DateTime SentAt { get; set; }

        [Column("delivered_at")]
        public DateTime DeliveredAt { get; set; }

        [ForeignKey("Tenant")]
        [Column("tenant_id")]
        public int TenantId { get; set; }


        public virtual MTenant Tenant { get; set; }



    }
}
