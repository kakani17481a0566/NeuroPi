using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.CommonLib.Model;

namespace NeuropiForms.Models
{
    [Table("soap_services")]
    public class MSoapService : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("base_url")]
        public string BaseUrl { get; set; }

        [Column("token_id")]
        public string TokenId { get; set; }

        [Column("parameters", TypeName = "jsonb")]
        public string Parameters { get; set; }

        [Column("response", TypeName = "jsonb")]
        public string Response { get; set; }

        [Column("token_url_id")]
        public string TokenUrlId { get; set; }

        [Column("app_id")]
        public int? AppId { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

    }
}
