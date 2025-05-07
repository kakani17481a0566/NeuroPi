using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NeuroPi.Models
{
    [Table("organizations")]
    public class MOrganization : MBaseModel
    {
        [Key]
        [Column("organization_id")]
        public int OrganizationId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Column("parent_id")]
        [ForeignKey("ParentOrganization")]
        public int? ParentId { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        // Navigation properties
        public virtual MOrganization ParentOrganization { get; set; }
        public virtual MTenant Tenant { get; set; }
        public virtual ICollection<MDepartment> Departments { get; set; } = new List<MDepartment>();
    }
}
