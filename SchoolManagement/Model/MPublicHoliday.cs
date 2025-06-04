using NeuroPi.UserManagment.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("public_holiday")]
    public class MPublicHoliday : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("date")]
        public DateTime Date { get; set; }

        [ForeignKey("Tenant")]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        public virtual MTenant Tenant { get; set; }
    }
}
