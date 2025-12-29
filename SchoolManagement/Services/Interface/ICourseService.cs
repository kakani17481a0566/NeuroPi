using SchoolManagement.ViewModel.Course;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface ICourseService
    {
        // Create a course
        CourseVm CreateCourse(CourseCreateVm courseCreateVm);

        // Update a course
        CourseVm UpdateCourse(int id, int tenantId, CourseUpdateVm courseUpdateVm);

        // Delete a course by ID and Tenant ID
        bool DeleteCourseByIdAndTenantId(int id, int tenantId);

        // Get all courses
        List<CourseVm> GetAllCourses();

        // Get course by ID
        CourseVm GetCourseById(int id);

        // Get course by ID and Tenant ID
        CourseVm GetCourseByIdAndTenantId(int id, int tenantId);

        // Get all courses for a tenant
        List<CourseVm> GetCoursesByTenantId(int tenantId);

        List<CourseDropDownOptionsVm> GetCourseDropDownOptions(int tenantId);


    }
}
