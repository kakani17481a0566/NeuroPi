using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.DailyAssessment
{
    public class DailyAssessmentRequestVm
    {
        public DateTime AssessmentDate { get; set; }
        public int TimeTableId { get; set; }
        public int WorksheetId { get; set; }
        public int GradeId { get; set; }
        public int StudentId { get; set; }
        //public int CourseId { get; set; }
        public int ConductedById { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
        public int? BranchId { get; set; }

        public MDailyAssessment ToModel()
        {
            return new MDailyAssessment
            {
                AssessmentDate = this.AssessmentDate,
                TimeTableId = this.TimeTableId,
                WorksheetId = this.WorksheetId,
                GradeId = this.GradeId,
                StudentId = this.StudentId,
                BranchId = this.BranchId,
                //CourseId = this.CourseId,
                ConductedById = this.ConductedById,
                TenantId = this.TenantId,
                CreatedBy = this.CreatedBy
            };
        }
    }
}
