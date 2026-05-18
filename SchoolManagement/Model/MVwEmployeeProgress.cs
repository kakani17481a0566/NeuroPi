using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("vw_employee_progress")]
    public class MVwEmployeeProgress
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("employee_code")]
        public string? EmployeeCode { get; set; }

        [Column("employee_name")]
        public string? EmployeeName { get; set; }

        [Column("contact_number")]
        public string? ContactNumber { get; set; }

        [Column("indian_number")]
        public string? IndianNumber { get; set; }

        [Column("nationality")]
        public string? Nationality { get; set; }

        [Column("designation")]
        public string? Designation { get; set; }

        [Column("unit")]
        public string? Unit { get; set; }

        [Column("date_of_joining")]
        public DateTime? DateOfJoining { get; set; }

        [Column("call_responses")]
        public string? CallResponses { get; set; }

        [Column("beneficiary")]
        public string? Beneficiary { get; set; }

        [Column("beneficiary_dob")]
        public DateTime? BeneficiaryDob { get; set; }

        [Column("beneficiary_relationship_name")]
        public string? BeneficiaryRelationshipName { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [Column("completed_steps")]
        public int CompletedSteps { get; set; }

        [Column("progress_status")]
        public string? ProgressStatus { get; set; }

        [Column("latest_stage")]
        public string? LatestStage { get; set; }

        [Column("latest_call_date")]
        public DateTime? LatestCallDate { get; set; }
    }
}
