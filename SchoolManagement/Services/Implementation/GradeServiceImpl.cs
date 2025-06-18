using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Grade;

namespace SchoolManagement.Services.Implementation
{
    public class GradeServiceImpl : IGradeService
    {
        private readonly SchoolManagementDb _db;
        public GradeServiceImpl(SchoolManagementDb context)
        {
            _db = context;
        }

        public List<GradeResponseVM> GradesByTenantId(int tenantId)
        {
            return _db.Grades
                .Where(g=>g.TenantId == tenantId && !g.IsDeleted)
                .Select(GradeResponseVM.ToViewModel)
                .ToList();
        }
    }
}
