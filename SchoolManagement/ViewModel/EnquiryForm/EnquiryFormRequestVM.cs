namespace SchoolManagement.ViewModel.EnquiryForm
{
    public class EnquiryFormRequestVM
    {
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string? ContactNumber { get; set; }
        public string Email { get; set; }
        public bool IsAgreed { get; set; }
        public string? DigitalSignature { get; set; }
        public int TenantId { get; set; }

        public int CreatedBy { get; set; } // frontend passes logged-in user ID
        
        public string? NdaBaseUrl { get; set; }
    }
}
