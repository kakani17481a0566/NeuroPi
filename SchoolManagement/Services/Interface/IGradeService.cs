using SchoolManagement.ViewModel.Grade;

namespace SchoolManagement.Services.Interface
{
    public interface IGradeService
    {
        List<GradeResponseVM> GetAllGrades();

        List<GradeResponseVM> GetAllGradesByTenantId(int tenantId);

        GradeResponseVM GetGradeById(int id);

        GradeResponseVM GetGradeByIdAndTenantId(int id, int tenantId);

        GradeResponseVM CreateGrade(GradeRequestVM gradeRequest);

        GradeResponseVM UpdateGrade(int id, GradeUpdateVM gradeUpdate);

        bool DeleteGrade(int id);

    }
}
