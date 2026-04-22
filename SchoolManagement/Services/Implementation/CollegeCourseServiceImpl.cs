using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.CollegeCourse;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class CollegeCourseServiceImpl : ICollegeCourseService
    {
        private readonly SchoolManagementDb _context;

        public CollegeCourseServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public List<CollegeCourseResponseVM> GetAllByTenantId(int tenantId)
        {
            var result = _context.CollegeCourses
                .Where(c => c.TenantId == tenantId)
                .Include(c => c.College)
                .Include(c => c.Course)
                .Select(c => new CollegeCourseResponseVM
                {
                    Id = c.Id,
                    CollegeId = c.CollegeId,
                    CollegeName = c.College != null ? c.College.CollegeName : null,
                    CourseId = c.CourseId,
                    CourseName = c.Course != null ? c.Course.CourseName : null,
                    TenantId = c.TenantId
                })
                .ToList();

            return result;
        }
    }
}
