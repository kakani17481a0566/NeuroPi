using SchoolManagement.ViewModel.GeneticRegistration;

namespace SchoolManagement.Services.Interface
{
    public interface IGeneticRegistrationService
    {
        string AddGeneticRegistration(GeneticRegistrationRequestVM request);

        GeneticRegistrationResponseVM GetGeneticRegistrationByGeneticIdOrRegistrationNumber(string GeneticId,string RegistrationNumber);

        GeneticRegistrationResponseVM GetGeneticRegistrationByUserId(int userId);

        List<GeneticRegistrationResponseVM> GetAllSubmissions(bool includeDeleted = true);

        List<GeneticRegistrationResponseVM> GetAllUserSubmissions(int userId, bool includeDeleted = false);

        bool DeleteSubmission(string registrationNumber, int userId);

        GeneticDashboardStatsVM GetDashboardStats();
    }
}
