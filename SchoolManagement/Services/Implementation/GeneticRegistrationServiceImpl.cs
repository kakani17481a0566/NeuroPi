using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.GeneticRegistration;

namespace SchoolManagement.Services.Implementation
{
    public class GeneticRegistrationServiceImpl : IGeneticRegistrationService
    {
        private readonly SchoolManagementDb _context;
        public GeneticRegistrationServiceImpl(SchoolManagementDb context)
        {
            _context = context;
            
        }
        public string AddGeneticRegistration (GeneticRegistrationRequestVM request)
        {
            var geneticRegistartion=GeneticRegistrationRequestVM.ToModel(request);
            geneticRegistartion.CreatedAt = DateTime.UtcNow;
            _context.GeneticRegistrations.Add(geneticRegistartion);
             int  result=_context.SaveChanges();
            if (result == 0)
            {
                return "not inserted";
            }
            return "inserted";

        }
    }
}
