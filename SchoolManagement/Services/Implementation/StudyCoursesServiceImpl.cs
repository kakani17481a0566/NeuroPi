using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.StudyCourses;

namespace SchoolManagement.Services.Implementation
{
    public class StudyCoursesServiceImpl : IStudyCoursesService
    {
        private readonly SchoolManagementDb _context;

        public StudyCoursesServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public StudyCoursesVm CreateStudyCourse(StudyCoursesCreateVm vm)
        {
            var entity = new MStudyCourses
            {
                Name = vm.Name,
                Description = vm.Description,
                Url = vm.Url,
                TenantId = vm.TenantId,
                CreatedBy = vm.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };

            _context.Add(entity);
            _context.SaveChanges();

            return StudyCoursesVm.ToViewModel(entity);
        }

        public StudyCoursesVm UpdateStudyCourse(int id, int tenantId, StudyCoursesUpdateVm vm)
        {
            var entity = _context.Set<MStudyCourses>().FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            if (entity == null) return null;

            entity.Name = vm.Name;
            entity.Description = vm.Description;
            entity.Url = vm.Url;
            entity.UpdatedBy = vm.UpdatedBy;
            entity.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();

            return StudyCoursesVm.ToViewModel(entity);
        }

        public bool DeleteStudyCourse(int id, int tenantId)
        {
            var entity = _context.Set<MStudyCourses>().FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            if (entity == null) return false;

            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return true;
        }

        public List<StudyCoursesVm> GetAllStudyCourses(int tenantId)
        {
            var list = _context.Set<MStudyCourses>().Where(x => x.TenantId == tenantId && !x.IsDeleted).ToList();
            return StudyCoursesVm.ToViewModelList(list);
        }

        public StudyCoursesVm GetStudyCourseById(int id, int tenantId)
        {
            var entity = _context.Set<MStudyCourses>().FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            return entity == null ? null : StudyCoursesVm.ToViewModel(entity);
        }
    }
}
