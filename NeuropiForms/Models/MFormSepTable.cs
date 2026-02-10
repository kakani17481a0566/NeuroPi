using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.CommonLib.Model;

namespace NeuropiForms.Models
{
    [Table("form_sep_table")]
    public class MFormSepTable : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("form_id")]
        public int? FormId { get; set; }

        [ForeignKey("FormId")]
        public virtual MForm MForm { get; set; }

        [Column("db_server_id")]
        public int? DbServerId { get; set; }

        [Column("server_name")]
        public string ServerName { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("use_pwd")]
        public bool? UsePwd { get; set; }

        [Column("pwd")]
        public string Pwd { get; set; }

        [Column("app_id")]
        public int? AppId { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

    }
}
