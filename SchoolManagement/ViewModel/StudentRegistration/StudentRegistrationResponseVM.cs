namespace SchoolManagement.ViewModel.StudentRegistration
{
    public class StudentRegistrationResponseVM
    {
        public int Id { get; set; }

        // --- Basic info ---
        public string RegNumber { get; set; } = string.Empty;
        public DateTime RegDate { get; set; }

        // --- Student identity ---
        public string StuLastName { get; set; } = string.Empty;
        public string StuGivenName { get; set; } = string.Empty;
        public DateTime StuDob { get; set; }
        public int GenderId { get; set; }
        public string? GenderName { get; set; }     // denormalized lookup

        // --- School details ---
        public int TenantId { get; set; }
        public string? TenantName { get; set; }
        public int BranchId { get; set; }
        public string? BranchName { get; set; }
        public int? CourseId { get; set; }
        public string? CourseName { get; set; }
        public int RegTypeId { get; set; }
        public string? RegTypeName { get; set; }

        // --- Transport ---
        public int RegTransportId { get; set; }
        public string? RegTransportName { get; set; }
        public int? AltTransportId { get; set; }
        public string? AltTransportName { get; set; }
        public string? OtherTransportText { get; set; }

        // --- Previous schooling ---
        public bool AttPreSchool { get; set; }
        public string? PrevScName { get; set; }
        public bool PrevRegKindergarten { get; set; }
        public string? PrevKindergarten1Nsc { get; set; }

        // --- Family / misc ---
        public bool SpeechTherapy { get; set; }
        public bool Custody { get; set; }
        public int? CustodyOfId { get; set; }
        public string? CustodyOfName { get; set; }
        public int? LivesWithId { get; set; }
        public string? LivesWithName { get; set; }
        public bool SiblingsInThisSchool { get; set; }
        public string? SiblingsThisNames { get; set; }
        public bool SiblingsInOtherSchool { get; set; }
        public string? SiblingsOtherNames { get; set; }

        // --- Audit ---
        public DateTime CreatedOn { get; set; }
        public string? CreatedByName { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedByName { get; set; }
    }
}
