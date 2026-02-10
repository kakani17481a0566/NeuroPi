using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.CommonLib.Model;

namespace NeuropiForms.Models
{
    [Table("database_resource")]
    public class MDatabaseResource : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("databse_id")]
        public string DatabaseId { get; set; }

        [Column("serverr_id")]
        public string ServerId { get; set; }

        [Column("portid")]
        public int? PortId { get; set; }

        [Column("user")]
        public string User { get; set; }

        [Column("pwd")]
        public string Pwd { get; set; }

        [Column("databse_type")]
        public string DatabaseType { get; set; }

        [Column("app_id")]
        public int? AppId { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

    }
}
