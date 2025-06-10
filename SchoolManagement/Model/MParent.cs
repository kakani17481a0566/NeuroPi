using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("parents")]
    public class MParent : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("user_id")]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual MUser User { get; set; }

        [Required]
        [Column("tenant_id")]
        [ForeignKey(nameof(Tenant))]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
