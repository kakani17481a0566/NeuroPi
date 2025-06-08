using NeuroPi.UserManagment.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("weeks")]
    public class MWeek : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("term_id")]
        [ForeignKey("Term")]
        public int TermId { get; set; } 
        public virtual MTerm Term { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        public virtual MTenant Tenant { get; set; }
    }
}
