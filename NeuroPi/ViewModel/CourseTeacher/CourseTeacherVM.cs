namespace NeuroPi.ViewModel.CourseTeacher
{
    public class CourseTeacherVM
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
