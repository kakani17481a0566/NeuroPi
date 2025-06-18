using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.ViewModel.DailyAssessment
{
    public class SubmitAssessmentGradesRequest
    {
        public int TenantId { get; set; }
        public int BranchId { get; set; }
        public int TimeTableId { get; set; }
        public int UpdatedBy { get; set; }

        public List<StudentAssessmentGrade> Grades { get; set; }
    }

    public class StudentAssessmentGrade
    {
        public int StudentId { get; set; }
        public int AssessmentId { get; set; }
        public int GradeId { get; set; }
    }


}
