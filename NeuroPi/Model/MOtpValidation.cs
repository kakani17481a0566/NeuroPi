using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.UserManagment.Model
{
    [Table("otp_validations")]
    public class MOtpValidation : MBaseModel
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        [Column("email")]
        public string Email { get; set; } = default!;

        [Required, MaxLength(10)]
        [Column("otp")]
        public string Otp { get; set; } = default!;

        [Required]
        [Column("expires_at")]
        public DateTime ExpiresAt { get; set; }

        [Column("is_used")]
        public bool IsUsed { get; set; } = false;
        
        [Column("tenant_id")]
        public int TenantId { get; set; }
    }
}
