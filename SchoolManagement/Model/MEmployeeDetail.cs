using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Model
{
    [Table("employee_details")]
    public class MEmployeeDetail : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("employee_code")]
        public string EmployeeCode { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("date_of_joining")]
        public DateTime? DateOfJoining { get; set; }

        [Column("contact_number")]
        public string ContactNumber { get; set; }

        [Column("indian_number")]
        public string IndianNumber { get; set; }

        [Column("call_responses")]
        public string CallResponses { get; set; }

        [Column("nationality")]
        public string Nationality { get; set; }

        [Column("designation")]
        public string Designation { get; set; }

        [Column("unit")]
        public string Unit { get; set; }

        [Column("beneficiary")]
        public string Beneficiary { get; set; }

        [Column("beneficiary_dob")]
        public DateTime? BeneficiaryDob { get; set; }

        [Column("grade")]
        public string Grade { get; set; }

        [Column("academic_year")]
        public string AcademicYear { get; set; }

        [Column("current_status")]
        public string CurrentStatus { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }
    }
}
