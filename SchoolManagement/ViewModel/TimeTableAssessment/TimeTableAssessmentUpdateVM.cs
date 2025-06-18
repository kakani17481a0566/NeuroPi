namespace SchoolManagement.ViewModel.TimeTableAssessment
{
    public class TimeTableAssessmentUpdateVM
    {
        public int TimeTableId { get; set; }
        public int AssessmentId { get; set; }

        public string Status { get; set; }

        public int TenantId { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
