using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NeuroPi.Model;

namespace NeuroPi.Models
{
    [Table("audit_logs")]
    public class MAuditLog : MBaseModel
    {
        [Key]
        [Column("audit_id")]
        public int AuditId { get; set; }

        [Column("user_id")]
        [ForeignKey("User")]
        public int? UserId { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("action")]
        public string Action { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("entity")]
        public string Entity { get; set; }

        [Column("record_id")]
        public int RecordId { get; set; }

        [Column("old_values")]
        public string OldValues { get; set; }

        [Column("new_values")]
        public string NewValues { get; set; }

        [Column("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        // Navigation properties
        public virtual MUser User { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
