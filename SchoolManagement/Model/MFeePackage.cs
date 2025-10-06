using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("fee_packages")]
    public class MFeePackage : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

       
        [Required]
        [Column("fee_structure_id")]
        public int FeeStructureId { get; set; }

        [Required]
        [Column("branch_id")]
        public int BranchId { get; set; }

        [Required]
        [Column("course_id")]
        public int CourseId { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [Column("tax_id")]
        public int? TaxId { get; set; }

        [Required]
        [Column("payment_period")]
        [MaxLength(50)]
        public string PaymentPeriod { get; set; }

       
        [ForeignKey("FeeStructureId")]
        public virtual MFeeStructure FeeStructure { get; set; }

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
    }
}
