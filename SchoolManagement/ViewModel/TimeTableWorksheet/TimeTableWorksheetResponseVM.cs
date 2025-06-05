using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.TimeTableWorksheet
{
    public class TimeTableWorksheetResponseVM
    {
        public int Id { get; set; }
        public int TimeTableId { get; set; }
        public int WorksheetId { get; set; }
        public int TenantId { get; set; }

        public static TimeTableWorksheetResponseVM FromModel(MTimeTableWorksheet model)
        {
            if (model == null) return null;

            return new TimeTableWorksheetResponseVM
            {
                Id = model.Id,
                TimeTableId = model.TimeTableId,
                WorksheetId = model.WorksheetId,
                TenantId = model.TenantId
            };
        }
    }
}
