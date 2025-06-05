using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Student;

namespace SchoolManagement.Services.Implementation
{
    public class StudentServiceImpl : IStudentService
    {
        private readonly SchoolManagementDb _context;
        public StudentServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public StudentResponseVM AddStudent(StudentRequestVM studentRequestVM)
        {
            var newStudent = StudentRequestVM.ToModel(studentRequestVM);
            newStudent.CreatedOn = DateTime.UtcNow;
            _context.Students.Add(newStudent);
            _context.SaveChanges();
            return StudentResponseVM.ToViewModel(newStudent);
        }

        public bool DeleteStudent(int Id, int tenantId)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == Id && s.TenantId == tenantId && !s.IsDeleted);
            
            if (student == null)
            {
                return false;
            }
            student.IsDeleted = true;
            student.UpdatedOn = DateTime.UtcNow;
            _context.Students.Update(student);
            _context.SaveChanges();
            return true;

        }

        public List<StudentResponseVM> GetAllStudents()
        {
            return _context.Students
                .Where(s => !s.IsDeleted)
                .Select(StudentResponseVM.ToViewModel)
                .ToList();
        }

        public StudentResponseVM GetStudentById(int Id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == Id && !s.IsDeleted);
            if (student == null)
            {
                return null; 
            }
            return StudentResponseVM.ToViewModel(student);
        }

        public StudentResponseVM GetStudentByTenantIdAndId(int tenantId, int Id)
        {
            var student = _context.Students
                .FirstOrDefault(s => s.Id == Id && s.TenantId == tenantId && !s.IsDeleted);
            if (student == null)
            {
                return null;
            }
            return StudentResponseVM.ToViewModel(student);
        }

        public List<StudentResponseVM> GetStudentsByTenantId(int tenantId)
        {
            var students = _context.Students
                .Where(s => s.TenantId == tenantId && !s.IsDeleted)
                .ToList();
            return StudentResponseVM.ToViewModeList(students);
        }

        public StudentResponseVM UpdateStudent(int Id, int tenantId, StudentUpdateVM UpdateVM)
        {
            var existingStudent = _context.Students
                .FirstOrDefault(s => s.Id == Id && s.TenantId == tenantId && !s.IsDeleted);
            if (existingStudent == null)
            {
                return null;
            }
            existingStudent.Name = UpdateVM.Name;
            existingStudent.CourseId = UpdateVM.CourseId;
            existingStudent.UpdatedBy = UpdateVM.UpdatedBy;
            existingStudent.UpdatedOn = DateTime.UtcNow;

            
            _context.SaveChanges();
            return StudentResponseVM.ToViewModel(existingStudent);


        }
    }
}
