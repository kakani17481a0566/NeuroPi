using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    public class MTimeTable : MBaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("Week")]
        public int? WeekId { get; set; }
        public virtual MWeek Week { get; set; }

        [ForeignKey("PublicHoliday")]
        public int? HolidayId { get; set; }
        public virtual MPublicHoliday PublicHoliday { get; set; }

        [Required]
        public string Status { get; set; } // working or holiday

        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
