using System;

namespace NeuroPi.Nutrition.ViewModel.QrCode
{
    public class QrCodeValidationResponseVM
    {
        public string Status { get; set; } // "Valid", "AlreadyUsed", "NotFound", "Error"
        public string? StudentName { get; set; }
        public string VisitorName { get; set; }
        public string Gender { get; set; }
        
        public string? CourseName { get; set; }
        public string? BranchName { get; set; }
        public string? Batch { get; set; } // Map from AdmissionGrade

        public DateTime ValidationTime { get; set; }
        public string? Message { get; set; }
    }
}
