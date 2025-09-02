// SchoolManagement.Model/MItemSupplier.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("item_supplier")]
    public class MItemSupplier : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("item_id")]
        public int ItemId { get; set; }

        [Column("branch_id")]
        public int BranchId { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [Column("adt")]
        public string Adt { get; set; }

        [Column("is_delete")]
        public bool IsDeleted { get; set; }  // if your column is is_delete (snake_case), keep this property name or map with [Column]
    }
}

