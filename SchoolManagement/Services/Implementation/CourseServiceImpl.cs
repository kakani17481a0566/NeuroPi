using NeuroPi.UserManagment.Model;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Course;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class CourseServiceImpl : ICourseService
    {
        private readonly SchoolManagementDb _dbContext;

        public CourseServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }

        // Create a new course
        public CourseVm CreateCourse(CourseCreateVm courseCreateVm)
        {
            var course = new MCourse
            {
                Name = courseCreateVm.Name,
                Description = courseCreateVm.Description,
                TenantId = courseCreateVm.TenantId,
                CreatedBy = courseCreateVm.CreatedBy, 
                CreatedOn = DateTime.UtcNow
            };

            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();

            return new CourseVm
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                TenantId = course.TenantId
            };
        }

        // Update a course
        public CourseVm UpdateCourse(int id, int tenantId, CourseUpdateVm courseUpdateVm)
        {
            var course = _dbContext.Courses.FirstOrDefault(c => c.Id == id && c.TenantId == tenantId && !c.IsDeleted);
            if (course == null)
                return null;

            course.Name = courseUpdateVm.Name;
            course.Description = courseUpdateVm.Description;
            course.UpdatedOn = DateTime.UtcNow;
            course.UpdatedBy = courseUpdateVm.UpdatedBy; 

            _dbContext.SaveChanges();

            return new CourseVm
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                TenantId = course.TenantId
            };
        }

        // Delete a course (soft delete)
        public bool DeleteCourseByIdAndTenantId(int id, int tenantId)
        {
            var course = _dbContext.Courses.FirstOrDefault(c => c.Id == id && c.TenantId == tenantId && !c.IsDeleted);
            if (course == null)
                return false;

            course.IsDeleted = true;
            course.UpdatedOn = DateTime.UtcNow;
            _dbContext.SaveChanges();

            return true;
        }

        // Get all courses
        public List<CourseVm> GetAllCourses()
        {
            var result= _dbContext.Courses.Where(c => !c.IsDeleted).ToList();
            if(result==null || result.Count == 0)
            {
                return null;
            }
            return CourseVm.ToViewModelList(result);

        }

        // Get a course by ID
        public CourseVm GetCourseById(int id)
        {
            var course = _dbContext.Courses.FirstOrDefault(c => c.Id == id && !c.IsDeleted);
            if (course == null)
                return null;

            return new CourseVm
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                TenantId = course.TenantId
            };
        }

        // Get a course by ID and Tenant ID
        public CourseVm GetCourseByIdAndTenantId(int id, int tenantId)
        {
            var course = _dbContext.Courses.FirstOrDefault(c => c.Id == id && c.TenantId == tenantId && !c.IsDeleted);
            if (course == null)
                return null;

            return new CourseVm
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                TenantId = course.TenantId
            };
        }

        // Get all courses for a specific tenant
        public List<CourseVm> GetCoursesByTenantId(int tenantId)
        {
            return _dbContext.Courses
                .Where(c => c.TenantId == tenantId && !c.IsDeleted)
                .Select(c => new CourseVm
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    TenantId = c.TenantId
                }).ToList();
        }
    }
}
