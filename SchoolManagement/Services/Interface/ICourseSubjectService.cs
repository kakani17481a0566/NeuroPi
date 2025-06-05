using SchoolManagement.ViewModel.CourseSubject;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface ICourseSubjectService
    {

        List<CourseSubjectResponseVM> GetAll();

        List<CourseSubjectResponseVM> GetAll(int tenantId);

      
        CourseSubjectResponseVM GetById(int id, int tenantId);

        
        CourseSubjectResponseVM GetById(int id);

        CourseSubjectResponseVM Create(CourseSubjectRequestVM request);
        CourseSubjectResponseVM Update(int id, int tenantId, CourseSubjectUpdateVM request);
        bool Delete(int id, int tenantId);
    }
}
