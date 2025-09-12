using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.CourseTeacher
{
    public class CourseTeacherRequestVM
    {
        public int CourseId { get; set; }

        public int TeacherId { get; set; }

        public int BranchId { get; set; }

        public int TenantId { get; set; }
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public MCourseTeacher ToModel()
        {
            return new MCourseTeacher
            {
                CourseId = this.CourseId,
                TeacherId = this.TeacherId,
                BranchId = this.BranchId,
                TenantId = this.TenantId,
                CreatedBy = this.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}
