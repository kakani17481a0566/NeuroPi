using SchoolManagement.ViewModel.GeneticRegistration;

namespace SchoolManagement.Services.Interface
{
    public interface IGeneticRegistrationService
    {
        string AddGeneticRegistration(GeneticRegistrationRequestVM request);
    }
}
