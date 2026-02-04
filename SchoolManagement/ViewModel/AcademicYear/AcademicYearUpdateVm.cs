using System;

namespace SchoolManagement.ViewModel.AcademicYear
{
    public class AcademicYearUpdateVm
    {
        public string Name { get; set; }
        public string Contact { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
