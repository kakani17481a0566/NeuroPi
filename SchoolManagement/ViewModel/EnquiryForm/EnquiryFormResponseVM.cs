using System;

namespace SchoolManagement.ViewModel.EnquiryForm
{
    public class EnquiryFormResponseVM
    {
        public Guid Uuid { get; set; }

        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string? ContactNumber { get; set; }
        public string Email { get; set; }

        public bool IsAgreed { get; set; }
        public string? DigitalSignature { get; set; }
        public DateTime? AgreedOn { get; set; }

        public int TenantId { get; set; }

        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
