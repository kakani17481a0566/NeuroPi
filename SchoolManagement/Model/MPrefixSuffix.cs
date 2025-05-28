using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.UserManagment.Model
{
    [Table("prefix_suffix")]
    public class MPrefixSuffix : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("prefix")]
        public string? Prefix { get; set; } = null!;

        [Column("suffix")]
        public string? Suffix { get; set; }

        [Column("length")]
        public int Length { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        public virtual MTenant Tenant { get; set; }



    }
}
