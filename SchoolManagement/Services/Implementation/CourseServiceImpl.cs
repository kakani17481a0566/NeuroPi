using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Course;

namespace SchoolManagement.Services.Implementation
{
    public class CourseServiceImpl : ICourseService
    {
        private readonly SchoolManagementDb _context;

        public CourseServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public List<CourseVM> GetCoursesByTenantId(int tenantId)
        {
            return _context.Set<MCourse>()
                .Where(c => c.TenantId == tenantId && !c.IsDeleted)
                .Select(c => new CourseVM
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    TenantId = c.TenantId
                })
                .OrderBy(c => c.Name)
                .ToList();
        }

        public CourseVM GetCourseById(int id, int tenantId)
        {
            var course = _context.Set<MCourse>()
                .FirstOrDefault(c => c.Id == id && c.TenantId == tenantId && !c.IsDeleted);

            if (course == null)
                return null;

            return new CourseVM
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                TenantId = course.TenantId
            };
        }
    }
}
