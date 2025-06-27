using System;

namespace SchoolManagement.ViewModel.Week
{
    public class WeekUpdateVm
    {
        public string Name { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int UpdatedBy { get; set; }
    }
}
