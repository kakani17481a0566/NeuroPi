using NeuroPi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NeuroPi.Model
{
    [Table("users")]
    public class MUser : MBaseModel
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("username")]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Column("middle_name")]
        public string? MiddleName { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("last_name")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("password")]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("email")]
        public string Email { get; set; }

        [MaxLength(20)]
        [Column("mobile_number")]
        public string? MobileNumber { get; set; }

        [MaxLength(20)]
        [Column("alternate_number")]
        public string? AlternateNumber { get; set; }

        [Column("dob")]
        public DateTime? DateOfBirth { get; set; }

        [Column("address")]
        public string? Address { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        // Navigation properties
        public virtual MTenant Tenant { get; set; }

        public virtual ICollection<MUserRole> UserRoles { get; set; } = new List<MUserRole>();
        public virtual ICollection<MTeamUser> TeamUsers { get; set; } = new List<MTeamUser>();
        public virtual ICollection<MGroupUser> GroupUsers { get; set; } = new List<MGroupUser>();
        public virtual ICollection<MUserDepartment> UserDepartments { get; set; } = new List<MUserDepartment>();
    }
}
