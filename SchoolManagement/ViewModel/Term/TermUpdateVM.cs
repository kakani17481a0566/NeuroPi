using System;

namespace SchoolManagement.ViewModel.Term
{
    public class TermUpdateVM
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AcademicYearId { get; set; }
        public int UpdatedBy { get; set; }
    }
}
