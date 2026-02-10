using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.CommonLib.Model;

namespace NeuropiForms.Models
{
    [Table("group")]
    public class MGroupModel : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("group_name")]
        public string GroupName { get; set; }

        [Column("display_name")]
        public string DisplayName { get; set; }

        [Column("no_of_col")]
        public int? NoOfCol { get; set; }

        [Column("app_id")]
        public int? AppId { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

    }
}
