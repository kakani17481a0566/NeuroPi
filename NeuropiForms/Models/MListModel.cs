using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.CommonLib.Model;

namespace NeuropiForms.Models
{
    [Table("list")]
    public class MListModel : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("query")]
        public string Query { get; set; }

        [Column("app_id")]
        public int? AppId { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

    }
}
