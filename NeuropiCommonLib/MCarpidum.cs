using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.CommonLib.Model
{
    [Table("carpidum", Schema = "nutrition")]
    public class MCarpidum : MBaseModel
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("student_id")]
        public int StudentId { get; set; }

        [Required]
        [Column("parent_type")]
        [MaxLength(20)]
        public string ParentType { get; set; } = default!; 

        [Column("guardian_name")]
        [MaxLength(100)]
        public string? GuardianName { get; set; }

        [Required]
        [Column("qr_code")]
        [MaxLength(255)]
        public string QrCode { get; set; } = default!;

        [Column("email")]
        [MaxLength(255)]
        public string? Email { get; set; }

        [Column("mobile_number")]
        [MaxLength(20)]
        public string? MobileNumber { get; set; }
        
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [NotMapped]
        public string? StudentName { get; set; }

        [NotMapped]
        public string? Gender { get; set; }

        [NotMapped]
        public string? CourseName { get; set; }

        [NotMapped]
        public string? BranchName { get; set; }

        [NotMapped]
        public string? Batch { get; set; }

        // MBaseModel has CreatedOn/UpdatedOn.
        // We need to map them to created_at/updated_at.
        // Since MBaseModel is in this same library, we can't easily shadow properly without knowing if MBaseModel virtuals them.
        // MBaseModel is abstract but props are not virtual.
        // Use 'new' keyword to shadow.
        
        [Column("created_at")]
        public new DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public new DateTime? UpdatedOn { get; set; }
    }
}
