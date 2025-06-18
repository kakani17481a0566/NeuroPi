namespace SchoolManagement.ViewModel.DailyAssessment
{
    public class UpdateGradeRequestVm
    {
        public int Id { get; set; }
        public int TimeTableId { get; set; }
        public int StudentId { get; set; }
        public int BranchId { get; set; }
        public int NewGradeId { get; set; }
    }
}
