using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.StudyPlan;

namespace SchoolManagement.Services.Implementation
{
    public class StudyPlanServiceImpl : IStudyPlanService
    {
        private readonly SchoolManagementDb _context;

        public StudyPlanServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public StudyPlanVm CreateStudyPlan(StudyPlanCreateVm vm)
        {
            var entity = new MStudyPlan
            {
                Name = vm.Name,
                Description = vm.Description,
                TenantId = vm.TenantId,
                CreatedBy = vm.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };

            _context.Add(entity);
            _context.SaveChanges();

            return StudyPlanVm.ToViewModel(entity);
        }

        public StudyPlanVm UpdateStudyPlan(int id, int tenantId, StudyPlanUpdateVm vm)
        {
            var entity = _context.Set<MStudyPlan>().FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            if (entity == null) return null;

            entity.Name = vm.Name;
            entity.Description = vm.Description;
            entity.UpdatedBy = vm.UpdatedBy;
            entity.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();

            return StudyPlanVm.ToViewModel(entity);
        }

        public bool DeleteStudyPlan(int id, int tenantId)
        {
            var entity = _context.Set<MStudyPlan>().FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            if (entity == null) return false;

            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return true;
        }

        public List<StudyPlanVm> GetAllStudyPlans(int tenantId)
        {
            var list = _context.Set<MStudyPlan>().Where(x => x.TenantId == tenantId && !x.IsDeleted).ToList();
            return StudyPlanVm.ToViewModelList(list);
        }

        public StudyPlanVm GetStudyPlanById(int id, int tenantId)
        {
            var entity = _context.Set<MStudyPlan>().FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            return entity == null ? null : StudyPlanVm.ToViewModel(entity);
        }
    }
}
