namespace SchoolManagement.ViewModel.TimeTable
{
    public class TimeTableRequestVM
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int? WeekId { get; set; }
        public int? HolidayId { get; set; }
        public string Status { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }

        public Model.MTimeTable ToModel()
        {
            return new Model.MTimeTable
            {
                Name = this.Name,
                Date = this.Date,
                WeekId = this.WeekId,
                HolidayId = this.HolidayId,
                Status = this.Status,
                TenantId = this.TenantId,
                CreatedBy = this.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    }
}
