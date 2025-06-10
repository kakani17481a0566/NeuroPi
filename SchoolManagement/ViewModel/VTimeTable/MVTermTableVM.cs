using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.ViewModel.VTimeTable
{
    public class MVTermTableVM
    {
        public int? TermId { get; set; }
        public string? TermName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? TenantId { get; set; }
        public DateTime? Date { get; set; }
        public string? DayName { get; set; }
        public int? TimeTableId { get; set; }
        public int? HolidayId { get; set; }
        public string? Status { get; set; }
        public int? CourseId { get; set; }
        public string? CourseName { get; set; }
        public int? TimeTableDetailId { get; set; }
        public int? PeriodId { get; set; }
        public string? PeriodName { get; set; }
        public int? SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? SubjectCode { get; set; }
        public int? TimeTableTopicId { get; set; }
        public int? TopicId { get; set; }

        public int? WeekId { get; set; }

     
        public string? WeekName { get; set; }

        public DateTime? WeekStart { get; set; }

       
        public DateTime? WeekEnd { get; set; }
        public string? TopicName { get; set; }
        public string? TopicDescription { get; set; }
        public string? TopicTypeName { get; set; }
        public string? TopicTypeCode { get; set; }

       
    }
}
