namespace SchoolManagement.ViewModel.TimeTable
{
    public class TimeTableResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int? WeekId { get; set; }
        public int? HolidayId { get; set; }
        public string Status { get; set; }
        public int TenantId { get; set; }

        public static TimeTableResponseVM FromModel(Model.MTimeTable model)
        {
            if (model == null) return null;

            return new TimeTableResponseVM
            {
                Id = model.Id,
                Name = model.Name,
                Date = model.Date,
                WeekId = model.WeekId,
                HolidayId = model.HolidayId,
                Status = model.Status,
                TenantId = model.TenantId
            };
        }
    }
}
