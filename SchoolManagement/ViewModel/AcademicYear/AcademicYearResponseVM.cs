using NeuroPi.CommonLib.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.ViewModel.AcademicYear
{
    public class AcademicYearResponseVM : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("contact")]
        public string Contact { get; set; }

        [Column("start_date")]
        public DateOnly StartDate { get; set; }

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
