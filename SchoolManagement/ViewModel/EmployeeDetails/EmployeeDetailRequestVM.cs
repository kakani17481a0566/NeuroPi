using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.EmployeeDetails
{
    public class EmployeeDetailRequestVM
    {
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public int? StatusId { get; set; }
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
        public int TenantId { get; set; }
        public int? PermanentAddressId { get; set; }

        public static MEmployeeDetail ToModel(EmployeeDetailRequestVM request)
        {
            return new MEmployeeDetail
            {
                EmployeeCode = request.EmployeeCode,
                Name = request.Name,
                StatusId = request.StatusId,
                DateOfJoining = request.DateOfJoining,
                ContactNumber = request.ContactNumber,
                IndianNumber = request.IndianNumber,
                CallResponses = request.CallResponses,
                Nationality = request.Nationality,
                Designation = request.Designation,
                Unit = request.Unit,
                Beneficiary = request.Beneficiary,
                BeneficiaryDob = request.BeneficiaryDob,
                BeneficiaryRelationshipName = request.BeneficiaryRelationshipName,
                Grade = request.Grade,
                AcademicYear = request.AcademicYear,
                CurrentStatusId = request.CurrentStatusId,
                TenantId = request.TenantId,
                PermanentAddressId = request.PermanentAddressId,
            };
        }
    }
}
