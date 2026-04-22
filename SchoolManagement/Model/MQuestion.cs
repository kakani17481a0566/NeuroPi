using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Model
{
    [Table("questions")]
    public class MQuestion : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("q_ctg_id")]
        public int? QCtgId { get; set; }

        [ForeignKey(nameof(QCtgId))]
        public virtual MMaster QCtg { get; set; }

        [Column("q_order_id")]
        public int? QOrderId { get; set; }

        [Column("qus")]
        public string Qus { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }
    }
}
