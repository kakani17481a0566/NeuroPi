using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.UserManagment.Model
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
        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual MOrganization ParentOrganization { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }

        public virtual ICollection<MDepartment> Departments { get; set; } = new List<MDepartment>();
    }
}
