using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NeuroPi.Models
{
    [Table("config")]
    public class MConfig : MBaseModel
    {
        [Key]
        [Column("config_id")]
        public int ConfigId { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("db_type")]
        public string DbType { get; set; }

        [MaxLength(255)]
        [Column("connection_string")]
        public string? ConnectionString { get; set; }

        [MaxLength(100)]
        [Column("db_host")]
        public string? DbHost { get; set; }

        [Column("db_port")]
        public int? DbPort { get; set; }

        [MaxLength(100)]
        [Column("db_name")]
        public string? DbName { get; set; }

        [MaxLength(100)]
        [Column("db_username")]
        public string? DbUsername { get; set; }

        [MaxLength(255)]
        [Column("db_password")]
        public string? DbPassword { get; set; }

        // Navigation properties
        public virtual MTenant Tenant { get; set; }
    }
}
