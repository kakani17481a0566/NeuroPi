using SchoolManagement.ViewModel.Grade;

namespace SchoolManagement.Services.Interface
{
    public interface IGradeService
    {
        List<GradeResponseVM> GradesByTenantId (int tenantId);
    }
}
