using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("Supplier")]
    public class MSupplier :MBaseModel
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Required]
        [Column("Name")]

        public string Name { get; set; }

        [Required]
        [Column("Contact_id")]
        [ForeignKey("Contact")]
        public int Contact_id { get; set; }
       

        [Required]
        [Column("Tenant_id")]
        [ForeignKey("Tenant")]
        public int Tenant_id { get; set; }
        [Required]
        [Column("Created_on")]
        public DateTime Created_On { get; set; }
        [Required]
        [Column("Created_by")]
        [ForeignKey("users")]
        public int Created_by { get; set; }
        [Required]
        [Column("Updated_on")]
        public DateTime Updated_on { get; set; }
        [Required]
        [Column("Updated_by")]
        [ForeignKey("users")]
        public int Updated_by { get; set; }
        [Required]
        [Column("is_delete")]
        public bool is_delete { get; set; }

        public virtual MContact Contact { get; set; }



    }
}
