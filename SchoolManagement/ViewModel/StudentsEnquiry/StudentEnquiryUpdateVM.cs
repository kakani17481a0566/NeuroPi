namespace SchoolManagement.ViewModel.StudentsEnquiry
{
    public class StudentEnquiryUpdateVM
    {
        public string StudentFirstName { get; set; }

        public string? StudentMiddleName { get; set; }

        public string StudentLastName { get; set; }

        public DateTime? Dob { get; set; }

        public int? GenderId { get; set; }

      

        public int AdmissionCourseId { get; set; }

        public string? PreviousSchoolName { get; set; }

        public int? FromCourseId { get; set; }

        public short? FromYear { get; set; }

        public int? ToCourseId { get; set; }

        public short? ToYear { get; set; }

        public bool IsGuardian { get; set; }

        public int ParentContactId { get; set; }

        public int? MotherContactId { get; set; }

        public int? HearAboutUsTypeId { get; set; }

        public bool IsAgreedToTerms { get; set; }

        public string? Signature { get; set; }

        public int StatusId { get; set; }
        

        public int? BranchId { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
