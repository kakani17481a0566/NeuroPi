using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("academic_year")]
    public class MAcademicYear : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("contact")]
        public string Contact { get; set; }

        [Column("start_date")]
        public DateOnly start_date { get; set; }

        [Column("end_date")]
        public DateOnly EndDate { get; set; }


        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }

    }

}
