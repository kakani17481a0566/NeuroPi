using System;
using System.Collections.Generic;

namespace SchoolManagement.ViewModel.Student
{
    public class SRStudentRegistrationRequestVM
    {
        public int TenantId { get; set; }
        public SRUserVM User { get; set; }
        public SRStudentVM Student { get; set; }
        public List<SRContactVM> Contacts { get; set; } = new();
    }

    // -----------------------
    // User (Parent Login)
    // -----------------------
    public class SRUserVM
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public int RoleTypeId { get; set; }
        public string? UserImageUrl { get; set; }
        public DateTime Dob { get; set; }


    }

    // -----------------------
    // Student
    // -----------------------
    public class SRStudentVM
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string? BloodGroup { get; set; }
        public string AdmissionGrade { get; set; }
        public DateTime DateOfJoining { get; set; }
        public int CourseId { get; set; }
        public int BranchId { get; set; }

        public string? RegistrationChannel { get; set; }  // "By Post", "In Person", "Online"


        // Images
        public string? StudentImageUrl { get; set; }
        public string? StudentImage { get; set; }
        public string? MotherPhoto { get; set; }
        public string? FatherPhoto { get; set; }
        public string? JointPhoto { get; set; }

        // Transport
        public SRTransportVM Transport { get; set; }

        // Custody & Family
        public SRCustodyFamilyVM CustodyFamily { get; set; }

        // Medical Info
        public SRMedicalInfoVM MedicalInfo { get; set; }

        // Languages
        public SRLanguagesVM Languages { get; set; }

        // English Skills
        public SREnglishSkillsVM EnglishSkills { get; set; }

        // Documents
        public SRDocumentsVM Documents { get; set; }
    }

    // -----------------------
    // Transport
    // -----------------------
    public class SRTransportVM
    {
        public SRTransportOptionVM Regular { get; set; }
        public SRTransportOptionVM Alternate { get; set; }
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
    // Documents
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
        public string Name { get; set; }
        public string PriNumber { get; set; }
        public string? SecNumber { get; set; }
        public string? Email { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string? Qualification { get; set; }
        public string? Profession { get; set; }
        public int RelationshipId { get; set; }
    }
}
