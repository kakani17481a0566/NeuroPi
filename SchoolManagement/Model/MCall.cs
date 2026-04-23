using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Model
{
    [Table("calls")]
    public class MCall : NeuroPi.CommonLib.Model.MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("contact_id")]
        public int ContactId { get; set; }

        [ForeignKey(nameof(ContactId))]
        public virtual MEmployeeDetail Contact { get; set; }

        [Column("stage_id")]
        public int? StageId { get; set; }

        [ForeignKey(nameof(StageId))]
        public virtual MMaster Stage { get; set; }

        [Column("direction_type_id")]
        public int? DirectionTypeId { get; set; }

        [ForeignKey(nameof(DirectionTypeId))]
        public virtual MMaster DirectionTypeName { get; set; }
        [Column("call_status_id")]
        public int? CallStatusId { get; set; }

        [ForeignKey(nameof(CallStatusId))]
        public virtual MMaster CallStatusName { get; set; }


        [Column("audio_link")]
        public string AudioLink { get; set; }

        [Column("remarks")]
        public string Remarks { get; set; }

        [Column("call_duration")]
        public TimeSpan? CallDuration { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual NeuroPi.UserManagment.Model.MTenant Tenant { get; set; }
    }
}
