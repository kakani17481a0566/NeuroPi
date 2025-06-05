namespace SchoolManagement.ViewModel.TimeTableDetail
{
    public class TimeTableDetailRequestVM
    {
        public int PeriodId { get; set; }
        public int SubjectId { get; set; }
        public int TimeTableId { get; set; }
        public int TeacherId { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }

        public SchoolManagement.Model.MTimeTableDetail ToModel()
        {
            return new SchoolManagement.Model.MTimeTableDetail
            {
                PeriodId = this.PeriodId,
                SubjectId = this.SubjectId,
                TimeTableId = this.TimeTableId,
                TeacherId = this.TeacherId,
                TenantId = this.TenantId,
                CreatedBy = this.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    }
}
