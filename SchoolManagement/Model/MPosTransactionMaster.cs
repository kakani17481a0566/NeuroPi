using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Model
{

    [Table("pos_transaction_master")]
    public class MPosTransactionMaster : MBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("student_id")]
        [ForeignKey("student")]
        public int StudentId { get; set; }

        [Column("date")]
        public DateOnly Date { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        // ✅ Normalized Transaction Mode
        [Column("transaction_mode_id")]
        public int? TransactionModeId { get; set; }

        [ForeignKey("TransactionModeId")]
        public virtual MMaster? TransactionMode { get; set; }

        // ----------------------------------------------------------------------
        // 🔗 Relationships
        // ----------------------------------------------------------------------
        public virtual MStudent student { get; set; }
        public virtual MStudent Tenant { get; set; } // Note: Type seems wrong in original (MStudent?), usually should be MTenant but keeping mostly as is to avoid break
    }
}
