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
        
        // Added for Profile Update
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? BloodGroup { get; set; }
        public string? AdmissionNumber { get; set; }
        public string? AdmissionGrade { get; set; }
        public DateTime? DateOfJoining { get; set; }

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
                BranchName = student.Branch?.Name,
                
                // Map new fields
                DateOfBirth = student.DateOfBirth?.ToDateTime(TimeOnly.MinValue),
                Gender = student.Gender,
                BloodGroup = student.BloodGroup,
                AdmissionNumber = student.RegNumber,
                AdmissionGrade = student.AdmissionGrade,
                DateOfJoining = student.DateOfJoining?.ToDateTime(TimeOnly.MinValue)
            };
        }
    }
}
