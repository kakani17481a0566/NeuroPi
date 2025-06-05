using SchoolManagement.ViewModel.Contact;
using SchoolManagement.ViewModel.institutions;
namespace SchoolManagement.ViewModel.Institutions
{
    public class InstitutionWithContactResponseVM
    {
        public ContactResponseVM Contact { get; set; }
        public InstitutionResponseVM Institution { get; set; }
    }

}
