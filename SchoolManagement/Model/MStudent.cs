using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("students")]
    public class MStudent : MBaseModel
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // ----------------------------
        // Student Core Info
        // ----------------------------
        [Column("first_name")]
        public string Name { get; set; } = default!;   // ✅ keep Name for backward compatibility

        [Column("last_name")]
        public string? LastName { get; set; }

        [Column("middle_name")]
        public string? MiddleName { get; set; }

        [NotMapped]
        public string FullName => $"{Name} {MiddleName} {LastName}".Trim();

        [Column("dob", TypeName = "date")]
        public DateOnly? DateOfBirth { get; set; }

        [Column("course_id")]
        public int CourseId { get; set; }

        [Column("branch_id")]
        public int BranchId { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        // ✅ Registration Number
        [Column("reg_number")]
        [MaxLength(20)]
        public string? RegNumber { get; set; }

        [Column("student_image_url")]
        public string? StudentImageUrl { get; set; }

        [Column("student_image")]
        public string? StudentImage { get; set; }

        [Column("bloodgroup")]
        public string? BloodGroup { get; set; }

        [Column("gender")]
        public string? Gender { get; set; }

        [Column("admission_grade")]
        public string? AdmissionGrade { get; set; }

        [Column("date_of_joining", TypeName = "date")]
        public DateOnly? DateOfJoining { get; set; }

        // ----------------------------
        // Contact References
        // ----------------------------
        [Column("f_contact")]
        public int? FatherContactId { get; set; }

        [Column("m_contact")]
        public int? MotherContactId { get; set; }

        [Column("g_contact")]
        public int? GuardianContactId { get; set; }

        [Column("as_contact")]
        public int? AdditionalSupportContactId { get; set; }

        [Column("emg_contact")]
        public int? EmergencyContactId { get; set; }

        // ----------------------------
        // Transport
        // ----------------------------
        [Column("has_reg_transport")]
        public bool HasRegularTransport { get; set; } = false;

        [Column("reg_transport_id")]
        public int? RegularTransportId { get; set; }

        [Column("reg_transport_text")]
        public string? RegularTransportText { get; set; }

        [Column("has_alt_transport")]
        public bool HasAlternateTransport { get; set; } = false;

        [Column("alt_transport_id")]
        public int? AlternateTransportId { get; set; }

        [Column("alt_transport_text")]
        public string? AlternateTransportText { get; set; }

        // ----------------------------
        // Custody & Family
        // ----------------------------
        [Column("speech_therapy")]
        public bool SpeechTherapy { get; set; } = false;

        [Column("custody")]
        public bool Custody { get; set; } = false;

        [Column("custody_of_id")]
        public int? CustodyOfId { get; set; }

        [Column("lives_with_id")]
        public int? LivesWithId { get; set; }

        [Column("siblings_in_this_school")]
        public bool SiblingsInThisSchool { get; set; } = false;

        [Column("siblings_this_names")]
        public string? SiblingsThisNames { get; set; }

        [Column("siblings_in_other_school")]
        public bool SiblingsInOtherSchool { get; set; } = false;

        [Column("siblings_other_names")]
        public string? SiblingsOtherNames { get; set; }

        // ----------------------------
        // Medical Info
        // ----------------------------
        [Column("any_allergy")]
        public bool AnyAllergy { get; set; } = false;

        [Column("what_allergy_id")]
        public int? WhatAllergyId { get; set; }

        [Column("other_allergy_text")]
        public string? OtherAllergyText { get; set; }

        [Column("medical_kit")]
        public bool MedicalKit { get; set; } = false;

        [Column("serious_medical_conditions", TypeName = "text")]
        public string? SeriousMedicalConditions { get; set; }

        [Column("serious_conditions_info", TypeName = "text")]
        public string? SeriousConditionsInfo { get; set; }

        [Column("other_medical_info", TypeName = "text")]
        public string? OtherMedicalInfo { get; set; }

        // ----------------------------
        // Languages
        // ----------------------------
        [Column("language_adults_home")]
        public string? LanguageAdultsHome { get; set; }

        [Column("language_most_used_with_child")]
        public string? LanguageMostUsedWithChild { get; set; }

        [Column("language_first_learned")]
        public string? LanguageFirstLearned { get; set; }

        // ----------------------------
        // English Skills
        // ----------------------------
        [Column("can_read_english")]
        public bool CanReadEnglish { get; set; } = false;

        [Column("read_skill_id")]
        public int? ReadSkillId { get; set; }

        [Column("can_write_english")]
        public bool CanWriteEnglish { get; set; } = false;

        [Column("write_skill_id")]
        public int? WriteSkillId { get; set; }

        // ----------------------------
        // Documents & Forms
        // ----------------------------
        [Column("signature", TypeName = "text")]
        public string? Signature { get; set; }

        [Column("birth_certificate")]
        public string? BirthCertificate { get; set; }

        [Column("kid_passport")]
        public string? KidPassport { get; set; }

        [Column("adhar")]
        public string? Adhar { get; set; }

        [Column("parent_adhar")]
        public string? ParentAdhar { get; set; }

        [Column("mother_adhar")]
        public string? MotherAdhar { get; set; }

        [Column("mother_photo")]
        public string? MotherPhoto { get; set; }

        [Column("father_photo")]
        public string? FatherPhoto { get; set; }

        [Column("joint_photo")]
        public string? JointPhoto { get; set; }

        [Column("health_form")]
        public string? HealthForm { get; set; }

        [Column("privacy_form")]
        public string? PrivacyForm { get; set; }

        [Column("liability_form")]
        public string? LiabilityForm { get; set; }


        [Column("registration_channel")]
        [MaxLength(20)]
        public string? RegistrationChannel { get; set; }


        // ----------------------------
        // Navigation Properties
        // ----------------------------
        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; } = default!;

        [ForeignKey(nameof(CourseId))]
        public virtual MCourse Course { get; set; } = default!;

        [ForeignKey(nameof(BranchId))]
        public virtual MBranch Branch { get; set; } = default!;

        // Contact navigations
        [ForeignKey(nameof(FatherContactId))]
        public virtual MContact? FatherContact { get; set; }

        [ForeignKey(nameof(MotherContactId))]
        public virtual MContact? MotherContact { get; set; }

        [ForeignKey(nameof(GuardianContactId))]
        public virtual MContact? GuardianContact { get; set; }

        [ForeignKey(nameof(AdditionalSupportContactId))]
        public virtual MContact? AdditionalSupportContact { get; set; }

        [ForeignKey(nameof(EmergencyContactId))]
        public virtual MContact? EmergencyContact { get; set; }

        // Master references
        [ForeignKey(nameof(RegularTransportId))]
        public virtual MMaster? RegularTransport { get; set; }

        [ForeignKey(nameof(AlternateTransportId))]
        public virtual MMaster? AlternateTransport { get; set; }

        [ForeignKey(nameof(CustodyOfId))]
        public virtual MMaster? CustodyOf { get; set; }

        [ForeignKey(nameof(LivesWithId))]
        public virtual MMaster? LivesWith { get; set; }

        [ForeignKey(nameof(WhatAllergyId))]
        public virtual MMaster? WhatAllergy { get; set; }

        [ForeignKey(nameof(ReadSkillId))]
        public virtual MMaster? ReadSkill { get; set; }

        [ForeignKey(nameof(WriteSkillId))]
        public virtual MMaster? WriteSkill { get; set; }

        // Audit self-references
        [ForeignKey(nameof(CreatedBy))]
        public virtual MUser? CreatedByUser { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public virtual MUser? UpdatedByUser { get; set; }

        // Collections
        public virtual ICollection<MStudentAttendance> StudentAttendances { get; set; } = new List<MStudentAttendance>();
        public virtual ICollection<MParentStudent> ParentStudents { get; set; } = new List<MParentStudent>();
        public virtual ICollection<MDailyAssessment> DailyAssessments { get; set; } = new List<MDailyAssessment>();
    }
}
