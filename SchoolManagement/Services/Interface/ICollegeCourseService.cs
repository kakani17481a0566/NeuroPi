using SchoolManagement.ViewModel.CollegeCourse;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface ICollegeCourseService
    {
        List<CollegeCourseResponseVM> GetAllByTenantId(int tenantId);
    }
}
