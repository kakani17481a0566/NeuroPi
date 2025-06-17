using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.DailyAssessment
{
    public class DailyAssessmentResponseVm
    {
        public int Id { get; set; }
        public DateTime AssessmentDate { get; set; }
        public int TimeTableId { get; set; }
        public int WorksheetId { get; set; }
        public int? GradeId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int ConductedById { get; set; }
        public int TenantId { get; set; }

        public int? BranchId { get; set; }
        public static DailyAssessmentResponseVm FromModel(MDailyAssessment model)
        {
            return new DailyAssessmentResponseVm
            {
                Id = model.Id,
                AssessmentDate = model.AssessmentDate,
                TimeTableId = model.TimeTableId,
                //WorksheetId = model.WorksheetId,
                GradeId = model.GradeId,
                StudentId = model.StudentId,
                BranchId = model.BranchId,
                //CourseId = model.CourseId,
                ConductedById = model.ConductedById,
                TenantId = model.TenantId
            };
        }
    }
}
