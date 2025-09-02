using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("visitors")]
    public class MVisitor : MBaseModel

    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("name")]
        // Basic Information
        public string name { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("address")]

        public string address { get; set; }

        [Required]
        [MaxLength(10)]
        [Column("mobilenumber")]
        public string mobilenumber { get; set; }
       
        public DateTime in_time { get; set; }
        public DateTime out_time { get; set; }
        [Required]
        [MaxLength(100)]
        [Column("purpose")]
        public string purpose { get; set; }
        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
        //public int createdby { get; set; }

        // Navigation properties can be added if needed
    }
}
