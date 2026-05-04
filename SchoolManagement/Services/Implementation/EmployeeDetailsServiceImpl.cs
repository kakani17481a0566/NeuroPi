using Microsoft.EntityFrameworkCore;
using NeuroPi.UserManagment.Model;
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

        private void LogAudit(int? userId, string action, int recordId, string oldValues, string newValues, int tenantId)
        {
            var auditLog = new MAuditLog
            {
                UserId = userId,
                Action = action,
                Entity = "EmployeeDetail",
                RecordId = recordId,
                OldValues = oldValues,
                NewValues = newValues,
                Timestamp = DateTime.UtcNow,
                TenantId = tenantId
            };
            context.AuditLogs.Add(auditLog);
        }
        public List<EmployeeDetailsVM> GetAllEmployees(int tenantId)
        {
            var result=context.EmployeeDetails.Where(e=>e.TenantId==tenantId && !e.IsDeleted).Include(e=>e.Status).Include(e=>e.CurrentStatus).Include(e=>e.PermanentAddress).ToList();
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
                    TenantId = r.TenantId,
                    PermanentAddressId = r.PermanentAddressId,
                    PermanentAddress = r.PermanentAddress
                }).ToList();

            }
            return null;
        }

        public EmployeeDetailsVM CreateEmployeeDetail(EmployeeDetailRequestVM request)
        {
            if (request.PermanentAddressId.HasValue)
            {
                var contact = context.Contacts.FirstOrDefault(c => c.Id == request.PermanentAddressId && c.TenantId == request.TenantId && !c.IsDeleted);
                if (contact == null)
                {
                    return null;
                }
            }

            var employeeDetail = EmployeeDetailRequestVM.ToModel(request);
            context.EmployeeDetails.Add(employeeDetail);
            context.SaveChanges();

            var createdDetail = context.EmployeeDetails
                .Include(e => e.Status)
                .Include(e => e.CurrentStatus)
                .Include(e => e.PermanentAddress)
                .FirstOrDefault(e => e.Id == employeeDetail.Id);

            return new EmployeeDetailsVM
            {
                Id = createdDetail.Id,
                Name = createdDetail.Name,
                EmployeeCode = createdDetail.EmployeeCode,
                AcademicYear = createdDetail.AcademicYear,
                StatusId = createdDetail.StatusId,
                Status = createdDetail.Status?.Name,
                DateOfJoining = createdDetail.DateOfJoining,
                ContactNumber = createdDetail.ContactNumber,
                IndianNumber = createdDetail.IndianNumber,
                CallResponses = createdDetail.CallResponses,
                Nationality = createdDetail.Nationality,
                Designation = createdDetail.Designation,
                Unit = createdDetail.Unit,
                Beneficiary = createdDetail.Beneficiary,
                BeneficiaryDob = createdDetail.BeneficiaryDob,
                BeneficiaryRelationshipName = createdDetail.BeneficiaryRelationshipName,
                Grade = createdDetail.Grade,
                CurrentStatusId = createdDetail.CurrentStatusId,
                CurrentStatus = createdDetail.CurrentStatus?.Name,
                TenantId = createdDetail.TenantId,
                PermanentAddressId = createdDetail.PermanentAddressId,
                PermanentAddress = createdDetail.PermanentAddress
            };
        }

        public EmployeeDetailsVM UpdateEmployeeDetail(int id, int tenantId, EmployeeDetailUpdateVM request)
        {
            var employeeDetail = context.EmployeeDetails.FirstOrDefault(e => e.Id == id && e.TenantId == tenantId && !e.IsDeleted);
            if (employeeDetail == null)
            {
                return null;
            }

            if (request.PermanentAddressId.HasValue)
            {
                var contact = context.Contacts.FirstOrDefault(c => c.Id == request.PermanentAddressId && c.TenantId == tenantId && !c.IsDeleted);
                if (contact == null)
                {
                    return null;
                }
            }

            // Capture old values for audit
            var oldPermanentAddressId = employeeDetail.PermanentAddressId;

            if (request.EmployeeCode != null) employeeDetail.EmployeeCode = request.EmployeeCode;
            if (request.Name != null) employeeDetail.Name = request.Name;
            if (request.StatusId.HasValue) employeeDetail.StatusId = request.StatusId;
            if (request.DateOfJoining.HasValue) employeeDetail.DateOfJoining = request.DateOfJoining;
            if (request.ContactNumber != null) employeeDetail.ContactNumber = request.ContactNumber;
            if (request.IndianNumber != null) employeeDetail.IndianNumber = request.IndianNumber;
            if (request.CallResponses != null) employeeDetail.CallResponses = request.CallResponses;
            if (request.Nationality != null) employeeDetail.Nationality = request.Nationality;
            if (request.Designation != null) employeeDetail.Designation = request.Designation;
            if (request.Unit != null) employeeDetail.Unit = request.Unit;
            if (request.Beneficiary != null) employeeDetail.Beneficiary = request.Beneficiary;
            if (request.BeneficiaryDob.HasValue) employeeDetail.BeneficiaryDob = request.BeneficiaryDob;
            if (request.BeneficiaryRelationshipName != null) employeeDetail.BeneficiaryRelationshipName = request.BeneficiaryRelationshipName;
            if (request.Grade != null) employeeDetail.Grade = request.Grade;
            if (request.AcademicYear != null) employeeDetail.AcademicYear = request.AcademicYear;
            if (request.CurrentStatusId.HasValue) employeeDetail.CurrentStatusId = request.CurrentStatusId;
            if (request.PermanentAddressId.HasValue) employeeDetail.PermanentAddressId = request.PermanentAddressId;

            // Log audit for permanentAddressId change
            if (oldPermanentAddressId != employeeDetail.PermanentAddressId)
            {
                LogAudit(employeeDetail.UpdatedBy, "UPDATE", employeeDetail.Id,
                    $"PermanentAddressId: {oldPermanentAddressId}",
                    $"PermanentAddressId: {employeeDetail.PermanentAddressId}",
                    tenantId);
            }

            context.SaveChanges();

            var updatedDetail = context.EmployeeDetails
                .Include(e => e.Status)
                .Include(e => e.CurrentStatus)
                .Include(e => e.PermanentAddress)
                .FirstOrDefault(e => e.Id == id);

            return new EmployeeDetailsVM
            {
                Id = updatedDetail.Id,
                Name = updatedDetail.Name,
                EmployeeCode = updatedDetail.EmployeeCode,
                AcademicYear = updatedDetail.AcademicYear,
                StatusId = updatedDetail.StatusId,
                Status = updatedDetail.Status?.Name,
                DateOfJoining = updatedDetail.DateOfJoining,
                ContactNumber = updatedDetail.ContactNumber,
                IndianNumber = updatedDetail.IndianNumber,
                CallResponses = updatedDetail.CallResponses,
                Nationality = updatedDetail.Nationality,
                Designation = updatedDetail.Designation,
                Unit = updatedDetail.Unit,
                Beneficiary = updatedDetail.Beneficiary,
                BeneficiaryDob = updatedDetail.BeneficiaryDob,
                BeneficiaryRelationshipName = updatedDetail.BeneficiaryRelationshipName,
                Grade = updatedDetail.Grade,
                CurrentStatusId = updatedDetail.CurrentStatusId,
                CurrentStatus = updatedDetail.CurrentStatus?.Name,
                TenantId = updatedDetail.TenantId,
                PermanentAddressId = updatedDetail.PermanentAddressId,
                PermanentAddress = updatedDetail.PermanentAddress
            };
        }

        public EmployeeDetailsVM DeleteEmployeeDetail(int id, int tenantId)
        {
            var employeeDetail = context.EmployeeDetails.FirstOrDefault(e => e.Id == id && e.TenantId == tenantId && !e.IsDeleted);
            if (employeeDetail == null)
            {
                return null;
            }

            employeeDetail.IsDeleted = true;
            context.SaveChanges();

            return new EmployeeDetailsVM
            {
                Id = employeeDetail.Id,
                Name = employeeDetail.Name,
                EmployeeCode = employeeDetail.EmployeeCode,
                TenantId = employeeDetail.TenantId,
                PermanentAddressId = employeeDetail.PermanentAddressId
            };
        }
    }
}
