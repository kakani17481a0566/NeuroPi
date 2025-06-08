using SchoolManagement.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.ViewModel.TimeTable
{
    public class TimeTableUpdateVM
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int? WeekId { get; set; }
        public int? HolidayId { get; set; }

        public string Status { get; set; }

  
        public int? CourseId { get; set; }
  
        public int UpdatedBy { get; set; }
    }
}
