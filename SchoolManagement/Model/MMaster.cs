using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("masters")]
    public class MMaster : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("masters_type")]
        public int MastersType { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

      
        [ForeignKey("TenantId")]
        public virtual MTenant Tenant { get; set; }
    }
}
