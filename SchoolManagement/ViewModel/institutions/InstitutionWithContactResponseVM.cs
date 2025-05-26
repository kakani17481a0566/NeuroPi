using SchoolManagement.ViewModel.Contact;
using SchoolManagement.ViewModel.Institutions;

namespace SchoolManagement.ViewModel.institutions
{
    public class InstitutionWithContactResponseVM
    {
        public ContactResponseVM Contact { get; set; }
        public InstitutionResponseVM Institution { get; set; }
    }

}
