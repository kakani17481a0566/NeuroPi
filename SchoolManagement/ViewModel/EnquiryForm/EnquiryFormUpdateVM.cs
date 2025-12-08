namespace SchoolManagement.ViewModel.EnquiryForm
{
    public class EnquiryFormUpdateVM
    {
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string? ContactNumber { get; set; }
        public string Email { get; set; }
        public bool IsAgreed { get; set; }
        public string? DigitalSignature { get; set; }

        public int TenantId { get; set; }

        public int UpdatedBy { get; set; }   // comes from frontend
    }
}
