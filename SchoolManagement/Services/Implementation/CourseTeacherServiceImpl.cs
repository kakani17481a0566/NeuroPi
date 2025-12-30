
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.CourseTeacher;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Services.Implementation
{
    public class CourseTeacherServiceImpl : ICourseTeacherService
    {
        private readonly SchoolManagementDb _context;

        public CourseTeacherServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public CourseTeacherResponseVM CreateCourseTeacher(CourseTeacherRequestVM courseTeacherRequestVM)
        {
            var newCourseTeacher = courseTeacherRequestVM.ToModel();
            newCourseTeacher.CreatedOn = DateTime.Now;
            _context.CourseTeachers.Add(newCourseTeacher);
            _context.SaveChanges();
            return CourseTeacherResponseVM.ToViewModel(newCourseTeacher);
        }

        public bool DeleteCourseTeacherByIdAndTenant(int id, int tenantId)
        {
            var courseTeacher = _context.CourseTeachers.FirstOrDefault(c=>!c.IsDeleted && c.TenantId == tenantId && c.Id==id);
            if (courseTeacher == null) return false;
            courseTeacher.IsDeleted = true;
            courseTeacher.UpdatedOn = DateTime.Now;
            _context.SaveChanges();
            return true;
        }

        public List<CourseTeacherResponseVM> GetAllCourseTeachers()
        {
            return _context.CourseTeachers
                 .Where(c => !c.IsDeleted)
                 .Select(c => new CourseTeacherResponseVM
                 {
                    Id = c.Id,
                    CourseId = c.CourseId,
                    TeacherId = c.TeacherId,
                    BranchId = c.BranchId,
                    TenantId = c.TenantId,
                    CreatedBy = c.CreatedBy,
                    CreatedOn = c.CreatedOn,
                    UpdatedBy = c.UpdatedBy,
                    UpdatedOn = c.UpdatedOn,
                 }).ToList();

        }

        public CourseTeacherResponseVM GetCourseTeacherById(int id)
        {
            var courseTeacher = _context.CourseTeachers.FirstOrDefault(c => !c.IsDeleted && c.Id == id);
            if (courseTeacher == null) return null;
            return CourseTeacherResponseVM.ToViewModel(courseTeacher);
            

        }

        public CourseTeacherResponseVM GetCourseTeacherByIdAndTenant(int id, int tenantId)
        {
            var courseTeacher = _context.CourseTeachers.FirstOrDefault(c => !c.IsDeleted && c.Id == id && c.TenantId == tenantId);
            if (courseTeacher == null) return null;
            return CourseTeacherResponseVM.ToViewModel(courseTeacher);
        }

        public List<CourseTeacherResponseVM> GetCourseTeachersByTenant(int tenantId)
        {
            return _context.CourseTeachers
                .Where(c=> !c.IsDeleted && c.TenantId == tenantId)  
                .Select(c => new CourseTeacherResponseVM
                {
                    Id = c.Id,
                    CourseId = c.CourseId,
                    TeacherId = c.TeacherId,
                    BranchId = c.BranchId,
                    TenantId = c.TenantId,
                    CreatedBy = c.CreatedBy,
                    CreatedOn = c.CreatedOn,
                    UpdatedBy = c.UpdatedBy,
                    UpdatedOn = c.UpdatedOn,
                }).ToList();

        }

        public CourseTeacherResponseVM UpdateCourseTeacher(int id, int tenantId, CourseTeacherUpdateVM courseTeacherUpdateVM)
        {
            var existingCourseTeacher = _context.CourseTeachers.FirstOrDefault(c => !c.IsDeleted && c.Id == id && c.TenantId == tenantId);
            if (existingCourseTeacher == null) return null;
            existingCourseTeacher.CourseId = courseTeacherUpdateVM.CourseId;
            existingCourseTeacher.TeacherId = courseTeacherUpdateVM.TeacherId;
            existingCourseTeacher.BranchId = courseTeacherUpdateVM.BranchId;
            existingCourseTeacher.UpdatedBy = courseTeacherUpdateVM.UpdatedBy;
            existingCourseTeacher.UpdatedOn = DateTime.Now;
            _context.SaveChanges();
            return CourseTeacherResponseVM.ToViewModel(existingCourseTeacher);  

        }

        public List<CourseTeacherResponseVM> GetCourseTeachersByTeacherId(int teacherId, int tenantId)
        {
            return _context.CourseTeachers
                .Include(x => x.Course)
                .Include(x => x.Branch)
                .Where(c => !c.IsDeleted && c.TenantId == tenantId && c.TeacherId == teacherId)
                .Select(c => new CourseTeacherResponseVM
                {
                    Id = c.Id,
                    CourseId = c.CourseId,
                    TeacherId = c.TeacherId,
                    BranchId = c.BranchId,
                    TenantId = c.TenantId,
                    CourseName = c.Course.Name,
                    BranchName = c.Branch.Name,
                    CreatedBy = c.CreatedBy,
                    CreatedOn = c.CreatedOn,
                    UpdatedBy = c.UpdatedBy,
                    UpdatedOn = c.UpdatedOn,
                }).ToList();
        }
    }   
}
