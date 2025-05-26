using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    public class MLibBookRecord : MBaseModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }

        public string IssuedTo { get; set; }
        public DateTime? IssuedOn { get; set; }
        public string IssuedBy { get; set; }
        public string Status { get; set; }

        [ForeignKey("BookCondition")]
        public int? BookConditionId { get; set; }

        [ForeignKey("ReturnBookCondition")]
        public int? ReturnBookConditionId { get; set; }

        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
    }
}
