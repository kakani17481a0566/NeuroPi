using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Courses;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class CoursesServiceImpl : ICoursesService
    {
        private readonly SchoolManagementDb _context;

        public CoursesServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public List<CoursesResponseVM> GetAllByTenantId(int tenantId)
        {
            var result = _context.SchoolCourses
                .Where(c => c.TenantId == tenantId)
                .Include(c => c.CourseType)
                .Select(c => new CoursesResponseVM
                {
                    Id = c.Id,
                    CourseName = c.CourseName,
                    CourseCode = c.CourseCode,
                    CourseTypeId = c.CourseTypeId,
                    CourseTypeName = c.CourseType != null ? c.CourseType.Name : null,
                    Duration = c.Duration,
                    ApxFee = c.ApxFee,
                    Status = c.Status,
                    TenantId = c.TenantId
                })
                .ToList();

            return result;
        }
    }
}
