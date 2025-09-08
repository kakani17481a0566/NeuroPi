using NeuroPi.UserManagment.Model;
using SchoolManagement.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("students_enquiry")]
    public class MStudentsEnquiry : MBaseModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required, MaxLength(100)]
        [Column("student_first_name")]
        public string StudentFirstName { get; set; }

        [MaxLength(100)]
        [Column("student_middle_name")]
        public string? StudentMiddleName { get; set; }

        [Required, MaxLength(100)]
        [Column("student_last_name")]
        public string StudentLastName { get; set; }

        [Column("dob")]
        public DateTime? Dob { get; set; }

        [Column("gender_id")]
        public int? GenderId { get; set; }

        [Required]
        [Column("admission_course_id")]
        public int AdmissionCourseId { get; set; }

        [MaxLength(150)]
        [Column("prv_sch_name")]
        public string? PreviousSchoolName { get; set; }

        [Column("frm_course_id")]
        public int? FromCourseId { get; set; }

        [Column("frm_year")]
        public short? FromYear { get; set; }

        [Column("to_course_id")]
        public int? ToCourseId { get; set; }

        [Column("to_year")]
        public short? ToYear { get; set; }

        [Required]
        [Column("is_guardian")]
        public bool IsGuardian { get; set; } = false;

        [Required]
        [Column("parent_contact_id")]
        public int ParentContactId { get; set; }

        [Column("mother_contact_id")]
        public int? MotherContactId { get; set; }

        [Column("hear_abt_us_type_id")]
        public int? HearAboutUsTypeId { get; set; }

        [Required]
        [Column("is_agreed_to_terms")]
        public bool IsAgreedToTerms { get; set; } = false;

        [Column("signature")]
        public string? Signature { get; set; }

        [Required]
        [Column("status_id")]
        public int StatusId { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [Column("branch_id")]
        public int? BranchId { get; set; }

        // ✅ Navigation Properties

        [ForeignKey(nameof(AdmissionCourseId))]
        public virtual MCourse? AdmissionCourse { get; set; }

        [ForeignKey(nameof(FromCourseId))]
        public virtual MCourse? FromCourse { get; set; }

        [ForeignKey(nameof(ToCourseId))]
        public virtual MCourse? ToCourse { get; set; }

        [ForeignKey(nameof(ParentContactId))]
        public virtual MContact? ParentContact { get; set; }

        [ForeignKey(nameof(MotherContactId))]
        public virtual MContact? MotherContact { get; set; }

        [ForeignKey(nameof(StatusId))]
        public virtual MMaster? Status { get; set; }

        [ForeignKey(nameof(GenderId))]
        public virtual MMaster? Gender { get; set; }

        [ForeignKey(nameof(HearAboutUsTypeId))]
        public virtual MMaster? HearAboutUs { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant? Tenant { get; set; }

        [ForeignKey(nameof(BranchId))]
        public virtual MBranch? Branch { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public virtual MUser? CreatedUser { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public virtual MUser? UpdatedUser { get; set; }
    }
}
