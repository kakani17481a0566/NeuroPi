using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Student
{
    public class StudentRequestVM
    {
        public string Name { get; set; }

        public int CourseId { get; set; }

        public int TenantId { get; set; }

        public int createdBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MStudent ToModel(StudentRequestVM requestVm)
        {
            return new MStudent()
            {
                Name = requestVm.Name,
                CourseId = requestVm.CourseId,
                TenantId = requestVm.TenantId,
                CreatedBy = requestVm.createdBy,
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}