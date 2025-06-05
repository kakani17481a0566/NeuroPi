namespace SchoolManagement.ViewModel.DailyAssessment
{
    public class DailyAssessmentUpdateVm
    {
        public DateTime AssessmentDate { get; set; }
        public int TimeTableId { get; set; }
        public int WorksheetId { get; set; }
        public int GradeId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int ConductedById { get; set; }
        public int UpdatedBy { get; set; }
    }
}
