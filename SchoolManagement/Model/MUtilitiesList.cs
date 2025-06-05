using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("utilities_list")]
    public class MUtilitiesList : MBaseModel
    {
        [Key]
    
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        // Navigation property
        public virtual MTenant Tenant { get; set; }
    }
}
