

namespace SchoolManagement.ViewModel.EmployeeDetails
{
    public class EmployeeDetailsVM
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public int? StatusId { get; set; }
        public string  Status { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string ContactNumber { get; set; }
        public string IndianNumber { get; set; }
        public string CallResponses { get; set; }
        public string Nationality { get; set; }
        public string Designation { get; set; }
        public string Unit { get; set; }

        public string Beneficiary { get; set; }

        public DateTime? BeneficiaryDob { get; set; }

        public string BeneficiaryRelationshipName { get; set; }

        public string Grade { get; set; }

        public string AcademicYear { get; set; }

        public int? CurrentStatusId { get; set; }

        public string CurrentStatus { get; set; }

        public int? TenantId { get; set; }
    }
}
