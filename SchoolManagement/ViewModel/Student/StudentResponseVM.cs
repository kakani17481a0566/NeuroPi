using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Student
{
    public class StudentResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CourseId { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public static StudentResponseVM ToViewModel(MStudent student)
        {
            return new StudentResponseVM
            {
                Id = student.Id,
                Name = student.Name,
                CourseId = student.CourseId,
                TenantId = student.TenantId,
                CreatedBy = student.CreatedBy,
                CreatedOn = student.CreatedOn,
                UpdatedBy = student.UpdatedBy,
                UpdatedOn = student.UpdatedOn
            };
        }

        public static List<StudentResponseVM> ToViewModeList(List<MStudent> studentList)
        {
            List<StudentResponseVM> result = new List<StudentResponseVM>();
            foreach (var student in studentList)
            {
                result.Add(ToViewModel(student));
            }
            return result;

        }
    }
}
