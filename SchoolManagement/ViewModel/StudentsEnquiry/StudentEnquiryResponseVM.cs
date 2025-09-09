// SchoolManagement/ViewModel/StudentsEnquiry/StudentEnquiryResponseVM.cs
using SchoolManagement.Model;
using System;
using System.Collections.Generic;

namespace SchoolManagement.ViewModel.StudentsEnquiry
{
    public class StudentEnquiryResponseVM
    {
        public long Id { get; set; }

        public string StudentFirstName { get; set; }
        public string? StudentMiddleName { get; set; }
        public string StudentLastName { get; set; }

        public DateTime? Dob { get; set; }
        public int? GenderId { get; set; }
        public string? GenderName { get; set; }

        public int AdmissionCourseId { get; set; }
        public string? PreviousSchoolName { get; set; }

        public int? FromCourseId { get; set; }
        public short? FromYear { get; set; }
        public int? ToCourseId { get; set; }
        public short? ToYear { get; set; }

        public bool IsGuardian { get; set; }
        public int ParentContactId { get; set; }
        public int? MotherContactId { get; set; }

        public int? HearAboutUsTypeId { get; set; }
        public bool IsAgreedToTerms { get; set; }

        // UI-friendly string (data URL like "data:image/png;base64,...")
        public string? Signature { get; set; }

        public int StatusId { get; set; }
        public int TenantId { get; set; }
        public int? BranchId { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public static StudentEnquiryResponseVM ToViewModel(MStudentsEnquiry enquiry)
        {
            return new StudentEnquiryResponseVM
            {
                Id = enquiry.Id,
                StudentFirstName = enquiry.StudentFirstName,
                StudentMiddleName = enquiry.StudentMiddleName,
                StudentLastName = enquiry.StudentLastName,
                Dob = enquiry.Dob,
                GenderId = enquiry.GenderId,
                GenderName = enquiry.Gender?.Name, // null-safe

                AdmissionCourseId = enquiry.AdmissionCourseId,
                PreviousSchoolName = enquiry.PreviousSchoolName,
                FromCourseId = enquiry.FromCourseId,
                FromYear = enquiry.FromYear,
                ToCourseId = enquiry.ToCourseId,
                ToYear = enquiry.ToYear,

                IsGuardian = enquiry.IsGuardian,
                ParentContactId = enquiry.ParentContactId,
                MotherContactId = enquiry.MotherContactId,
                HearAboutUsTypeId = enquiry.HearAboutUsTypeId,
                IsAgreedToTerms = enquiry.IsAgreedToTerms,

                // FIX: convert byte[] -> data URL string
                Signature = ToDataUrl(enquiry.Signature),

                StatusId = enquiry.StatusId,
                TenantId = enquiry.TenantId,
                BranchId = enquiry.BranchId,
                CreatedBy = enquiry.CreatedBy,
                CreatedOn = enquiry.CreatedOn,
                UpdatedBy = enquiry.UpdatedBy,
                UpdatedOn = enquiry.UpdatedOn
            };
        }

        public List<StudentEnquiryResponseVM> ToViewModelList(List<MStudentsEnquiry> enquiryList)
        {
            var result = new List<StudentEnquiryResponseVM>();
            foreach (var enquiry in enquiryList)
                result.Add(ToViewModel(enquiry));
            return result;
        }

        private static string? ToDataUrl(byte[]? bytes, string mime = "image/png")
        {
            if (bytes == null || bytes.Length == 0) return null;
            var b64 = Convert.ToBase64String(bytes);
            return $"data:{mime};base64,{b64}";
        }
    }
}
