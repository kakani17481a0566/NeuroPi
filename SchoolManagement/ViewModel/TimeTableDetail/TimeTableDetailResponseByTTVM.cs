namespace SchoolManagement.ViewModel.TimeTableDetail
{
    public class TimeTableDetailResponseByTTVM
    {
        public int Id { get; set; }

        public int PeriodId { get; set; }
        public string PeriodName { get; set; }

        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        public int TimeTableId { get; set; }
    }
}
