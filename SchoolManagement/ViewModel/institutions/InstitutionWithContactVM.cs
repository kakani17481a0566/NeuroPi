using SchoolManagement.ViewModel.Contact;

namespace SchoolManagement.ViewModel.Institutions
{
    public class InstitutionWithContactRequestVM
    {
        public ContactRequestVM Contact { get; set; }
        public string InstitutionName { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
    }

    public class InstitutionWithContactResponseVM
    {
        public ContactResponseVM Contact { get; set; }
        public InstitutionResponseVM Institution { get; set; }
    }
}
