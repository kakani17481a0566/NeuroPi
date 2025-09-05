using System;
using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.StudentRegistration
{
    public class StudentRegistrationRequestVM
    {
        // --- Required foreign keys ---
        public int TenantId { get; set; }
        public int BranchId { get; set; }
        public int? CourseId { get; set; }
        public int RegTypeId { get; set; }
        public int GenderId { get; set; }
        public int RegTransportId { get; set; }
        public int? AltTransportId { get; set; }

        // --- Registration details ---
        public DateTime? RegDate { get; set; }         // optional (DB default = CURRENT_DATE)
        public string? RegNumber { get; set; }

        // --- Student identity ---
        public string StuLastName { get; set; } = string.Empty;
        public string StuGivenName { get; set; } = string.Empty;
        public DateTime StuDob { get; set; }

        // --- Transport ---
        public string? OtherTransportText { get; set; }

        // --- Previous schooling ---
        public bool AttPreSchool { get; set; } = false;
        public string? PrevScName { get; set; }
        public bool PrevRegKindergarten { get; set; } = false;
        public string? PrevKindergarten1Nsc { get; set; }

        // --- Misc ---
        public bool SpeechTherapy { get; set; } = false;
        public bool Custody { get; set; } = false;
        public int? CustodyOfId { get; set; }
        public int? LivesWithId { get; set; }

        // --- Siblings ---
        public bool SiblingsInThisSchool { get; set; } = false;
        public string? SiblingsThisNames { get; set; }
        public bool SiblingsInOtherSchool { get; set; } = false;
        public string? SiblingsOtherNames { get; set; }

        // --- Audit ---
        public int CreatedBy { get; set; }

        // --- Mapper ---
        public static MStudentRegistration ToModel(StudentRegistrationRequestVM request)
        {
            return new MStudentRegistration
            {
                TenantId = request.TenantId,
                BranchId = request.BranchId,
                CourseId = request.CourseId,
                RegTypeId = request.RegTypeId,
                GenderId = request.GenderId,
                RegTransportId = request.RegTransportId,
                AltTransportId = request.AltTransportId,

                RegDate = request.RegDate ?? DateTime.UtcNow.Date,
                RegNumber = request.RegNumber,

                StuLastName = request.StuLastName,
                StuGivenName = request.StuGivenName,
                StuDob = request.StuDob,

                OtherTransportText = request.OtherTransportText,
                AttPreSchool = request.AttPreSchool,
                PrevScName = request.PrevScName,
                PrevRegKindergarten = request.PrevRegKindergarten,
                PrevKindergarten1Nsc = request.PrevKindergarten1Nsc,

                SpeechTherapy = request.SpeechTherapy,
                Custody = request.Custody,
                CustodyOfId = request.CustodyOfId,
                LivesWithId = request.LivesWithId,

                SiblingsInThisSchool = request.SiblingsInThisSchool,
                SiblingsThisNames = request.SiblingsThisNames,
                SiblingsInOtherSchool = request.SiblingsInOtherSchool,
                SiblingsOtherNames = request.SiblingsOtherNames,

                CreatedBy = request.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    }
}
