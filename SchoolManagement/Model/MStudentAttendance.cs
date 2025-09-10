using NeuroPi.UserManagment.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("student_attendance")]
    public class MStudentAttendance : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("student_id")]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public virtual MStudent Student { get; set; }

        //[Column("first_name")]
        //public string StudentName { get; set; }
        

        //[Column("name")]
        //public string CourseName { get; set; }
        //public virtual MCourse Course { get; set; }

        [Required]
        [Column("user_id")]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual MUser User { get; set; }

        [Required]
        [Column("date")]
        public DateOnly Date { get; set; }

        [Required]
        [Column("from_time")]
        public TimeSpan FromTime { get; set; }

        [Column("to_time")]
        public TimeSpan ToTime { get; set; }

        [Column("branch_id")]
        [ForeignKey("Branch")]
        public int? BranchId { get; set; }
        public virtual MBranch Branch { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
