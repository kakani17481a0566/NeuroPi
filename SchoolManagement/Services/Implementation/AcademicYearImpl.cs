using Microsoft.EntityFrameworkCore;
using NeuroPi.UserManagment.Model;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.AcademicYear;
using SchoolManagement.ViewModel.Branch;
using SchoolManagement.ViewModel.CourseTeacher;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class AcademicYearImpl : IAcademicYear

    {
        private readonly SchoolManagementDb _context;
        public AcademicYearImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        
        public List<AcademicYearResponseVM> GetAllAcadamicYears()
        {
            var academicYears = _context.academicyear.ToList();
            return academicYears.Select(a=>new AcademicYearResponseVM()
            {
                Id = a.Id,
                Name = a.Name,
                StartDate = a.start_date,
                EndDate = a.EndDate,
                IsActive = a.IsActive       
            }).ToList();


        }

        public AcademicYearResponseVM GetAcademicYearById(int id)
        {
            var academicYear = _context.academicyear.FirstOrDefault(x => x.Id == id);
            if (academicYear == null) return null;

            return new AcademicYearResponseVM
            {
                Id = academicYear.Id,
                Name = academicYear.Name,
                StartDate = academicYear.start_date,
                EndDate = academicYear.EndDate,
                IsActive = academicYear.IsActive
            };
        }

        public AcademicYearResponseVM CreateAcademicYear(AcademicYearCreateVm model)
        {
            var academicYear = new MAcademicYear
            {
                Name = model.Name,
                Contact = model.Contact,
                start_date = model.StartDate,
                EndDate = model.EndDate,
                IsActive = model.IsActive
            };

            _context.academicyear.Add(academicYear);
            _context.SaveChanges();

            return new AcademicYearResponseVM
            {
                Id = academicYear.Id,
                Name = academicYear.Name,
                StartDate = academicYear.start_date,
                EndDate = academicYear.EndDate,
                IsActive = academicYear.IsActive
            };
        }

        public AcademicYearResponseVM UpdateAcademicYear(int id, AcademicYearUpdateVm model)
        {
            var academicYear = _context.academicyear.FirstOrDefault(x => x.Id == id);
            if (academicYear == null) return null;

            academicYear.Name = model.Name;
            academicYear.Contact = model.Contact;
            academicYear.start_date = model.StartDate;
            academicYear.EndDate = model.EndDate;
            academicYear.IsActive = model.IsActive;

            _context.academicyear.Update(academicYear);
            _context.SaveChanges();

            return new AcademicYearResponseVM
            {
                Id = academicYear.Id,
                Name = academicYear.Name,
                StartDate = academicYear.start_date,
                EndDate = academicYear.EndDate,
                IsActive = academicYear.IsActive
            };
        }

        public bool DeleteAcademicYear(int id)
        {
            var academicYear = _context.academicyear.FirstOrDefault(x => x.Id == id);
            if (academicYear == null) return false;

            _context.academicyear.Remove(academicYear);
            _context.SaveChanges();
            return true;
        }
    }
}
