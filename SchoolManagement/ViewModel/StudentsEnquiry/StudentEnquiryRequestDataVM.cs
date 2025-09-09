using SchoolManagement.Model;
using System;

namespace SchoolManagement.ViewModel.StudentsEnquiry
{
    public class StudentEnquiryRequestDataVM
    {
        // ----------------------------
        // Student Details
        // ----------------------------
        public string StudentFirstName { get; set; }
        public string? StudentMiddleName { get; set; }
        public string StudentLastName { get; set; }
        public DateTime? Dob { get; set; }
        public int? GenderId { get; set; }
        public int AdmissionCourseId { get; set; }
        public string? PrevSchoolName { get; set; }
        public int? FromCourseId { get; set; }
        public short? FromYear { get; set; }
        public int? ToCourseId { get; set; }
        public short? ToYear { get; set; }
        public bool IsGuardian { get; set; }

        // ----------------------------
        // Parent / Guardian Contact
        // ----------------------------
        public string ParentFirstName { get; set; }
        public string? ParentMiddleName { get; set; }
        public string ParentLastName { get; set; }
        public string ParentPhone { get; set; }
        public string? ParentAlternatePhone { get; set; }
        public string? ParentEmail { get; set; }
        public string ParentAddress1 { get; set; }
        public string? ParentAddress2 { get; set; }
        public string ParentCity { get; set; }
        public string? ParentState { get; set; }
        public string? ParentPincode { get; set; }

        public string? ParentQualification { get; set; }
        public string? ParentProfession { get; set; }

        // ----------------------------
        // Mother Contact (Optional)
        // ----------------------------
        public string? MotherFirstName { get; set; }
        public string? MotherMiddleName { get; set; }
        public string? MotherLastName { get; set; }
        public string? MotherPhone { get; set; }
        public string? MotherEmail { get; set; }
        public string? MotherQualification { get; set; }
        public string? MotherProfession { get; set; }

        // ----------------------------
        // Additional Info
        // ----------------------------
        public int? HearAboutUsTypeId { get; set; }
        public bool IsAgreedToTerms { get; set; }

        // UI sends a data URL or base64 string; service decodes to byte[] (DB bytea)
        public string? Signature { get; set; }  // <-- CHANGED from byte[]? to string?

        public int StatusId { get; set; }

        // ----------------------------
        // Context Info
        // ----------------------------
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
        public int? BranchId { get; set; }

        // ----------------------------
        // Mapping Helpers
        // ----------------------------
        public MContact ToParentContact()
        {
            return new MContact
            {
                Name = $"{ParentFirstName} {ParentMiddleName} {ParentLastName}".Trim(),
                PriNumber = ParentPhone,
                SecNumber = ParentAlternatePhone,
                Email = ParentEmail,
                Address1 = ParentAddress1,
                Address2 = ParentAddress2,
                City = ParentCity,
                State = ParentState,
                Pincode = ParentPincode,
                Qualification = ParentQualification,
                Profession = ParentProfession,
                TenantId = TenantId,
                CreatedBy = CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
        }

        public MContact? ToMotherContact()
        {
            if (string.IsNullOrWhiteSpace(MotherFirstName) || string.IsNullOrWhiteSpace(MotherPhone))
                return null;

            return new MContact
            {
                Name = $"{MotherFirstName} {MotherMiddleName} {MotherLastName}".Trim(),
                PriNumber = MotherPhone,
                Email = MotherEmail,
                Address1 = ParentAddress1, // Assuming same address
                Address2 = ParentAddress2,
                City = ParentCity,
                State = ParentState,
                Pincode = ParentPincode,
                Qualification = MotherQualification,
                Profession = MotherProfession,
                TenantId = TenantId,
                CreatedBy = CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    }
}
