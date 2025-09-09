using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.StudentsEnquiry;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Services.Implementation
{
    public class StudentEnquiryImpl : IStudentsEnquiry
    {
        private readonly SchoolManagementDb _db;

        public StudentEnquiryImpl(SchoolManagementDb db)
        {
            _db = db;
        }

        public long CreateStudentEnquiry(StudentEnquiryRequestDataVM vm)
        {
            
            var existingEnquiry = _db.StudentsEnquiries
                .Include(e => e.ParentContact)
                .FirstOrDefault(e =>
                    e.StudentFirstName == vm.StudentFirstName &&
                    e.StudentLastName == vm.StudentLastName &&
                    e.Dob == vm.Dob &&
                    e.TenantId == vm.TenantId &&
                    !e.IsDeleted &&
                    e.ParentContact.PriNumber == vm.ParentPhone
                );

            if (existingEnquiry != null)
            {
                
                return existingEnquiry.Id;
            }

            
            var parentContact = vm.ToParentContact();
            _db.Contacts.Add(parentContact);
            _db.SaveChanges();

            
            int? motherContactId = null;
            var motherContact = vm.ToMotherContact();
            if (motherContact != null)
            {
                _db.Contacts.Add(motherContact);
                _db.SaveChanges();
                motherContactId = motherContact.Id;
            }

            
            var newEnquiry = new MStudentsEnquiry
            {
                StudentFirstName = vm.StudentFirstName,
                StudentMiddleName = vm.StudentMiddleName,
                StudentLastName = vm.StudentLastName,
                Dob = vm.Dob,
                GenderId = vm.GenderId,
                AdmissionCourseId = vm.AdmissionCourseId,
                PreviousSchoolName = vm.PrevSchoolName,
                FromCourseId = vm.FromCourseId,
                FromYear = vm.FromYear,
                ToCourseId = vm.ToCourseId,
                ToYear = vm.ToYear,
                IsGuardian = vm.IsGuardian,
                ParentContactId = parentContact.Id,
                MotherContactId = motherContactId,
                HearAboutUsTypeId = vm.HearAboutUsTypeId,
                IsAgreedToTerms = vm.IsAgreedToTerms,
                Signature = vm.Signature,
                StatusId = vm.StatusId,
                TenantId = vm.TenantId,
                BranchId = vm.BranchId,
                CreatedBy = vm.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            _db.StudentsEnquiries.Add(newEnquiry);
            _db.SaveChanges();

            return newEnquiry.Id;
        }

        public bool DeleteStudentEnquiryByIdAndTenant(long id, int tenantId)
        {
            var studentEnquiry = _db.StudentsEnquiries.FirstOrDefault(e => !e.IsDeleted && e.Id == id && e.TenantId == tenantId);
                if (studentEnquiry == null)
            {
                return false;
            }
            studentEnquiry.IsDeleted = true;
            studentEnquiry.UpdatedOn = DateTime.UtcNow;
            _db.SaveChanges();
            return true;
                
        }

        public List<StudentEnquiryResponseVM> GetAllStudentEnquiries()
        {
            return _db.StudentsEnquiries.Include(m => m.Gender)
                .Where(e => !e.IsDeleted)
                .Select(e => new StudentEnquiryResponseVM
                {
                    Id = e.Id,
                    StudentFirstName = e.StudentFirstName,
                    StudentMiddleName = e.StudentMiddleName,
                    StudentLastName = e.StudentLastName,
                    Dob = e.Dob,
                    GenderId = e.GenderId,
                    GenderName=e.Gender.Name,
                    AdmissionCourseId = e.AdmissionCourseId,
                    PreviousSchoolName = e.PreviousSchoolName,
                    FromCourseId = e.FromCourseId,
                    FromYear = e.FromYear,
                    ToCourseId = e.ToCourseId,
                    ToYear = e.ToYear,
                    IsGuardian = e.IsGuardian,
                    ParentContactId = e.ParentContactId,
                    MotherContactId = e.MotherContactId,
                    HearAboutUsTypeId = e.HearAboutUsTypeId,
                    IsAgreedToTerms = e.IsAgreedToTerms,
                    Signature = e.Signature,
                    StatusId = e.StatusId,
                    TenantId = e.TenantId,
                    BranchId = e.BranchId,
                    CreatedBy = e.CreatedBy,
                    CreatedOn = e.CreatedOn,
                    UpdatedBy = e.UpdatedBy,
                    UpdatedOn = e.UpdatedOn
                }).ToList();
        }

        public List<StudentEnquiryResponseVM> GetStudentEnquiriesByTenant(int tenantId)
        {
            return _db.StudentsEnquiries
                .Include(m => m.Gender)
                .Where(e => !e.IsDeleted && e.TenantId == tenantId)
                .Select(e => new StudentEnquiryResponseVM
                {
                    Id = e.Id,
                    StudentFirstName = e.StudentFirstName,
                    StudentMiddleName = e.StudentMiddleName,
                    StudentLastName = e.StudentLastName,
                    Dob = e.Dob,
                    GenderId = e.GenderId,
                    GenderName = e.Gender.Name,
                    AdmissionCourseId = e.AdmissionCourseId,
                    PreviousSchoolName = e.PreviousSchoolName,
                    FromCourseId = e.FromCourseId,
                    FromYear = e.FromYear,
                    ToCourseId = e.ToCourseId,
                    ToYear = e.ToYear,
                    IsGuardian = e.IsGuardian,
                    ParentContactId = e.ParentContactId,
                    MotherContactId = e.MotherContactId,
                    HearAboutUsTypeId = e.HearAboutUsTypeId,
                    IsAgreedToTerms = e.IsAgreedToTerms,
                    Signature = e.Signature,
                    StatusId = e.StatusId,
                    TenantId = e.TenantId,
                    BranchId = e.BranchId,
                    CreatedBy = e.CreatedBy,
                    CreatedOn = e.CreatedOn,
                    UpdatedBy = e.UpdatedBy,
                    UpdatedOn = e.UpdatedOn
                }).ToList();
        }

        public StudentEnquiryResponseVM GetStudentEnquiryById(long id)
        {
            var enquiry = _db.StudentsEnquiries
                .Include(m =>m.Gender)
                .Include(e => e.ParentContact)
                .Include(e => e.MotherContact)
                .FirstOrDefault(e => !e.IsDeleted && e.Id == id);
            if (enquiry == null) return null;
            return StudentEnquiryResponseVM.ToViewModel(enquiry);
        }

        public StudentEnquiryResponseVM GetStudentEnquiryByIdAndTenant(long id, int tenantId)
        {
            var enquiry = _db.StudentsEnquiries
                .Include(m => m.Gender)
                .Include(e => e.ParentContact)
                .Include(e => e.MotherContact)
                .FirstOrDefault(e => !e.IsDeleted && e.Id == id && e.TenantId == tenantId);
            if (enquiry == null) return null;
            return StudentEnquiryResponseVM.ToViewModel(enquiry);
        }

        public StudentEnquiryResponseVM UpdateStudentEnquiry(long id, int tenantId,[FromBody] StudentEnquiryUpdateVM vm)
        {
            var enquiry = _db.StudentsEnquiries
                .Include(e => e.ParentContact)
                .Include(e => e.MotherContact)
                
                .FirstOrDefault(e => !e.IsDeleted && e.Id == id && e.TenantId == tenantId);
            if (enquiry == null) return null;
            enquiry.StudentFirstName = vm.StudentFirstName;
            enquiry.StudentMiddleName = vm.StudentMiddleName;
            enquiry.StudentLastName = vm.StudentLastName;
            enquiry.Dob = vm.Dob;
            enquiry.GenderId = vm.GenderId;
            enquiry.AdmissionCourseId = vm.AdmissionCourseId;
            enquiry.PreviousSchoolName = vm.PreviousSchoolName;
            enquiry.FromCourseId = vm.FromCourseId;
            enquiry.FromYear = vm.FromYear;
            enquiry.ToCourseId = vm.ToCourseId;
            enquiry.ToYear = vm.ToYear;
            enquiry.IsGuardian = vm.IsGuardian;
            enquiry.ParentContactId = vm.ParentContactId;
            enquiry.MotherContactId = vm.MotherContactId;
            enquiry.HearAboutUsTypeId = vm.HearAboutUsTypeId;
            enquiry.IsAgreedToTerms = vm.IsAgreedToTerms;
            enquiry.Signature = vm.Signature;
            enquiry.StatusId = vm.StatusId;
            enquiry.BranchId = vm.BranchId;
            enquiry.UpdatedBy = vm.UpdatedBy;
            enquiry.UpdatedOn = DateTime.UtcNow;
            _db.SaveChanges();
            return StudentEnquiryResponseVM.ToViewModel(enquiry);
        }
    }
}
