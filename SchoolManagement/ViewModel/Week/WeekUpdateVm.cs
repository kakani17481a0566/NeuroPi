using System;

namespace SchoolManagement.ViewModel.Week
{
    public class WeekUpdateVm
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UpdatedBy { get; set; }
    }
}
