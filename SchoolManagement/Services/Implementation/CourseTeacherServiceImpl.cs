using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.CourseTeacher;
using SchoolManagement.Model;

namespace SchoolManagement.Services.Implementation
{
    public class CourseTeacherServiceImpl : ICourseTeacherService
    {
        private readonly SchoolManagementDb _context;

        public CourseTeacherServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public List<CourseTeacherVM> GetCoursesByTeacherId(int teacherId, int tenantId)
        {
            var sql = @"
                SELECT 
                    ct.id,
                    ct.course_id as CourseId,
                    c.name as CourseName,
                    c.description as CourseDescription,
                    ct.branch_id as BranchId,
                    b.name as BranchName,
                    b.address as BranchAddress,
                    ct.teacher_id as TeacherId,
                    CONCAT(u.first_name, ' ', u.last_name) as TeacherName,
                    ct.created_on as CreatedOn,
                    ct.is_deleted as IsDeleted
                FROM course_teacher ct
                INNER JOIN course c ON ct.course_id = c.id
                INNER JOIN branch b ON ct.branch_id = b.id
                INNER JOIN users u ON ct.teacher_id = u.user_id
                WHERE ct.teacher_id = {0} 
                    AND ct.tenant_id = {1} 
                    AND ct.is_deleted = false
                ORDER BY ct.created_on DESC";

            return _context.Database
                .SqlQueryRaw<CourseTeacherVM>(sql, teacherId, tenantId)
                .ToList();
        }

        public CourseTeacherVM AssignCourseToTeacher(AssignCourseTeacherVM model)
        {
            // Check if assignment already exists
            var existing = _context.Set<MCourseTeacher>()
                .FirstOrDefault(ct => 
                    ct.TeacherId == model.TeacherId &&
                    ct.CourseId == model.CourseId &&
                    ct.BranchId == model.BranchId &&
                    ct.TenantId == model.TenantId &&
                    !ct.IsDeleted);

            if (existing != null)
            {
                throw new Exception("This course-branch assignment already exists for this teacher");
            }

            var courseTeacher = new MCourseTeacher
            {
                TeacherId = model.TeacherId,
                CourseId = model.CourseId,
                BranchId = model.BranchId,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy ?? model.TeacherId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.Set<MCourseTeacher>().Add(courseTeacher);
            _context.SaveChanges();

            // Return the created assignment with full details
            var assignments = GetCoursesByTeacherId(model.TeacherId, model.TenantId);
            return assignments.FirstOrDefault(a => a.Id == courseTeacher.Id);
        }

        public bool RemoveCourseFromTeacher(int id, int tenantId)
        {
            var courseTeacher = _context.Set<MCourseTeacher>()
                .FirstOrDefault(ct => ct.Id == id && ct.TenantId == tenantId && !ct.IsDeleted);

            if (courseTeacher == null)
                return false;

            // Soft delete
            courseTeacher.IsDeleted = true;
            courseTeacher.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();

            return true;
        }
    }
}
