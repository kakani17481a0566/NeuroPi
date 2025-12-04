using SchoolManagement.ViewModel.Subject;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface ISubjectService
    {
        List<SubjectResponseVM> GetAllSubjects();
        List<SubjectResponseVM> GetAllSubjects(int tenantId);
        SubjectResponseVM GetSubjectById(int id);
        SubjectResponseVM GetSubjectById(int id, int tenantId);
        SubjectResponseVM CreateSubject(SubjectRequestVM subject);
        SubjectResponseVM UpdateSubject(int id, int tenantId, SubjectUpdateVM subject);

        List<SubjectResponseVM> GetSubjectsByCourseIdAndTenantIt(int courseId, int tenantId);
        bool DeleteSubject(int id, int tenantId);
    }
}
