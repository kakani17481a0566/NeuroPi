using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.EmployeeDetails;

namespace SchoolManagement.Services.Implementation
{
    public class EmployeeDetailsServiceImpl : IEmployeeDetailService
    {
        private readonly SchoolManagementDb context;
        public EmployeeDetailsServiceImpl(SchoolManagementDb _context)
        {
            context = _context;
            
        }
        public List<EmployeeDetailsVM> GetAllEmployees(int tenantId)
        {
            var result=context.EmployeeDetails.Where(e=>e.TenantId==tenantId).Include(e=>e.Status).Include(e=>e.CurrentStatus).ToList();
            if (result.Count > 0 && result != null)
            {
               return result.Select(r => new EmployeeDetailsVM
                {
                    Id = r.Id,
                    Name = r.Name,
                    EmployeeCode = r.EmployeeCode,
                    AcademicYear = r.AcademicYear,
                    StatusId = r.StatusId,
                    Status = r.Status.Name,
                    DateOfJoining = r.DateOfJoining,
                    ContactNumber = r.ContactNumber,
                    IndianNumber = r.IndianNumber,
                    CallResponses = r.CallResponses,
                    Nationality = r.Nationality,
                    Designation = r.Designation,
                    Unit = r.Unit,
                    Beneficiary = r.Beneficiary,
                    BeneficiaryDob = r.BeneficiaryDob,
                    BeneficiaryRelationshipName = r.BeneficiaryRelationshipName,
                    Grade = r.Grade,
                    CurrentStatusId = r.CurrentStatusId,
                    CurrentStatus = r.CurrentStatus.Name,
                    TenantId = r.TenantId
                }).ToList();

            }
            return null;
        }
    }
}
