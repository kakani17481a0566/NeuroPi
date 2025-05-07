using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NeuroPi.Model;

namespace NeuroPi.Models
{
    [Table("group_users")]
    public class MGroupUser : MBaseModel
    {
        [Key]
        [Column("group_user_id")]
        public int GroupUserId { get; set; }

        [Column("group_id")]
        [ForeignKey("Group")]
        public int GroupId { get; set; }

        [Column("user_id")]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        // Navigation properties
        public virtual MGroup Group { get; set; }
        public virtual MUser User { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
