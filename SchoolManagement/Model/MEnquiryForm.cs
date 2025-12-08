
using NeuroPi.UserManagment.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("enquiry_form")]
    public class MEnquiryForm : MBaseModel
    {
        [Key]
        [Column("uuid")]
        public Guid Uuid { get; set; } = Guid.NewGuid();

        [Required]
        [Column("company_name")]
        [StringLength(255)]
        public string CompanyName { get; set; }

        [Required]
        [Column("contact_person")]
        [StringLength(255)]
        public string ContactPerson { get; set; }

        [Column("contact_number")]
        [StringLength(20)]
        public string? ContactNumber { get; set; }

        [Required]
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; }

        [Column("is_agreed")]
        public bool IsAgreed { get; set; }

        [Column("digital_signature", TypeName = "text")]
        public string? DigitalSignature { get; set; }

        [Column("agreed_on")]
        public DateTime? AgreedOn { get; set; } = DateTime.UtcNow;

        [Column("tenant_id")]
        public int TenantId { get; set; }
    }
}
