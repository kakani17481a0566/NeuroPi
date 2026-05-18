using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.StudyCoursesMap;

namespace SchoolManagement.Services.Implementation
{
    public class StudyCoursesMapServiceImpl : IStudyCoursesMapService
    {
        private readonly SchoolManagementDb _context;

        public StudyCoursesMapServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public StudyCoursesMapVm CreateStudyCoursesMap(StudyCoursesMapCreateVm vm)
        {
            var entity = new MStudyCoursesMap
            {
                StudyPlanId = vm.StudyPlanId,
                StudyCoursesId = vm.StudyCoursesId,
                SeqOrd = vm.SeqOrd,
                TenantId = vm.TenantId,
                CreatedBy = vm.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };

            _context.Add(entity);
            _context.SaveChanges();

            return StudyCoursesMapVm.ToViewModel(entity);
        }

        public StudyCoursesMapVm UpdateStudyCoursesMap(int id, int tenantId, StudyCoursesMapUpdateVm vm)
        {
            var entity = _context.Set<MStudyCoursesMap>().FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            if (entity == null) return null;

            entity.SeqOrd = vm.SeqOrd;
            entity.UpdatedBy = vm.UpdatedBy;
            entity.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();

            return StudyCoursesMapVm.ToViewModel(entity);
        }

        public bool DeleteStudyCoursesMap(int id, int tenantId)
        {
            var entity = _context.Set<MStudyCoursesMap>().FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            if (entity == null) return false;

            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return true;
        }

        public List<StudyCoursesMapVm> GetAllStudyCoursesMaps(int tenantId)
        {
            var list = _context.Set<MStudyCoursesMap>().Where(x => x.TenantId == tenantId && !x.IsDeleted).ToList();
            return StudyCoursesMapVm.ToViewModelList(list);
        }

        public StudyCoursesMapVm GetStudyCoursesMapById(int id, int tenantId)
        {
            var entity = _context.Set<MStudyCoursesMap>().FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            return entity == null ? null : StudyCoursesMapVm.ToViewModel(entity);
        }

        public List<StudyCoursesMapVm> GetStudyCoursesMapsByPlanId(int studyPlanId, int tenantId)
        {
            var list = _context.Set<MStudyCoursesMap>().Where(x => x.StudyPlanId == studyPlanId && x.TenantId == tenantId && !x.IsDeleted).OrderBy(x => x.SeqOrd).ToList();
            return StudyCoursesMapVm.ToViewModelList(list);
        }
    }
}
