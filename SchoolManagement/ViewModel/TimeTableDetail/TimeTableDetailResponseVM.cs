namespace SchoolManagement.ViewModel.TimeTableDetail
{
    public class TimeTableDetailResponseVM
    {
        public int Id { get; set; }
        public int PeriodId { get; set; }
        public int SubjectId { get; set; }
        public int TimeTableId { get; set; }
        public int TeacherId { get; set; }
        public int TenantId { get; set; }

        public static TimeTableDetailResponseVM FromModel(SchoolManagement.Model.MTimeTableDetail model)
        {
            if (model == null) return null;
            return new TimeTableDetailResponseVM
            {
                Id = model.Id,
                PeriodId = model.PeriodId,
                SubjectId = model.SubjectId,
                TimeTableId = model.TimeTableId,
                TeacherId = model.TeacherId,
                TenantId = model.TenantId
            };
        }
    }
}
