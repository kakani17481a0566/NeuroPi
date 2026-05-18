using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.StudyPlanSteps;

namespace SchoolManagement.Services.Implementation
{
    public class StudyPlanStepsServiceImpl : IStudyPlanStepsService
    {
        private readonly SchoolManagementDb _context;

        public StudyPlanStepsServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public StudyPlanStepsVm CreateStudyPlanStep(StudyPlanStepsCreateVm vm)
        {
            var entity = new MStudyPlanSteps
            {
                StudyPlanId = vm.StudyPlanId,
                SeqOrd = vm.SeqOrd,
                Name = vm.Name,
                Description = vm.Description,
                TenantId = vm.TenantId,
                CreatedBy = vm.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };

            _context.Add(entity);
            _context.SaveChanges();

            return StudyPlanStepsVm.ToViewModel(entity);
        }

        public StudyPlanStepsVm UpdateStudyPlanStep(int id, int tenantId, StudyPlanStepsUpdateVm vm)
        {
            var entity = _context.Set<MStudyPlanSteps>().FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            if (entity == null) return null;

            entity.SeqOrd = vm.SeqOrd;
            entity.Name = vm.Name;
            entity.Description = vm.Description;
            entity.UpdatedBy = vm.UpdatedBy;
            entity.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();

            return StudyPlanStepsVm.ToViewModel(entity);
        }

        public bool DeleteStudyPlanStep(int id, int tenantId)
        {
            var entity = _context.Set<MStudyPlanSteps>().FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            if (entity == null) return false;

            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return true;
        }

        public List<StudyPlanStepsVm> GetAllStudyPlanSteps(int tenantId)
        {
            var list = _context.Set<MStudyPlanSteps>().Where(x => x.TenantId == tenantId && !x.IsDeleted).ToList();
            return StudyPlanStepsVm.ToViewModelList(list);
        }

        public StudyPlanStepsVm GetStudyPlanStepById(int id, int tenantId)
        {
            var entity = _context.Set<MStudyPlanSteps>().FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            return entity == null ? null : StudyPlanStepsVm.ToViewModel(entity);
        }

        public List<StudyPlanStepsVm> GetStudyPlanStepsByPlanId(int studyPlanId, int tenantId)
        {
            var list = _context.Set<MStudyPlanSteps>().Where(x => x.StudyPlanId == studyPlanId && x.TenantId == tenantId && !x.IsDeleted).OrderBy(x => x.SeqOrd).ToList();
            return StudyPlanStepsVm.ToViewModelList(list);
        }
    }
}
