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

        [Required]
        [Column("user_id")]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual MUser User { get; set; }

        [Required]
        [Column("date")]
        public DateTime Date { get; set; }

        [Required]
        [Column("from_time")]
        public TimeSpan FromTime { get; set; }

        [Required]
        [Column("to_time")]
        public TimeSpan ToTime { get; set; }

        [Column("branch_id")]
        [ForeignKey("Branch")]
        public int? BranchId { get; set; }
        public virtual MBranch Branch { get; set; }
    }
}
