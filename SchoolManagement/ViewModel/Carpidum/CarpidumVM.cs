using System;

namespace SchoolManagement.ViewModel.Carpidum
{
    public class CarpidumVM
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty; // Optional helper
        public string ParentType { get; set; } = string.Empty;
        public string? GuardianName { get; set; }
        public string QrCode { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public int TenantId { get; set; }
    }

    public class CarpidumRequestVM
    {
        public int StudentId { get; set; }
        public string ParentType { get; set; } = string.Empty; // Enforce enum values in service/controller
        public string? GuardianName { get; set; }
        public string QrCode { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
         public int TenantId { get; set; }
    }
}
