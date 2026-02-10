using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.CommonLib.Model;

namespace NeuropiForms.Models
{
    [Table("complintens_status")]
    public class MComplintensStatus : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("complintens_id")]
        public int? ComplintensId { get; set; }

        [ForeignKey("ComplintensId")]
        public virtual MComplintensMaster MComplintensMaster { get; set; }

        [Column("status_name")]
        public string StatusName { get; set; }

        [Column("min")]
        public int? Min { get; set; }

        [Column("max")]
        public int? Max { get; set; }

        [Column("col_code")]
        public string ColCode { get; set; }

        [Column("app_id")]
        public int? AppId { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

    }
}
