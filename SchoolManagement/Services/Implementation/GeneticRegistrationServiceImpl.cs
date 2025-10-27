using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.GeneticRegistration;
using System;
using System.Linq;
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

        // ------------------------------------------------------------
        // Add new Genetic Registration
        // ------------------------------------------------------------
        public string AddGeneticRegistration(GeneticRegistrationRequestVM request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var geneticRegistration = GeneticRegistrationRequestVM.ToModel(request);

            // Defensive handling for null/short country/state
            string countryCode = SafeSubstring(request.Country?.ToUpper().Trim(), 3);
            string stateCode = GenerateStateCode(request.State);
            string timeStampDigits = ExtractNumbers(DateTime.UtcNow.ToString("yyyyMMddHHmmss"));

            // Construct Genetic ID
            string geneticId = $"{countryCode}/{stateCode}/{timeStampDigits}";
            geneticRegistration.GeneticId = geneticId;

            // Set timestamps
            geneticRegistration.CreatedOn = DateTime.UtcNow;
            geneticRegistration.UpdatedOn = DateTime.UtcNow;

            // Save to DB
            _context.GeneticRegistrations.Add(geneticRegistration);
            int result = _context.SaveChanges();

            return result > 0 ? geneticId : "not inserted";
        }

        // ------------------------------------------------------------
        // Helper: Extract numbers from a string
        // ------------------------------------------------------------
        public static string ExtractNumbers(string input)
        {
            return Regex.Replace(input ?? string.Empty, @"\D", "");
        }

        // ------------------------------------------------------------
        // Helper: Generate state code (handles multi-word)
        // ------------------------------------------------------------
        private static string GenerateStateCode(string stateName)
        {
            if (string.IsNullOrWhiteSpace(stateName))
                return "XXX"; // fallback code

            var parts = stateName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 1)
                return string.Join("", parts.Select(s => s[0].ToString().ToUpper()));

            return SafeSubstring(stateName.ToUpper().Trim(), 3);
        }

        // ------------------------------------------------------------
        // Helper: Safe substring to avoid exceptions
        // ------------------------------------------------------------
        private static string SafeSubstring(string input, int length)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "UNK"; // unknown
            return input.Length >= length ? input.Substring(0, length) : input.PadRight(length, 'X');
        }

        // ------------------------------------------------------------
        // Get Genetic Registration by GeneticId or RegistrationNumber
        // ------------------------------------------------------------
        public GeneticRegistrationResponseVM GetGeneticRegistrationByGeneticIdOrRegistrationNumber(
            string geneticId, string registrationNumber)
        {
            var record = _context.GeneticRegistrations
                .FirstOrDefault(gr => gr.GeneticId == geneticId || gr.RegistrationNumber == registrationNumber);

            if (record == null)
                return null;

            return new GeneticRegistrationResponseVM
            {
                RegistrationNumber = record.RegistrationNumber,
                UserId = record.UserId,
                UserName = record.UserName,
                GeneticId = record.GeneticId,
                Country = record.Country,
                State = record.State,
                City = record.City,
                ClassName = record.ClassName,
                Branch = record.Branch,
                FatherName = record.FatherName,
                FatherOccupation = record.FatherOccupation,
                MotherName = record.MotherName,
                MotherOccupation = record.MotherOccupation,
                CountryCode = record.CountryCode,
                ContactNumber = record.ContactNumber,
                Email = record.Email,
                Age = record.Age,
                Gender = record.Gender,
                Height = record.Height,
                Weight = record.Weight,
                Consanguinity = record.Consanguinity,
                ParentsOccupation = record.ParentsOccupation,
                DietType = record.DietType,
                Activity = record.Activity,
                SleepDuration = record.SleepDuration,
                SleepQuality = record.SleepQuality,
                ScreenTime = record.ScreenTime,
                FoodTiming = record.FoodTiming,
                Fruits = record.Fruits,
                Vegetables = record.Vegetables,
                PlantBasedProtein = record.PlantBasedProtein,
                AnimalBasedProtein = record.AnimalBasedProtein,
                FoodFrequency = record.FoodFrequency,
                FamilyType = record.FamilyType,
                Siblings = record.Siblings,
                Vaccination = record.Vaccination,
                NatureAccess = record.NatureAccess,
                PollutionAir = record.PollutionAir,
                PollutionNoise = record.PollutionNoise,
                PollutionWater = record.PollutionWater,
                PassiveSmoking = record.PassiveSmoking,
                TravelTime = record.TravelTime,
                DateOfBirth = record.DateOfBirth,
                FatherDateOfBirth = record.FatherDateOfBirth,
                MotherDateOfBirth = record.MotherDateOfBirth,
                CreatedOn = record.CreatedOn,
                UpdatedOn = record.UpdatedOn
            };
        }
    }
}
