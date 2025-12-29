using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.ViewModel.CourseTeacher
{
    public class AssignCourseTeacherVM
    {
        [Required]
        public int TeacherId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public int TenantId { get; set; }

        public int? CreatedBy { get; set; }
    }
}
