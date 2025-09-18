using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.StudentsEnquiry;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SchoolManagement.Services.Implementation
{
    public class StudentEnquiryImpl : IStudentsEnquiry
    {
        private readonly SchoolManagementDb _db;

        public StudentEnquiryImpl(SchoolManagementDb db)
        {
            _db = db;
        }

        public int CreateStudentEnquiry(StudentEnquiryRequestDataVM vm)
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
                return existingEnquiry.Id; // ✅ int
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
                Signature = DecodeBase64DataUrl(vm.Signature),
                StatusId = vm.StatusId,
                TenantId = vm.TenantId,
                BranchId = vm.BranchId,
                CreatedBy = vm.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            _db.StudentsEnquiries.Add(newEnquiry);
            _db.SaveChanges();

            return newEnquiry.Id; // ✅ int
        }

        public bool DeleteStudentEnquiryByIdAndTenant(int id, int tenantId)
        {
            var studentEnquiry = _db.StudentsEnquiries
                .FirstOrDefault(e => !e.IsDeleted && e.Id == id && e.TenantId == tenantId);

            if (studentEnquiry == null) return false;

            studentEnquiry.IsDeleted = true;
            studentEnquiry.UpdatedOn = DateTime.UtcNow;
            _db.SaveChanges();
            return true;
        }

        public List<StudentEnquiryResponseVM> GetAllStudentEnquiries()
        {
            return _db.StudentsEnquiries
                .Include(m => m.Gender)
                .Where(e => !e.IsDeleted)
                .AsEnumerable()
                .Select(StudentEnquiryResponseVM.ToViewModel)
                .ToList();
        }

        public List<StudentEnquiryResponseVM> GetStudentEnquiriesByTenant(int tenantId)
        {
            return _db.StudentsEnquiries
                .Include(m => m.Gender)
                .Where(e => !e.IsDeleted && e.TenantId == tenantId)
                .AsEnumerable()
                .Select(StudentEnquiryResponseVM.ToViewModel)
                .ToList();
        }

        public StudentEnquiryResponseVM GetStudentEnquiryById(int id)
        {
            var enquiry = _db.StudentsEnquiries
                .Include(m => m.Gender)
                .Include(e => e.ParentContact)
                .Include(e => e.MotherContact)
                .FirstOrDefault(e => !e.IsDeleted && e.Id == id);

            return enquiry != null ? StudentEnquiryResponseVM.ToViewModel(enquiry) : null;
        }

        public StudentEnquiryResponseVM GetStudentEnquiryByIdAndTenant(int id, int tenantId)
        {
            var enquiry = _db.StudentsEnquiries
                .Include(m => m.Gender)
                .Include(e => e.ParentContact)
                .Include(e => e.MotherContact)
                .FirstOrDefault(e => !e.IsDeleted && e.Id == id && e.TenantId == tenantId);

            return enquiry != null ? StudentEnquiryResponseVM.ToViewModel(enquiry) : null;
        }

        public StudentEnquiryResponseVM UpdateStudentEnquiry(int id, int tenantId, [FromBody] StudentEnquiryUpdateVM vm)
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
            enquiry.Signature = DecodeBase64DataUrl(vm.Signature);
            enquiry.StatusId = vm.StatusId;
            enquiry.BranchId = vm.BranchId;
            enquiry.UpdatedBy = vm.UpdatedBy;
            enquiry.UpdatedOn = DateTime.UtcNow;

            _db.SaveChanges();
            return StudentEnquiryResponseVM.ToViewModel(enquiry);
        }

        // -------- Helpers --------
        private static byte[]? DecodeBase64DataUrl(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            var commaIndex = input.IndexOf(',');
            var base64 = commaIndex >= 0 ? input.Substring(commaIndex + 1) : input;

            try
            {
                return Convert.FromBase64String(base64);
            }
            catch
            {
                return null;
            }
        }
    }
}
