using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.GeneticRegistration;
using System.Text.RegularExpressions;

namespace SchoolManagement.Services.Implementation
{
    public class GeneticRegistrationServiceImpl : IGeneticRegistrationService
    {
        private readonly SchoolManagementDb _context;
        public GeneticRegistrationServiceImpl(SchoolManagementDb context)
        {
            _context = context;

        }
        public string AddGeneticRegistration(GeneticRegistrationRequestVM request)
        {
            var geneticRegistartion = GeneticRegistrationRequestVM.ToModel(request);
            string geneticId = "";
            geneticId += request.Country.ToUpper().Trim().Substring(0, 3) + "/";
            string stateCode = "";
            if (request.State.Contains(" "))
            {
                string[] stateArray = request.State.Split(' ');

                foreach (string state in stateArray)
                {
                    stateCode += state.ToUpper().Trim().Substring(0, 1);
                }
            }
            else
            {
                stateCode = request.State.ToUpper().Trim().Substring(0, 3);
            }
            geneticId += stateCode + "/";
            geneticId += ExtractNumbers(DateTime.Now.ToString());
            geneticRegistartion.GeneticId = geneticId;
            geneticRegistartion.CreatedAt = DateTime.UtcNow;
            _context.GeneticRegistrations.Add(geneticRegistartion);
            int result = _context.SaveChanges();
            if (result == 0)
            {
                return "not inserted";
            }
            return "inserted";

        }
        public static string ExtractNumbers(string input)
        {
            return Regex.Replace(input, @"\D", "");
        }

        public GeneticRegistrationResponseVM GetGeneticRegistrationByGeneticIdOrRegistrationNumber(string GeneticId, string RegistrationNumber)
        {
            var geneticRegistration = _context.GeneticRegistrations
                .FirstOrDefault(gr => gr.GeneticId == GeneticId || gr.RegistrationNumber == RegistrationNumber);
            if (geneticRegistration == null)
            {
                return null;
            }
            return new GeneticRegistrationResponseVM
            {
                RegistrationNumber = geneticRegistration.RegistrationNumber,
                StudentId = geneticRegistration.StudentId,
                StudentName = geneticRegistration.StudentName,
                Country = geneticRegistration.Country,
                State = geneticRegistration.State,
                City = geneticRegistration.City,
                GeneticId = geneticRegistration.GeneticId,
                ClassName = geneticRegistration.ClassName,
                Branch = geneticRegistration.Branch,
                FatherName = geneticRegistration.FatherName,
                FatherOccupation = geneticRegistration.FatherOccupation,
                MotherName = geneticRegistration.MotherName,
                MotherOccupation = geneticRegistration.MotherOccupation,
                CountryCode = geneticRegistration.CountryCode,
                ContactNumber = geneticRegistration.ContactNumber,
                Age = geneticRegistration.Age,
                Gender = geneticRegistration.Gender,
                Height = geneticRegistration.Height,
                Weight = geneticRegistration.Weight,
                FoodFrequency = geneticRegistration.FoodFrequency,
                Consanguinity = geneticRegistration.Consanguinity,
                ParentsOccupation = geneticRegistration.ParentsOccupation,
                DietType = geneticRegistration.DietType,
                Activity = geneticRegistration.Activity,
                SleepDuration = geneticRegistration.SleepDuration,
                SleepQuality = geneticRegistration.SleepQuality,
                ScreenTime = geneticRegistration.ScreenTime,
                FoodTiming = geneticRegistration.FoodTiming,
                Fruits = geneticRegistration.Fruits,
                Vegetables = geneticRegistration.Vegetables,
                FamilyType = geneticRegistration.FamilyType,
                Siblings = geneticRegistration.Siblings,
                Vaccination = geneticRegistration.Vaccination,
                NatureAccess = geneticRegistration.NatureAccess,
                PollutionAir = geneticRegistration.PollutionAir,
                PollutionNoise = geneticRegistration.PollutionNoise,
                PollutionWater = geneticRegistration.PollutionWater,
                TravelTime = geneticRegistration.TravelTime,

            };
        }
    }
}