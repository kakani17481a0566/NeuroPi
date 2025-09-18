using System;
using System.Collections.Generic;

namespace SchoolManagement.ViewModel.Student
{
    public class SRStudentRegistrationRequestVM
    {
        public int TenantId { get; set; }
        public SRUserVM User { get; set; } = default!;
        public SRStudentVM Student { get; set; } = default!;
        public List<SRContactVM> Contacts { get; set; } = new();
    }

    // -----------------------
    // User (Parent Login)
    // -----------------------
    public class SRUserVM
    {
        public string Username { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;  // ⚠️ hash before saving
        public string MobileNumber { get; set; } = default!;
        public int RoleTypeId { get; set; }

        // ✅ Base64 string from client
        public string? UserImageUrl { get; set; }

        public DateTime Dob { get; set; }
    }

    // -----------------------
    // Student
    // -----------------------
    public class SRStudentVM
    {
        public string FirstName { get; set; } = default!;
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; } = default!;
        public string? BloodGroup { get; set; }
        public string AdmissionGrade { get; set; } = default!;
        public DateTime DateOfJoining { get; set; }
        public int CourseId { get; set; }
        public int BranchId { get; set; }

        public string? RegistrationChannel { get; set; }  // "By Post", "In Person", "Online"

        // ✅ Images (Base64 strings)
        public string? StudentImage { get; set; }
        public string? MotherPhoto { get; set; }
        public string? FatherPhoto { get; set; }
        public string? JointPhoto { get; set; }

        // Transport
        public SRTransportVM Transport { get; set; } = default!;

        // Custody & Family
        public SRCustodyFamilyVM CustodyFamily { get; set; } = default!;

        // Medical Info
        public SRMedicalInfoVM MedicalInfo { get; set; } = default!;

        // Languages
        public SRLanguagesVM Languages { get; set; } = default!;

        // English Skills
        public SREnglishSkillsVM EnglishSkills { get; set; } = default!;

        // Documents
        public SRDocumentsVM Documents { get; set; } = default!;
    }

    // -----------------------
    // Transport
    // -----------------------
    public class SRTransportVM
    {
        public SRTransportOptionVM Regular { get; set; } = default!;
        public SRTransportOptionVM Alternate { get; set; } = default!;
        public string? OtherTransportText { get; set; }
    }

    public class SRTransportOptionVM
    {
        public bool IsEnabled { get; set; }
        public int? TransportId { get; set; }
        public string? FreeText { get; set; }
    }

    // -----------------------
    // Custody & Family
    // -----------------------
    public class SRCustodyFamilyVM
    {
        public bool SpeechTherapy { get; set; }
        public bool Custody { get; set; }
        public int? CustodyOfId { get; set; }
        public int? LivesWithId { get; set; }
        public bool SiblingsInThisSchool { get; set; }
        public string? SiblingsThisNames { get; set; }
        public bool SiblingsInOtherSchool { get; set; }
        public string? SiblingsOtherNames { get; set; }
    }

    // -----------------------
    // Medical Info
    // -----------------------
    public class SRMedicalInfoVM
    {
        public bool AnyAllergy { get; set; }
        public int? WhatAllergyId { get; set; }
        public string? OtherAllergyText { get; set; }
        public bool MedicalKit { get; set; }
        public string? SeriousMedicalConditions { get; set; }
        public string? SeriousConditionsInfo { get; set; }
        public string? OtherMedicalInfo { get; set; }
    }

    // -----------------------
    // Languages
    // -----------------------
    public class SRLanguagesVM
    {
        public string? LanguageAdultsHome { get; set; }
        public string? LanguageMostUsedWithChild { get; set; }
        public string? LanguageFirstLearned { get; set; }
    }

    // -----------------------
    // English Skills
    // -----------------------
    public class SREnglishSkillsVM
    {
        public bool CanReadEnglish { get; set; }
        public int? ReadSkillId { get; set; }
        public bool CanWriteEnglish { get; set; }
        public int? WriteSkillId { get; set; }
    }

    // -----------------------
    // Documents (Base64 strings)
    // -----------------------
    public class SRDocumentsVM
    {
        public string? Signature { get; set; }
        public string? BirthCertificate { get; set; }
        public string? KidPassport { get; set; }
        public string? Adhar { get; set; }
        public string? ParentAdhar { get; set; }
        public string? MotherAdhar { get; set; }
        public string? HealthForm { get; set; }
        public string? PrivacyForm { get; set; }
        public string? LiabilityForm { get; set; }
    }

    // -----------------------
    // Contact
    // -----------------------
    public class SRContactVM
    {
        public string Name { get; set; } = default!;
        public string PriNumber { get; set; } = default!;
        public string? SecNumber { get; set; }
        public string? Email { get; set; }
        public string Address1 { get; set; } = default!;
        public string? Address2 { get; set; }
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Pincode { get; set; } = default!;
        public string? Qualification { get; set; }
        public string? Profession { get; set; }
        public int RelationshipId { get; set; }
    }
}
