namespace SchoolManagement.ViewModel.CourseTeacher
{
    public class CourseTeacherUpdateVM
    {
        public int CourseId { get; set; }

        public int TeacherId { get; set; }

        public int BranchId { get; set; }

        public int TenantId { get; set; }
        
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
