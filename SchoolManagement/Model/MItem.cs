using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    public class MItem : MBaseModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ItemHeader")]
        public int ItemHeaderId { get; set; }

        public string BookCondition { get; set; }
        public string Status { get; set; }
        public DateTime? PurchasedOn { get; set; }

        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        public virtual MTenant Tenant { get; set; }
    }
}
