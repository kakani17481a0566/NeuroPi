using NeuroPi.UserManagment.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("contact")]
    public class MContact : MBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; } 

        [Required]
        [Column("pri_number", TypeName = "varchar(20)")]
        public string PriNumber { get; set; } 

        [Column("sec_number", TypeName = "varchar(20)")]
        public string? SecNumber { get; set; }

        [Column("email", TypeName = "varchar(100)")]
        public string? Email { get; set; }

        [Required]
        [Column("address_1", TypeName = "varchar(200)")]
        public string Address1 { get; set; } 

        [Column("address_2", TypeName = "varchar(200)")]
        public string? Address2 { get; set; }

        [Column("state", TypeName = "varchar(50)")]
        public string? State { get; set; }

        [Required]
        [Column("city", TypeName = "varchar(50)")]
        public string City { get; set; } 

        [Column("pincode", TypeName = "varchar(20)")]
        public string? Pincode { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

       

        public virtual ICollection<MInstitution> Institutions { get; set; } = new List<MInstitution>();

        public virtual MTenant Tenant { get; set; }
    }
}