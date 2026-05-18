using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.EmployeeProgress
{
    public class EmployeeProgressVm
    {
        public int Id { get; set; }
        public string? EmployeeCode { get; set; }
        public string? EmployeeName { get; set; }
        public string? ContactNumber { get; set; }
        public string? IndianNumber { get; set; }
        public string? Nationality { get; set; }
        public string? Designation { get; set; }
        public string? Unit { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string? CallResponses { get; set; }
        public string? Beneficiary { get; set; }
        public DateTime? BeneficiaryDob { get; set; }
        public string? BeneficiaryRelationshipName { get; set; }
        public int TenantId { get; set; }
        public int CompletedSteps { get; set; }
        public string? ProgressStatus { get; set; }
        public string? LatestStage { get; set; }
        public DateTime? LatestCallDate { get; set; }

        public static EmployeeProgressVm FromModel(MVwEmployeeProgress model)
        {
            return new EmployeeProgressVm
            {
                Id = model.Id,
                EmployeeCode = model.EmployeeCode,
                EmployeeName = model.EmployeeName,
                ContactNumber = model.ContactNumber,
                IndianNumber = model.IndianNumber,
                Nationality = model.Nationality,
                Designation = model.Designation,
                Unit = model.Unit,
                DateOfJoining = model.DateOfJoining,
                CallResponses = model.CallResponses,
                Beneficiary = model.Beneficiary,
                BeneficiaryDob = model.BeneficiaryDob,
                BeneficiaryRelationshipName = model.BeneficiaryRelationshipName,
                TenantId = model.TenantId,
                CompletedSteps = model.CompletedSteps,
                ProgressStatus = model.ProgressStatus,
                LatestStage = model.LatestStage,
                LatestCallDate = model.LatestCallDate
            };
        }
    }
}
