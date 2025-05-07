using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.Model;

namespace NeuroPi.Models
{
    [Table("user_departments")]
    public class MUserDepartment : MBaseModel
    {
        [Column("user_id")]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Column("department_id")]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        // Navigation properties
        public virtual MUser User { get; set; }
        public virtual MDepartment Department { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}