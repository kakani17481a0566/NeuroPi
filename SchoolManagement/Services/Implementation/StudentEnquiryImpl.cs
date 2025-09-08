using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.StudentsEnquiry;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            // STEP 1: Check if enquiry already exists for same student & phone
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
                // Skip insert if already exists
                return existingEnquiry.Id;
            }

            // STEP 2: Insert Parent Contact
            var parentContact = vm.ToParentContact();
            _db.Contacts.Add(parentContact);
            _db.SaveChanges();

            // STEP 3: Insert Mother Contact (optional)
            int? motherContactId = null;
            var motherContact = vm.ToMotherContact();
            if (motherContact != null)
            {
                _db.Contacts.Add(motherContact);
                _db.SaveChanges();
                motherContactId = motherContact.Id;
            }

            // STEP 4: Insert Student Enquiry
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
    }
}
