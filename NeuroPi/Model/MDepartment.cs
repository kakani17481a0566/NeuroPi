using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NeuroPi.Model;

namespace NeuroPi.Models
{
    [Table("departments")]
    public class MDepartment : MBaseModel
    {
        [Key]
        [Column("department_id")]
        public int DepartmentId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Column("head_user_id")]
        [ForeignKey("HeadUser")]
        public int? HeadUserId { get; set; }

        [Column("organization_id")]
        [ForeignKey("Organization")]
        public int OrganizationId { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        // Navigation properties
        public virtual MUser HeadUser { get; set; }
        public virtual MOrganization Organization { get; set; }
        public virtual MTenant Tenant { get; set; }
        public virtual ICollection<MUserDepartment> UserDepartments { get; set; } = new List<MUserDepartment>();
    }
}
