// SchoolManagement.Model/MItemSupplier.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("item_suppliers")] 
    public class MItemSupplier : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("item_id")]
        public int ItemId { get; set; }

        [Required]
        [Column("branch_id")]
        public int BranchId { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [Column("adt")]
        public int? Adt { get; set; }  // ✅ fixed type to match DB (nullable int)
    }
}
