using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("item")]  // map explicitly to your table name (lowercase)
    public class MItem : MBaseModel
    {
        [Key]
        [Column("id")]   
        public int Id { get; set; }

        [ForeignKey("ItemHeader")]
        [Column("item_header_id")]
        public int ItemHeaderId { get; set; }

        [Column("book_condition")]
        public string BookCondition { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("purchased_on")]
        public DateTime? PurchasedOn { get; set; }

        [ForeignKey("Tenant")]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        public virtual MTenant Tenant { get; set; }
    }
}
