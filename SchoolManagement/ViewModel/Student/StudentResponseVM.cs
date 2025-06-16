using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Students
{
    public class StudentResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }    // New
        public int TenantId { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }    // New

        public static StudentResponseVM ToViewModel(MStudent student)
        {
            return new StudentResponseVM
            {
                Id = student.Id,
                Name = student.Name,
                CourseId = student.CourseId,
                CourseName = student.Course?.Name,
                TenantId = student.TenantId,
                BranchId = student.BranchId,
                BranchName = student.Branch?.Name
            };
        }
    }
}
