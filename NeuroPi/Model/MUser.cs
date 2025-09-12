using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.UserManagment.Model
{
    [Table("users")]
    public class MUser : MBaseModel
    {
        [Key]
        [Column("user_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required, MaxLength(50)]
        [Column("username")]
        public string Username { get; set; } = default!;

        [Required, MaxLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; } = default!;

        [MaxLength(50)]
        [Column("middle_name")]
        public string? MiddleName { get; set; }

        [Required, MaxLength(50)]
        [Column("last_name")]
        public string LastName { get; set; } = default!;

        [Required, MaxLength(255)]
        [Column("password")]
        public string Password { get; set; } = default!;

        [Required, MaxLength(100)]
        [Column("email")]
        public string Email { get; set; } = default!;

        [MaxLength(20)]
        [Column("mobile_number")]
        public string? MobileNumber { get; set; }

        [MaxLength(20)]
        [Column("alternate_number")]
        public string? AlternateNumber { get; set; }

        [Column("dob", TypeName = "date")]
        public DateOnly? DateOfBirth { get; set; }

        [Column("address", TypeName = "text")]
        public string? Address { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [Column("user_image_url", TypeName = "text")]
        public string? UserImageUrl { get; set; }

        // ----------------------------
        // Extended profile info
        // ----------------------------
        [Column("father_name")]
        public string? FatherName { get; set; }

        [Column("mother_name")]
        public string? MotherName { get; set; }

        [Column("gender")]
        public string? Gender { get; set; }

        [Column("marital_status")]
        public string? MaritalStatus { get; set; }

        [Column("spouse_name")]
        public string? SpouseName { get; set; }

        [Column("wedding_anniversary_date", TypeName = "date")]
        public DateOnly? WeddingAnniversaryDate { get; set; }

        [Column("joining_date", TypeName = "date")]
        public DateOnly? JoiningDate { get; set; }

        [Column("working_start_time", TypeName = "time")]
        public TimeOnly? WorkingStartTime { get; set; }

        [Column("working_end_time", TypeName = "time")]
        public TimeOnly? WorkingEndTime { get; set; }

        [Column("role_type_id")]
        public int? RoleTypeId { get; set; }

        // ----------------------------
        // Navigation Properties
        // ----------------------------
        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; } = default!;

        [ForeignKey(nameof(CreatedBy))]
        public virtual MUser? CreatedByUser { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public virtual MUser? UpdatedByUser { get; set; }

        // Collections
        public virtual ICollection<MUserRole> UserRoles { get; set; } = new List<MUserRole>();
        public virtual ICollection<MTeamUser> TeamUsers { get; set; } = new List<MTeamUser>();
        public virtual ICollection<MGroupUser> GroupUsers { get; set; } = new List<MGroupUser>();
        public virtual ICollection<MUserDepartment> UserDepartments { get; set; } = new List<MUserDepartment>();
    }
}
