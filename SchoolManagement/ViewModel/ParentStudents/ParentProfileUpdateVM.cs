using System;

namespace SchoolManagement.ViewModel.ParentStudents
{
    public class ParentProfileUpdateVM
    {
        public int UserId { get; set; }
        public int TenantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? WeddingAnniversaryDate { get; set; }
        public string SpouseName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public int UpdatedBy { get; set; }
    }
}
