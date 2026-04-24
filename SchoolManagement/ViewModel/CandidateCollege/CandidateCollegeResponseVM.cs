namespace SchoolManagement.ViewModel.CandidateCollege
{
    public class CandidateCollegeResponseVM
    {
        public int Id { get; set; }
        public int CollegeId { get; set; }
        public string CollegeName { get; set; }
        public int EmpId { get; set; }
        public string EmployeeName { get; set; }
        public int? TenantId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}