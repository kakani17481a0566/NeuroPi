using System.Collections.Generic;
using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Students
{
    public class StudentResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public int TenantId { get; set; }
        public int BranchId { get; set; }

        public static StudentResponseVM ToViewModel(MStudent student)
        {
            return new StudentResponseVM
            {
                Id = student.Id,
                Name = student.Name,
                CourseId = student.CourseId,
                TenantId = student.TenantId,
                BranchId = student.BranchId
            };
        }

        public static List<StudentResponseVM> ToViewModelList(List<MStudent> students)
        {
            var list = new List<StudentResponseVM>();
            foreach (var student in students)
            {
                list.Add(ToViewModel(student));
            }
            return list;
        }
    }
}
