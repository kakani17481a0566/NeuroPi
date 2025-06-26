using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.StudentAttendance
{
    public class StudentAttendanceRequestVM
    {
        public int StudentId { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }

        public int? BranchId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int TenantId { get; set; }


        public static MStudentAttendance ToModel(StudentAttendanceRequestVM request)
        {
            return new MStudentAttendance
            {
                StudentId = request.StudentId,
                UserId = request.UserId,
                Date = request.Date,
                FromTime = request.FromTime,
                ToTime = request.ToTime,
                BranchId = request.BranchId,
                CreatedBy = request.CreatedBy,
                CreatedOn = request.CreatedOn,
                TenantId = request.TenantId
            };
        }
    }
}