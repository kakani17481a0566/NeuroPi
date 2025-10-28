using SchoolManagement.ViewModel.CourseTeacher;
using SchoolManagement.ViewModel.ParentStudents;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IParentStudentsService
    {
        List<ParentStudentResponseVM> GetAll();
        List<ParentStudentResponseVM> GetAllByTenantId(int tenantId);

        ParentStudentResponseVM GetById(int id);
        ParentStudentResponseVM GetByIdAndTenantId(int id, int tenantId);

        ParentStudentResponseVM Create(ParentStudentRequestVM request);
        ParentStudentResponseVM UpdateByIdAndTenantId(int id, int tenantId, ParentStudentUpdateVM request);
        ParentStudentResponseVM DeleteByIdAndTenantId(int id, int tenantId);

        ParentWithStudentsResponseVM GetFullParentDetailsByUserId(int userId, int tenantId);

        List<ParentWithStudentsResponseVM> GetAllParentsWithStudentsByTenantIdAndBranchIdAndCourseId(int tenantId, int courseId, int branchId);



        CourseTeacherVM GetParentDetails(int userId, int tenantId);
    }
}
