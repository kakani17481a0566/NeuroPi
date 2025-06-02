using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    public class MTimeTableWorksheet : MBaseModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("TimeTable")]
        public int TimeTableId { get; set; }
        public virtual MTimeTable TimeTable { get; set; }

        [ForeignKey("Worksheet")]
        public int WorksheetId { get; set; }
        public virtual MWorksheet Worksheet { get; set; }

        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
