using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.TimeTableAssessment
{
    public class TimeTableAssessmentRequestVM
    {
        public int TimeTableId { get; set; }
        public int AssessmentId { get; set; }

        public string Status { get; set; }

        public int TenantId { get; set; }

        public int createdBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MTimeTableAssessment ToModel(TimeTableAssessmentRequestVM request)
        {
            return new MTimeTableAssessment()
            {
                TimeTableId = request.TimeTableId,
                AssessmentId = request.AssessmentId,
                Status = request.Status,
                TenantId = request.TenantId,
                CreatedBy = request.createdBy,
                CreatedOn = request.CreatedOn
            };
        }
    }
}
