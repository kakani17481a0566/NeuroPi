namespace SchoolManagement.ViewModel.Courses
{
    public class CoursesResponseVM
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public int? CourseTypeId { get; set; }
        public string CourseTypeName { get; set; }
        public string Duration { get; set; }
        public decimal? ApxFee { get; set; }
        public string Status { get; set; }
        public int TenantId { get; set; }
    }
}
