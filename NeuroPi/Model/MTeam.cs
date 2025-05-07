using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Models
{
    [Table("teams")]
    public class MTeam : MBaseModel
    {
        [Key]
        [Column("team_id")]
        public int TeamId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        // Navigation properties
        public virtual MTenant Tenant { get; set; }
        public virtual ICollection<MTeamUser> TeamUsers { get; set; } = new List<MTeamUser>();
    }
}
