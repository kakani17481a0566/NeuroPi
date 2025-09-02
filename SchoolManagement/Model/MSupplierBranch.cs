using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("SupplierBranch")]
    public class MSupplierBranch
    {
        [Required]
        [Column("Id")]
        public int Id { get; set; }
        [Required]
        [Column("Supplier_id")]
        [ForeignKey("Supplier")]
        public int Supplier_id { get; set; }
        [Required]
        [Column("Branch_id")]
        [ForeignKey("Branch")]
        public int Branch_id { get; set; }
        [Required]
        [Column("Tenant_id")]
        [ForeignKey("Tenant")]
        public int Tenant_id { get; set; }
        [Required]
        [Column("Created_on")]
        public DateTime Created_On { get; set; }
        [Required]
        [Column("Created_by")]
        [ForeignKey("users")]
        public int Created_by { get; set; }
        [Required]
        [Column("Updated_on")]
        public DateTime Updated_on { get; set; }
        [Required]
        [Column("Updated_by")]
        [ForeignKey("users")]
        public int Updated_by { get; set; }
        [Required]
        [Column("is_delete")]
        public bool is_delete { get; set; }



    }
}
