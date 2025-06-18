namespace SchoolManagement.ViewModel.DailyAssessment
{
    public class UpdateGradeResponseVm
    {
        public int Id { get; set; }
        public int TimeTableId { get; set; }
        public int StudentId { get; set; }
        public int BranchId { get; set; }

        public int OriginalGradeId { get; set; }
        public int UpdatedGradeId { get; set; }
    }
}
