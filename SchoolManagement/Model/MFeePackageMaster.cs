using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("fee_package_master")]
    public class MFeePackageMaster : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("branch_id")]
        public int BranchId { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [Required]
        [Column("course_id")]
        public int CourseId { get; set; }

        // 🔹 Navigation Properties
        [ForeignKey("BranchId")]
        public virtual MBranch Branch { get; set; }

        [ForeignKey("CourseId")]
        public virtual MCourse Course { get; set; }

        [ForeignKey("TenantId")]
        public virtual MTenant Tenant { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual MUser CreatedByUser { get; set; }

        [ForeignKey("UpdatedBy")]
        public virtual MUser UpdatedByUser { get; set; }

        // 🔹 Reverse Navigation: one Master can have many FeePackages
        public virtual ICollection<MFeePackage> FeePackages { get; set; }
    }
}
