using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("grade")]
    public class MGrade : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("min_percentage")]
        public decimal MinPercentage { get; set; }

        [Required]
        [Column("max_percentage")]
        public decimal MaxPercentage { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [ForeignKey("Tenant")]
        [Column("tenant_id")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
