using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("candidate_college")]
    public class MCandidateCollege : NeuroPi.UserManagment.Model.MBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("college_id")]
        [ForeignKey(nameof(College))]
        public int CollegeId { get; set; }

        [Column("emp_id")]
        [ForeignKey(nameof(Employee))]
        public int EmpId { get; set; }

        [Column("tenant_id")]
        [ForeignKey(nameof(Tenant))]
        public int? TenantId { get; set; }

        [ForeignKey(nameof(CollegeId))]
        public virtual MCollegeDetail College { get; set; }

        [ForeignKey(nameof(EmpId))]
        public virtual MEmployeeDetail Employee { get; set; }

        public virtual NeuroPi.UserManagment.Model.MTenant Tenant { get; set; }
    }
}