using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NeuroPi.Models
{
    [Table("teams")]
    public class MTeam : MBaseModel
    {
        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        // Navigation properties
        public virtual MTenant Tenant { get; set; }
        public virtual ICollection<MTeamUser> TeamUsers { get; set; }
    }
}