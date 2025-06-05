using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Grade;

namespace SchoolManagement.Services.Implementation
{
    public class GradeServiceImpl : IGradeService
    {
        private readonly SchoolManagementDb _context;
        public GradeServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public GradeResponseVM CreateGrade(GradeRequestVM gradeRequest)
        {
            var newGrade = GradeRequestVM.ToModel(gradeRequest);
            newGrade.CreatedOn = DateTime.UtcNow;
            _context.Grades.Add(newGrade);
            _context.SaveChanges();
            return GradeResponseVM.ToViewModel(newGrade);


        }

        public bool DeleteGrade(int id)
        {
            var grade = _context.Grades
                .FirstOrDefault(g => g.Id == id && !g.IsDeleted);
            if (grade == null)
            {
                return false;
            }
            grade.IsDeleted = true;
            grade.UpdatedOn = DateTime.UtcNow;
            _context.Grades.Update(grade);
            _context.SaveChanges();
            return true;
        }

        public List<GradeResponseVM> GetAllGrades()
        {
            var grades = _context.Grades.ToList();
            return grades.Select(GradeResponseVM.ToViewModel).ToList();
        }

        public List<GradeResponseVM> GetAllGradesByTenantId(int tenantId)
        {
            var grades = _context.Grades.Where(g => g.TenantId == tenantId && !g.IsDeleted)
                .ToList();
            return grades.Select(GradeResponseVM.ToViewModel).ToList();

        }

        public GradeResponseVM GetGradeById(int id)
        {
            var grade = _context.Grades.FirstOrDefault(g => g.Id == id && !g.IsDeleted);
            if (grade == null)
            {
                return null;
            }
            return GradeResponseVM.ToViewModel(grade);
        }

        public GradeResponseVM GetGradeByIdAndTenantId(int id, int tenantId)
        {
            var grade = _context.Grades
                .FirstOrDefault(g => g.Id == id && g.TenantId == tenantId && !g.IsDeleted);
            if (grade == null)
            {
                return null;
            }
            return GradeResponseVM.ToViewModel(grade);
        }

        public GradeResponseVM UpdateGrade(int id, GradeUpdateVM gradeUpdate)
        {
            var existingGrade = _context.Grades
                .FirstOrDefault(g => g.Id == id && !g.IsDeleted);
            if (existingGrade == null)
            {
                return null;
            }
            existingGrade.Name = gradeUpdate.Name;
            existingGrade.MinPercentage = gradeUpdate.MinPercentage;
            existingGrade.MaxPercentage = gradeUpdate.MaxPercentage;
            existingGrade.Description = gradeUpdate.Description;
            existingGrade.UpdatedOn = DateTime.UtcNow;

            _context.Grades.Update(existingGrade);
            _context.SaveChanges();
            return GradeResponseVM.ToViewModel(existingGrade);
        }
    }
}
