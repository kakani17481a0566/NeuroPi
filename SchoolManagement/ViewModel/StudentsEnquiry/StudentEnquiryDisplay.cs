namespace SchoolManagement.ViewModel.StudentsEnquiry
{
    public class StudentEnquiryDisplay
    {
        public int StudentEnquiryId { get; set; }
        // Student Info
        public string FullName { get; set; } = string.Empty;
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; }
        // Course Info
        public int AdmissionCourseId { get; set; }
        public string? AdmissionCourseName { get; set; }
        public int? FromCourseId { get; set; }
        public string? FromCourseName { get; set; }
        public short? FromYear { get; set; }
        public int? ToCourseId { get; set; }
        public string? ToCourseName { get; set; }
        public short? ToYear { get; set; }
        public string? PreviousSchoolName { get; set; }
        // Parent & Mother Contacts
        public int ParentContactId { get; set; }
        public string? ParentName { get; set; }
        public string? ParentPhone { get; set; }
        public string? ParentEmail { get; set; }
        public int? MotherContactId { get; set; }
        public string? MotherName { get; set; }
        public string? MotherPhone { get; set; }
        public string? MotherEmail { get; set; }
        // Heard About Us
        public int? HearAboutUsTypeId { get; set; }
        public string? HearAboutUsName { get; set; }
        // Status
        public int StatusId { get; set; }
        public string? StatusName { get; set; }
        // Branch
        public int? BranchId { get; set; }
        public string? BranchName { get; set; }
        public string? BranchContact { get; set; }
        public string? BranchAddress { get; set; }
        public string? BranchDistrict { get; set; }
        public string? BranchState { get; set; }
        public string? BranchPincode { get; set; }
        // Tenant
        public int TenantId { get; set; }
        public string? TenantName { get; set; }
        // Audit Info
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public string? UpdatedByName { get; set; }
        // Misc
        public bool IsGuardian { get; set; }
        public bool IsAgreedToTerms { get; set; }
    }
}
