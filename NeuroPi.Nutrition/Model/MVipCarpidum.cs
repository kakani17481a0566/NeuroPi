using NeuroPi.CommonLib.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Nutrition.Model
{
    [Table("m_vip_carpidum", Schema = "nutrition")]
    public class MVipCarpidum : MBaseModel
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("vip_name")]
        public string VipName { get; set; } = string.Empty;

        [Column("vip_email")]
        public string VipEmail { get; set; } = string.Empty;

        [Column("vip_phone")]
        public string? VipPhone { get; set; }

        [Column("qr_code")]
        public Guid QrCode { get; set; }

        [Column("pass_count")]
        public int PassCount { get; set; }
    }
}
