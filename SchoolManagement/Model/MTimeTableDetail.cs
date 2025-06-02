using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    public class MTimeTableDetail : MBaseModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Period")]
        public int PeriodId { get; set; }
        public virtual MPeriod Period { get; set; }

        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual MSubject Subject { get; set; }

        [ForeignKey("TimeTable")]
        public int TimeTableId { get; set; }
        public virtual MTimeTable TimeTable { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public virtual MUser Teacher { get; set; }

        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
