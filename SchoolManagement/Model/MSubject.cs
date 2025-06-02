using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("subjects")] // Optional, but useful if your table name differs from class name
    public class MSubject : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Subject name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [Column("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Subject code is required.")]
        [StringLength(20, ErrorMessage = "Code cannot exceed 20 characters.")]
        [Column("code")]
        public string Code { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Required]
        [ForeignKey("Tenant")]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        public virtual MTenant Tenant { get; set; }
    }
}
