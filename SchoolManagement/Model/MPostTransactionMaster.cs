using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{

    [Table("pos_transaction_master")]
    public class MPostTransactionMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column ("student_id")]
        [ForeignKey("student")]
        public int StudentId { get; set; }
        [Column ("date")]

        public DateOnly Date { get; set; }
        [Column ("tenant_id")]
        [ForeignKey("Tenant")]

        public int TenantId { get; set; }
        public virtual MStudent student { get; set; }
        public virtual MStudent Tenant { get; set; }

    }
}
