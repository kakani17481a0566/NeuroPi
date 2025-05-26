using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.UserManagment.Model
{
    [Table("transactions")]
    public class MTransaction : MBaseModel
    {
        [Key]
        [Column("trx_id")]
        public int TrxId { get; set; }

        [Column("acc_id")]
        public int AccId { get; set; }

        [Column("trx_type_id")]
        public int? TrxTypeId { get; set; }

        [Column("trx_mode_id")]
        public int? TrxModeId { get; set; }

        [Column("trx_desc")]
        public string TrxDesc { get; set; }

        [Column("trx_amount")]
        public float? TrxAmount { get; set; }

        [Column("acc_head_id")]
        public int? AccHeadId { get; set; }

        [Column("trx_status")]
        public int? TrxStatus { get; set; }

        [Column("ref_trns_id")]
        public string RefTrnsId { get; set; }

        [Column("modifyed_by")]
        public int? ModifiedBy { get; set; }

        [Column("modifyed_on")]
        public DateTime? ModifiedOn { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }
    }
}
