using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("fee_structure")]
    public class MFeeStructure : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Column("amount", TypeName = "decimal(12,2)")]
        public decimal Amount { get; set; }

     
        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

       
        [ForeignKey("TenantId")]
        public virtual MTenant Tenant { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual MUser CreatedByUser { get; set; }

        [ForeignKey("UpdatedBy")]
        public virtual MUser UpdatedByUser { get; set; }

  
        public virtual ICollection<MFeePackage> FeePackages { get; set; }
    }
}
