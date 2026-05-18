namespace SchoolManagement.ViewModel.EmployeeStudyPlan
{
    public class EmployeeStudyPlanDetailVm
    {
        public int Id { get; set; }
        public int EmployeeDetailsId { get; set; }
        public int StudyPlanId { get; set; }
        public int TenantId { get; set; }
        public StudyPlanDetailVm? StudyPlan { get; set; }
    }

    public class StudyPlanDetailVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<StudyPlanStepDetailVm> Steps { get; set; } = new();
        public List<StudyPlanCourseDetailVm> Courses { get; set; } = new();
    }

    public class StudyPlanStepDetailVm
    {
        public int Id { get; set; }
        public int? SeqOrd { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class StudyPlanCourseDetailVm
    {
        public int Id { get; set; }
        public int StudyCoursesId { get; set; }
        public int? SeqOrd { get; set; }
        public string? CourseName { get; set; }
        public string? CourseDescription { get; set; }
        public string? CourseUrl { get; set; }
    }
}
