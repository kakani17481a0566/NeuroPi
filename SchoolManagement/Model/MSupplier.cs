using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("supplier")]
    public class MSupplier :MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]

        public string Name { get; set; }

        [Required]
        [Column("contact_id")]
        [ForeignKey("Contact")]
        public int Contact_id { get; set; }
       

        [Required]
        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int Tenant_id { get; set; }


        public virtual MContact Contact { get; set; }





    }
}
