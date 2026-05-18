using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.EmployeeStudyPlan;

namespace SchoolManagement.Services.Implementation
{
    public class EmployeeStudyPlanServiceImpl : IEmployeeStudyPlanService
    {
        private readonly SchoolManagementDb _context;

        public EmployeeStudyPlanServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public EmployeeStudyPlanVm CreateEmployeeStudyPlan(EmployeeStudyPlanCreateVm vm)
        {
            var entity = new MEmployeeStudyPlan
            {
                EmployeeDetailsId = vm.EmployeeDetailsId,
                StudyPlanId = vm.StudyPlanId,
                TenantId = vm.TenantId,
                CreatedBy = vm.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };

            _context.Add(entity);
            _context.SaveChanges();

            return EmployeeStudyPlanVm.ToViewModel(entity);
        }

        public bool DeleteEmployeeStudyPlan(int id, int tenantId)
        {
            var entity = _context.Set<MEmployeeStudyPlan>().FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            if (entity == null) return false;

            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return true;
        }

        public List<EmployeeStudyPlanVm> GetAllEmployeeStudyPlans(int tenantId)
        {
            var list = _context.Set<MEmployeeStudyPlan>().Where(x => x.TenantId == tenantId && !x.IsDeleted).ToList();
            return EmployeeStudyPlanVm.ToViewModelList(list);
        }

        public EmployeeStudyPlanDetailVm? GetEmployeeStudyPlanById(int id, int tenantId)
        {
            var entity = _context.Set<MEmployeeStudyPlan>()
                .Include(x => x.StudyPlan)
                    .ThenInclude(sp => sp.StudyPlanSteps.Where(s => !s.IsDeleted).OrderBy(s => s.SeqOrd))
                .Include(x => x.StudyPlan)
                    .ThenInclude(sp => sp.StudyCoursesMaps.Where(m => !m.IsDeleted).OrderBy(m => m.SeqOrd))
                        .ThenInclude(m => m.StudyCourse)
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null) return null;

            return MapToDetail(entity);
        }

        public List<EmployeeStudyPlanDetailVm> GetEmployeeStudyPlansByEmployeeId(int employeeDetailsId, int tenantId)
        {
            var list = _context.Set<MEmployeeStudyPlan>()
                .Include(x => x.StudyPlan)
                    .ThenInclude(sp => sp.StudyPlanSteps.Where(s => !s.IsDeleted).OrderBy(s => s.SeqOrd))
                .Include(x => x.StudyPlan)
                    .ThenInclude(sp => sp.StudyCoursesMaps.Where(m => !m.IsDeleted).OrderBy(m => m.SeqOrd))
                        .ThenInclude(m => m.StudyCourse)
                .Where(x => x.EmployeeDetailsId == employeeDetailsId && x.TenantId == tenantId && !x.IsDeleted)
                .ToList();

            return list.Select(MapToDetail).ToList();
        }

        public List<EmployeeStudyPlanVm> GetEmployeeStudyPlansByPlanId(int studyPlanId, int tenantId)
        {
            var list = _context.Set<MEmployeeStudyPlan>().Where(x => x.StudyPlanId == studyPlanId && x.TenantId == tenantId && !x.IsDeleted).ToList();
            return EmployeeStudyPlanVm.ToViewModelList(list);
        }

        private static EmployeeStudyPlanDetailVm MapToDetail(MEmployeeStudyPlan entity)
        {
            return new EmployeeStudyPlanDetailVm
            {
                Id = entity.Id,
                EmployeeDetailsId = entity.EmployeeDetailsId,
                StudyPlanId = entity.StudyPlanId,
                TenantId = entity.TenantId,
                StudyPlan = entity.StudyPlan == null ? null : new StudyPlanDetailVm
                {
                    Id = entity.StudyPlan.Id,
                    Name = entity.StudyPlan.Name,
                    Description = entity.StudyPlan.Description,
                    Steps = entity.StudyPlan.StudyPlanSteps.Select(s => new StudyPlanStepDetailVm
                    {
                        Id = s.Id,
                        SeqOrd = s.SeqOrd,
                        Name = s.Name,
                        Description = s.Description
                    }).ToList(),
                    Courses = entity.StudyPlan.StudyCoursesMaps.Select(m => new StudyPlanCourseDetailVm
                    {
                        Id = m.Id,
                        StudyCoursesId = m.StudyCoursesId,
                        SeqOrd = m.SeqOrd,
                        CourseName = m.StudyCourse?.Name,
                        CourseDescription = m.StudyCourse?.Description,
                        CourseUrl = m.StudyCourse?.Url
                    }).ToList()
                }
            };
        }
    }
}
