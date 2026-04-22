using SchoolManagement.ViewModel.Courses;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface ICoursesService
    {
        List<CoursesResponseVM> GetAllByTenantId(int tenantId);
    }
}
