using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.Model;

namespace NeuroPi.Models
{
    [Table("user_departments")]
    public class MUserDepartment : MBaseModel
    {
        [Key]  // Indicates that this column will be the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
        [Column("user_dept_id")]
        public int UserDeptId { get; set; }

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
