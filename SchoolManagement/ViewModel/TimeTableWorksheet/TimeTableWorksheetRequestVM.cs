using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.TimeTableWorksheet
{
    public class TimeTableWorksheetRequestVM
    {
        public int TimeTableId { get; set; }
        public int WorksheetId { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }

        public MTimeTableWorksheet ToModel()
        {
            return new MTimeTableWorksheet
            {
                TimeTableId = this.TimeTableId,
                WorksheetId = this.WorksheetId,
                TenantId = this.TenantId,
                CreatedBy = this.CreatedBy,
                // CreatedOn set in service
            };
        }
    }
}
