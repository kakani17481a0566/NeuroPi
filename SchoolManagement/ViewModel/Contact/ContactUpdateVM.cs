namespace SchoolManagement.ViewModel.Contact
{
    public class ContactUpdateVM
    {
        public string Name { get; set; } = string.Empty;
        public string PriNumber { get; set; } = string.Empty;
        public string? SecNumber { get; set; }
        public string? Email { get; set; }
        public string Address1 { get; set; } = string.Empty;
        public string? Address2 { get; set; }
        public string? State { get; set; }
        public string City { get; set; } = string.Empty;
        public string? Pincode { get; set; }

        public int UpdatedBy { get; set; }
    }
}
