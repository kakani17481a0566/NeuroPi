using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.UserManagment.Model
{
    [Table("team_users")]
    public class MTeamUser : MBaseModel
    {
        [Key]
        [Column("team_user_id")]
        public int TeamUserId { get; set; }

        [Column("team_id")]
        [ForeignKey("Team")]
        public int TeamId { get; set; }

        [Column("user_id")]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        // Navigation properties
        public virtual MTeam Team { get; set; }
        public virtual MUser User { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
