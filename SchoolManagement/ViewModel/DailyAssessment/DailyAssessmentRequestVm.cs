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
        public int BranchId { get; set; }
        public MDailyAssessment ToModel(DailyAssessmentRequestVm requestVm)
        {
            return new MDailyAssessment
            {
                AssessmentDate = requestVm.AssessmentDate,
                TimeTableId = requestVm.TimeTableId,
                //WorksheetId = this.WorksheetId,
                GradeId = requestVm.GradeId,
                StudentId = requestVm.StudentId,
                BranchId = requestVm.BranchId,
                //CourseId = this.CourseId,
                ConductedById = requestVm.ConductedById,
                TenantId = requestVm.TenantId,
                CreatedBy = requestVm.CreatedBy
            };
        }
    }
}