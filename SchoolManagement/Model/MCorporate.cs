using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("corporate")]
    public class MCorporate : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [StringLength(255)]
        public string? Name { get; set; }

        [Column("contact_id")]
        public int? ContactId { get; set; }

        [Column("discount", TypeName = "numeric")]
        public decimal Discount { get; set; } = 0;

        [Column("tenant_id")]
        public int? TenantId { get; set; }

        // 🔹 Navigation Properties
        [ForeignKey("ContactId")]
        public virtual MContact? Contact { get; set; }

        [ForeignKey("TenantId")]
        public virtual MTenant? Tenant { get; set; }
    }
}
