using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.StudentsEnquiry;
using System;

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
            // Create Parent Contact
            var parentContact = vm.ToParentContact();
            _db.Contacts.Add(parentContact);
            _db.SaveChanges();

            // Create Mother Contact (optional)
            int? motherContactId = null;
            var motherContact = vm.ToMotherContact();
            if (motherContact != null)
            {
                _db.Contacts.Add(motherContact);
                _db.SaveChanges();
                motherContactId = motherContact.Id;
            }

            // Create Student Enquiry
            var enquiry = new MsStudentsEnquiry
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

            _db.StudentsEnquiries.Add(enquiry);
            _db.SaveChanges();

            return enquiry.Id;
        }
    }
}
