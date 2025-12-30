using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.CourseTeacher
{
    public class CourseTeacherResponseVM
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        
        public int TeacherId { get; set; }
       
        public int BranchId { get; set; }
        
        public int TenantId { get; set; }

        public string? CourseName { get; set; }
        public string? BranchName { get; set; }
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public static CourseTeacherResponseVM ToViewModel(MCourseTeacher model)
        {
            return new CourseTeacherResponseVM
            {
                Id = model.Id,
                CourseId = model.CourseId,
                TeacherId = model.TeacherId,
                BranchId = model.BranchId,
                TenantId = model.TenantId,
                CourseName = model.Course?.Name,
                BranchName = model.Branch?.Name,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn
            };
        }

        public static List<CourseTeacherResponseVM> ToViewModelList(List<MCourseTeacher> modelList)
        {
            List<CourseTeacherResponseVM> result = new List<CourseTeacherResponseVM>();
            foreach (var model in modelList)
            {
                result.Add(ToViewModel(model));
            }
            return result;
        }
    }
}
