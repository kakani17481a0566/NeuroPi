namespace SchoolManagement.ViewModel.CollegeDetail
{
    public class CollegeDetailResponseVM
    {
        public int Id { get; set; }
        public string CollegeName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public int TenantId { get; set; }
    }
}
