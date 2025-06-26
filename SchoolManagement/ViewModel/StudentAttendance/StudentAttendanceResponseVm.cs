using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.StudentAttendance
{
    public class StudentAttendanceResponseVm
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }

        public int? BranchId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int TenantId { get; set; }

        public static StudentAttendanceResponseVm ToViewModel(MStudentAttendance studentAttendance)
        {
            return new StudentAttendanceResponseVm
            {
                Id = studentAttendance.Id,
                StudentId = studentAttendance.StudentId,
                UserId = studentAttendance.UserId,
                Date = studentAttendance.Date,
                FromTime = studentAttendance.FromTime,
                ToTime = studentAttendance.ToTime,
                BranchId = studentAttendance.BranchId,
                CreatedBy = studentAttendance.CreatedBy,
                CreatedOn = studentAttendance.CreatedOn,
                UpdatedBy = studentAttendance.UpdatedBy,
                UpdatedOn = studentAttendance.UpdatedOn,
                TenantId = studentAttendance.TenantId
            };
        }

        public static List<StudentAttendanceResponseVm> ToViewModelList(List<MStudentAttendance> studentAttendanceList)
        {
            List<StudentAttendanceResponseVm> result = new List<StudentAttendanceResponseVm>();
            foreach (var studentAttendance in studentAttendanceList)
            {
                result.Add(ToViewModel(studentAttendance));
            }
            return result;
        }
    }

}
