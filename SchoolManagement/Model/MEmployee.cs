using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("employee")]
    public class MEmployee : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("emp_code")]
        public string EmpCode { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("designation_id")]
        public int DesignationId { get; set; }

        [Column("branch_id")]
        public int BranchId { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }
    }
}
