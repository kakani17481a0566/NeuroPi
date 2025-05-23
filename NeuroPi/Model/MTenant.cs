using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NeuroPi.UserManagment.Model
{
    [Table("tenants")]
    public class MTenant : MBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; } = string.Empty;

        

        public virtual ICollection<MUser> Users { get; set; } = new List<MUser>();


      



  }
}