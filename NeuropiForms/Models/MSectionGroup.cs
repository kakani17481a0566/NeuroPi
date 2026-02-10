using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.CommonLib.Model;

namespace NeuropiForms.Models
{
    [Table("section_group")]
    public class MSectionGroup : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("group_id")]
        public int? GroupId { get; set; }

        [ForeignKey("GroupId")]
        public virtual MGroupModel MGroup { get; set; }

        [Column("section_id")]
        public int? SectionId { get; set; }

        [ForeignKey("SectionId")]
        public virtual MSection MSection { get; set; }

        [Column("app_id")]
        public int? AppId { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

    }
}
