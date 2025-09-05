using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.StudentRegistration;
using System;

namespace SchoolManagement.Services.Implementation
{
    public class StudentRegistrationImpl : IStudentRegistration
    {
        private readonly SchoolManagementDb _context;

        public StudentRegistrationImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public StudentRegistrationResponseVM Create(StudentRegistrationRequestVM request)
        {
            // Map VM → Model
            var student = StudentRegistrationRequestVM.ToModel(request);

            // Save to DB
            _context.StudentRegistrations.Add(student);
            _context.SaveChanges();

            // Map Model → Response VM
            return new StudentRegistrationResponseVM
            {
                Id = student.Id,
                RegNumber = student.RegNumber ?? string.Empty,
                RegDate = student.RegDate ?? DateTime.UtcNow.Date,
                StuLastName = student.StuLastName,
                StuGivenName = student.StuGivenName,
                StuDob = student.StuDob,
                GenderId = student.GenderId,
                TenantId = student.TenantId,
                BranchId = student.BranchId,
                CourseId = student.CourseId,
                RegTypeId = student.RegTypeId,
                RegTransportId = student.RegTransportId,
                AltTransportId = student.AltTransportId,
                OtherTransportText = student.OtherTransportText,
                AttPreSchool = student.AttPreSchool,
                PrevScName = student.PrevScName,
                PrevRegKindergarten = student.PrevRegKindergarten,
                PrevKindergarten1Nsc = student.PrevKindergarten1Nsc,
                SpeechTherapy = student.SpeechTherapy,
                Custody = student.Custody,
                CustodyOfId = student.CustodyOfId,
                LivesWithId = student.LivesWithId,
                SiblingsInThisSchool = student.SiblingsInThisSchool,
                SiblingsThisNames = student.SiblingsThisNames,
                SiblingsInOtherSchool = student.SiblingsInOtherSchool,
                SiblingsOtherNames = student.SiblingsOtherNames,
                CreatedOn = student.CreatedOn,
                CreatedByName = request.CreatedBy.ToString() 
            };
        }
    }
}
