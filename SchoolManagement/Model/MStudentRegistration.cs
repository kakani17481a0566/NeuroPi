using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("student_registration")]
    public class MStudentRegistration : MBaseModel
    {
        // PK
        [Key]
        [Column("id")]
        public int Id { get; set; }

        // ---- Foreign keys (required per domain) ----
        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }               // FK -> tenants.tenant_id (fk_sr_tenant)

        [Required]
        [Column("branch_id")]
        public int BranchId { get; set; }               // FK -> branch.id (fk_sr_branch)

        [Column("course_id")]
        public int? CourseId { get; set; }              // FK -> course.id (fk_sr_course)

        [Required]
        [Column("reg_type_id")]
        public int RegTypeId { get; set; }              // FK -> masters.id (fk_sr_regtype)

        // ---- Registration details ----
        [Column("reg_date")]
        public DateTime? RegDate { get; set; }          // DB has default CURRENT_DATE

        [Column("reg_number")]
        public string? RegNumber { get; set; }          // Nullable in schema

        // ---- Previous schooling ----
        [Required]
        [Column("att_pre_school")]
        public bool AttPreSchool { get; set; } = false;

        [Column("prev_sc_name")]
        public string? PrevScName { get; set; }

        [Required]
        [Column("prev_reg_kindergarten")]
        public bool PrevRegKindergarten { get; set; } = false;

        [Column("prev_kindergarten1_nsc")]
        public string? PrevKindergarten1Nsc { get; set; }

        // ---- Student identity ----
        [Required]
        [Column("stu_last_name")]
        public string StuLastName { get; set; } = string.Empty;

        [Required]
        [Column("stu_given_name")]
        public string StuGivenName { get; set; } = string.Empty;

        [Required]
        [Column("stu_dob")]
        public DateTime StuDob { get; set; }

        [Required]
        [Column("gender_id")]
        public int GenderId { get; set; }               // FK -> masters.id (fk_sr_gender)

        // ---- Transport ----
        [Required]
        [Column("reg_transport_id")]
        public int RegTransportId { get; set; }         // FK -> masters.id (fk_sr_transport)

        [Column("alt_transport_id")]
        public int? AltTransportId { get; set; }        // FK -> masters.id (fk_sr_alttransport)

        [Column("other_transport_text")]
        public string? OtherTransportText { get; set; }

        // ---- Misc ----
        [Required]
        [Column("speech_therapy")]
        public bool SpeechTherapy { get; set; } = false;

        [Required]
        [Column("custody")]
        public bool Custody { get; set; } = false;

        [Column("custody_of_id")]
        public int? CustodyOfId { get; set; }           

        [Column("lives_with_id")]
        public int? LivesWithId { get; set; }           

        // ---- Siblings ----
        [Required]
        [Column("siblings_in_this_school")]
        public bool SiblingsInThisSchool { get; set; } = false;

        [Column("siblings_this_names")]
        public string? SiblingsThisNames { get; set; }

        [Required]
        [Column("siblings_in_other_school")]
        public bool SiblingsInOtherSchool { get; set; } = false;

        [Column("siblings_other_names")]
        public string? SiblingsOtherNames { get; set; }
    }
}
