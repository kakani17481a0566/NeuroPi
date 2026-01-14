using Microsoft.EntityFrameworkCore;
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

            var geneticRegistration = GeneticRegistrationRequestVM.ToModel(request)
                ?? throw new InvalidOperationException("Mapping to model failed.");

            // 🔹 Generate Registration Number if not provided
            if (string.IsNullOrWhiteSpace(geneticRegistration.RegistrationNumber))
                geneticRegistration.RegistrationNumber = Guid.NewGuid().ToString();

            // 🔹 Defensive handling for null / short country / state
            string countryCode = SafeSubstring(request.Country?.ToUpper().Trim(), 3);
            string stateCode = GenerateStateCode(request.State);
            string timeStampDigits = ExtractNumbers(DateTime.UtcNow.ToString("yyyyMMddHHmmss"));

            // 🔹 Construct unique Genetic ID (e.g., IND/TN/20251031104703)
            string geneticId = $"{countryCode}/{stateCode}/{timeStampDigits}";
            geneticRegistration.GeneticId = geneticId;

            // 🔹 System fields
            geneticRegistration.CreatedOn = DateTime.UtcNow;
            geneticRegistration.UpdatedOn = DateTime.UtcNow;
            geneticRegistration.IsDeleted = false;
            geneticRegistration.TenantId ??= request.TenantId;
            geneticRegistration.CreatedBy ??= request.CreatedBy;
            geneticRegistration.UpdatedBy ??= request.UpdatedBy;

            // 🔹 Save to DB
            _context.GeneticRegistrations.Add(geneticRegistration);
            int result = _context.SaveChanges();

            return result > 0 ? geneticId : "not inserted";
        }

        // ------------------------------------------------------------
        // Get Genetic Registration by GeneticId or RegistrationNumber
        // ------------------------------------------------------------
        public GeneticRegistrationResponseVM? GetGeneticRegistrationByGeneticIdOrRegistrationNumber(
            string geneticId, string registrationNumber)
        {
            if (string.IsNullOrWhiteSpace(geneticId) && string.IsNullOrWhiteSpace(registrationNumber))
                throw new ArgumentException("Either GeneticId or RegistrationNumber must be provided.");

            var record = _context.GeneticRegistrations
                .AsNoTracking()
                .FirstOrDefault(gr =>
                    !gr.IsDeleted &&
                    (gr.GeneticId == geneticId || gr.RegistrationNumber == registrationNumber));

            if (record == null)
                return null;

            // 🔹 Map entity → Response VM
            return new GeneticRegistrationResponseVM
            {
                RegistrationNumber = record.RegistrationNumber,
                UserId = record.UserId,
                UserName = record.UserName,
                GeneticId = record.GeneticId,

                ClassName = record.ClassName,
                Branch = record.Branch,

                FatherName = record.FatherName,
                FatherOccupation = record.FatherOccupation,
                MotherName = record.MotherName,
                MotherOccupation = record.MotherOccupation,
                FatherDateOfBirth = record.FatherDateOfBirth,
                MotherDateOfBirth = record.MotherDateOfBirth,

                CountryCode = record.CountryCode,
                ContactNumber = record.ContactNumber,
                Email = record.Email,

                Country = record.Country,
                State = record.State,
                City = record.City,

                // 🔹 Biological Address
                BiologicalCountry = record.BiologicalCountry,
                BiologicalState = record.BiologicalState,
                BiologicalCity = record.BiologicalCity,
                IsBiologicalSame = record.IsBiologicalSame,

                // 🔹 Guardian Info
                HasGuardian = record.HasGuardian,
                GuardianFirstName = record.GuardianFirstName,
                GuardianMiddleName = record.GuardianMiddleName,
                GuardianLastName = record.GuardianLastName,
                GuardianOccupation = record.GuardianOccupation,
                GuardianRelationship = record.GuardianRelationship,
                GuardianContactNumber = record.GuardianContactNumber,
                GuardianEmail = record.GuardianEmail,

                // 🔹 Health & Lifestyle
                DateOfBirth = record.DateOfBirth,
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

                // 🔹 Nutrition
                Fruits = record.Fruits,
                Vegetables = record.Vegetables,
                PlantBasedProtein = record.PlantBasedProtein,
                AnimalBasedProtein = record.AnimalBasedProtein,
                FoodFrequency = record.FoodFrequency,

                // 🔹 Family & Medical
                FamilyType = record.FamilyType,
                Siblings = record.Siblings,
                Vaccination = record.Vaccination,

                // 🔹 Environment
                NatureAccess = record.NatureAccess,
                PollutionAir = record.PollutionAir,
                PollutionNoise = record.PollutionNoise,
                PollutionWater = record.PollutionWater,
                PassiveSmoking = record.PassiveSmoking,
                TravelTime = record.TravelTime,

                // 🔹 Audit & Tenant Info
                TenantId = record.TenantId,
                CreatedBy = record.CreatedBy,
                UpdatedBy = record.UpdatedBy,
                IsDeleted = record.IsDeleted,
                CreatedOn = record.CreatedOn,
                UpdatedOn = record.UpdatedOn
            };

        }

        // ------------------------------------------------------------
        // Get Latest Genetic Registration by UserId
        // ------------------------------------------------------------
        public GeneticRegistrationResponseVM? GetGeneticRegistrationByUserId(int userId)
        {
            var record = _context.GeneticRegistrations
                .AsNoTracking()
                .Where(gr => gr.UserId == userId && !gr.IsDeleted)
                .OrderByDescending(gr => gr.CreatedOn)
                .FirstOrDefault();

            if (record == null)
                return null;

            return new GeneticRegistrationResponseVM
            {
                RegistrationNumber = record.RegistrationNumber,
                UserId = record.UserId,
                UserName = record.UserName,
                GeneticId = record.GeneticId,

                ClassName = record.ClassName,
                Branch = record.Branch,

                FatherName = record.FatherName,
                FatherOccupation = record.FatherOccupation,
                MotherName = record.MotherName,
                MotherOccupation = record.MotherOccupation,
                FatherDateOfBirth = record.FatherDateOfBirth,
                MotherDateOfBirth = record.MotherDateOfBirth,

                CountryCode = record.CountryCode,
                ContactNumber = record.ContactNumber,
                Email = record.Email,

                Country = record.Country,
                State = record.State,
                City = record.City,

                // 🔹 Biological Address
                BiologicalCountry = record.BiologicalCountry,
                BiologicalState = record.BiologicalState,
                BiologicalCity = record.BiologicalCity,
                IsBiologicalSame = record.IsBiologicalSame,

                // 🔹 Guardian Info
                HasGuardian = record.HasGuardian,
                GuardianFirstName = record.GuardianFirstName,
                GuardianMiddleName = record.GuardianMiddleName,
                GuardianLastName = record.GuardianLastName,
                GuardianOccupation = record.GuardianOccupation,
                GuardianRelationship = record.GuardianRelationship,
                GuardianContactNumber = record.GuardianContactNumber,
                GuardianEmail = record.GuardianEmail,

                // 🔹 Health & Lifestyle
                DateOfBirth = record.DateOfBirth,
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

                // 🔹 Nutrition
                Fruits = record.Fruits,
                Vegetables = record.Vegetables,
                PlantBasedProtein = record.PlantBasedProtein,
                AnimalBasedProtein = record.AnimalBasedProtein,
                FoodFrequency = record.FoodFrequency,

                // 🔹 Family & Medical
                FamilyType = record.FamilyType,
                Siblings = record.Siblings,
                Vaccination = record.Vaccination,

                // 🔹 Environment
                NatureAccess = record.NatureAccess,
                PollutionAir = record.PollutionAir,
                PollutionNoise = record.PollutionNoise,
                PollutionWater = record.PollutionWater,
                PassiveSmoking = record.PassiveSmoking,
                TravelTime = record.TravelTime,

                // 🔹 Audit & Tenant Info
                TenantId = record.TenantId,
                CreatedBy = record.CreatedBy,
                UpdatedBy = record.UpdatedBy,
                IsDeleted = record.IsDeleted,
                CreatedOn = record.CreatedOn,
                UpdatedOn = record.UpdatedOn,
                IsDraft = record.IsDraft // Ensure VM has this property too if updated previously
            };
        }

        // ------------------------------------------------------------
        // Get All Genetic Registrations (for admin/master view)
        // ------------------------------------------------------------
        public List<GeneticRegistrationResponseVM> GetAllSubmissions(bool includeDeleted = true)
        {
            var query = _context.GeneticRegistrations
                .AsNoTracking();

            // Filter by deleted status if not including deleted
            if (!includeDeleted)
            {
                query = query.Where(gr => !gr.IsDeleted);
            }

            var records = query.OrderByDescending(gr => gr.CreatedOn).ToList();

            if (records == null || !records.Any())
                return new List<GeneticRegistrationResponseVM>();

            return records.Select(gr => new GeneticRegistrationResponseVM
            {
                RegistrationNumber = gr.RegistrationNumber,
                GeneticId = gr.GeneticId,
                UserId = gr.UserId,
                UserName = gr.UserName,
                ClassName = gr.ClassName,
                Branch = gr.Branch,
                FatherName = gr.FatherName,
                FatherOccupation = gr.FatherOccupation,
                MotherName = gr.MotherName,
                MotherOccupation = gr.MotherOccupation,
                FatherDateOfBirth = gr.FatherDateOfBirth,
                MotherDateOfBirth = gr.MotherDateOfBirth,
                CountryCode = gr.CountryCode,
                ContactNumber = gr.ContactNumber,
                Email = gr.Email,
                Country = gr.Country,
                State = gr.State,
                City = gr.City,
                BiologicalCountry = gr.BiologicalCountry,
                BiologicalState = gr.BiologicalState,
                BiologicalCity = gr.BiologicalCity,
                IsBiologicalSame = gr.IsBiologicalSame,
                HasGuardian = gr.HasGuardian,
                GuardianFirstName = gr.GuardianFirstName,
                GuardianMiddleName = gr.GuardianMiddleName,
                GuardianLastName = gr.GuardianLastName,
                GuardianOccupation = gr.GuardianOccupation,
                GuardianRelationship = gr.GuardianRelationship,
                GuardianContactNumber = gr.GuardianContactNumber,
                GuardianEmail = gr.GuardianEmail,
                DateOfBirth = gr.DateOfBirth,
                Age = gr.Age,
                Gender = gr.Gender,
                Height = gr.Height,
                Weight = gr.Weight,
                FoodFrequency = gr.FoodFrequency,
                Consanguinity = gr.Consanguinity,
                ParentsOccupation = gr.ParentsOccupation,
                DietType = gr.DietType,
                PlantBasedProtein = gr.PlantBasedProtein,
                AnimalBasedProtein = gr.AnimalBasedProtein,
                Activity = gr.Activity,
                SleepDuration = gr.SleepDuration,
                SleepQuality = gr.SleepQuality,
                ScreenTime = gr.ScreenTime,
                FoodTiming = gr.FoodTiming,
                Fruits = gr.Fruits,
                Vegetables = gr.Vegetables,
                FamilyType = gr.FamilyType,
                Siblings = gr.Siblings,
                Vaccination = gr.Vaccination,
                NatureAccess = gr.NatureAccess,
                PollutionAir = gr.PollutionAir,
                PollutionNoise = gr.PollutionNoise,
                PollutionWater = gr.PollutionWater,
                PassiveSmoking = gr.PassiveSmoking,
                TravelTime = gr.TravelTime,
                CreatedBy = gr.CreatedBy,
                UpdatedBy = gr.UpdatedBy,
                TenantId = gr.TenantId,
                IsDeleted = gr.IsDeleted,
                IsDraft = gr.IsDraft,
                CreatedOn = gr.CreatedOn,
                UpdatedOn = gr.UpdatedOn
            }).ToList();
        }

        // ------------------------------------------------------------
        // Get All Genetic Registrations by Created By (logged-in user)
        // ------------------------------------------------------------
        public List<GeneticRegistrationResponseVM> GetAllUserSubmissions(int userId, bool includeDeleted = false)
        {
            var query = _context.GeneticRegistrations
                .AsNoTracking()
                .Where(gr => gr.CreatedBy.HasValue && gr.CreatedBy.Value == userId);

            // Filter by deleted status if not including deleted
            if (!includeDeleted)
            {
                query = query.Where(gr => !gr.IsDeleted);
            }

            var records = query.OrderByDescending(gr => gr.CreatedOn).ToList();


            if (records == null || !records.Any())
                return new List<GeneticRegistrationResponseVM>();

            return records.Select(record => new GeneticRegistrationResponseVM
            {
                RegistrationNumber = record.RegistrationNumber,
                UserId = record.UserId,
                UserName = record.UserName,
                GeneticId = record.GeneticId,

                ClassName = record.ClassName,
                Branch = record.Branch,

                FatherName = record.FatherName,
                FatherOccupation = record.FatherOccupation,
                MotherName = record.MotherName,
                MotherOccupation = record.MotherOccupation,
                FatherDateOfBirth = record.FatherDateOfBirth,
                MotherDateOfBirth = record.MotherDateOfBirth,

                CountryCode = record.CountryCode,
                ContactNumber = record.ContactNumber,
                Email = record.Email,

                Country = record.Country,
                State = record.State,
                City = record.City,

                // 🔹 Biological Address
                BiologicalCountry = record.BiologicalCountry,
                BiologicalState = record.BiologicalState,
                BiologicalCity = record.BiologicalCity,
                IsBiologicalSame = record.IsBiologicalSame,

                // 🔹 Guardian Info
                HasGuardian = record.HasGuardian,
                GuardianFirstName = record.GuardianFirstName,
                GuardianMiddleName = record.GuardianMiddleName,
                GuardianLastName = record.GuardianLastName,
                GuardianOccupation = record.GuardianOccupation,
                GuardianRelationship = record.GuardianRelationship,
                GuardianContactNumber = record.GuardianContactNumber,
                GuardianEmail = record.GuardianEmail,

                // 🔹 Health & Lifestyle
                DateOfBirth = record.DateOfBirth,
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

                // 🔹 Nutrition
                Fruits = record.Fruits,
                Vegetables = record.Vegetables,
                PlantBasedProtein = record.PlantBasedProtein,
                AnimalBasedProtein = record.AnimalBasedProtein,
                FoodFrequency = record.FoodFrequency,

                // 🔹 Family & Medical
                FamilyType = record.FamilyType,
                Siblings = record.Siblings,
                Vaccination = record.Vaccination,

                // 🔹 Environment
                NatureAccess = record.NatureAccess,
                PollutionAir = record.PollutionAir,
                PollutionNoise = record.PollutionNoise,
                PollutionWater = record.PollutionWater,
                PassiveSmoking = record.PassiveSmoking,
                TravelTime = record.TravelTime,

                // 🔹 Audit & Tenant Info
                TenantId = record.TenantId,
                CreatedBy = record.CreatedBy,
                UpdatedBy = record.UpdatedBy,
                IsDeleted = record.IsDeleted,
                CreatedOn = record.CreatedOn,
                UpdatedOn = record.UpdatedOn,
                IsDraft = record.IsDraft
            }).ToList();
        }

        // ------------------------------------------------------------
        // Delete Submission (Soft Delete)
        // ------------------------------------------------------------
        public bool DeleteSubmission(string registrationNumber, int userId)
        {
            var record = _context.GeneticRegistrations
                .FirstOrDefault(gr => gr.RegistrationNumber == registrationNumber && !gr.IsDeleted);

            if (record == null)
                return false;

            // Verify user owns this submission
            if (record.CreatedBy != userId)
                return false;

            // Soft delete
            record.IsDeleted = true;
            record.UpdatedOn = DateTime.UtcNow;
            record.UpdatedBy = userId;

            _context.SaveChanges();
            return true;
        }


        // ------------------------------------------------------------
        // Helper: Extract numbers from a string
        // ------------------------------------------------------------
        public static string ExtractNumbers(string input) =>
            Regex.Replace(input ?? string.Empty, @"\D", "");

        // ------------------------------------------------------------
        // Helper: Generate state code (handles multi-word)
        // ------------------------------------------------------------
        private static string GenerateStateCode(string stateName)
        {
            if (string.IsNullOrWhiteSpace(stateName))
                return "XXX"; // fallback

            var parts = stateName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return parts.Length > 1
                ? string.Join("", parts.Select(s => s[0].ToString().ToUpper()))
                : SafeSubstring(stateName.ToUpper().Trim(), 3);
        }

        // ------------------------------------------------------------
        // Get Dashboard Stats 
        // ------------------------------------------------------------
        public GeneticDashboardStatsVM GetDashboardStats()
        {
            var query = _context.GeneticRegistrations.AsNoTracking();

            // Aggregates
            var total = query.Count();
            var deleted = query.Count(r => r.IsDeleted);
            var drafts = query.Count(r => r.IsDraft && !r.IsDeleted);
            var completed = query.Count(r => !r.IsDraft && !r.IsDeleted);

            // Monthly Trends (Last 12 months for example, or all time)
            // Grouping by Month/Year.
            // Note: EF Core GroupBy might be tricky depending on provider, switching to client eval for small datasets is okay
            // but for scalability, better to select needed fields first.
            
            var monthlyData = query
                .Select(r => new { r.CreatedOn, r.IsDraft, r.IsDeleted })
                .ToList()
                .GroupBy(r => new { r.CreatedOn.Year, r.CreatedOn.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .Select(g => new MonthlyStatVM
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM yyyy"),
                    Total = g.Count(),
                    Completed = g.Count(x => !x.IsDraft && !x.IsDeleted),
                    Drafts = g.Count(x => x.IsDraft && !x.IsDeleted)
                })
                .ToList();

            // Top Branches
            var topBranches = query
                .Where(r => !r.IsDeleted && !r.IsDraft && !string.IsNullOrEmpty(r.Branch))
                .GroupBy(r => r.Branch)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToList()
                .Select(x => new BranchStatVM { BranchName = x.Name, Count = x.Count })
                .ToList();

            // Top Locations (State)
            var topLocations = query
                .Where(r => !r.IsDeleted && !r.IsDraft && !string.IsNullOrEmpty(r.State))
                .GroupBy(r => r.State)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToList()
                .Select(x => new LocationStatVM { Location = x.Name, Count = x.Count })
                .ToList();

            // Gender Stats
            var genderStats = query
                .Where(r => !r.IsDeleted && !r.IsDraft && !string.IsNullOrEmpty(r.Gender))
                .GroupBy(r => r.Gender)
                .Select(g => new { Gender = g.Key, Count = g.Count() })
                .ToList()
                .Select(x => new GenderStatVM { Gender = x.Gender, Count = x.Count })
                .ToList();

            // Time-based Stats
            var today = DateTime.UtcNow.Date;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            var startOfMonth = new DateTime(today.Year, today.Month, 1);

            var countToday = query.Count(r => r.CreatedOn >= today);
            var countWeek = query.Count(r => r.CreatedOn >= startOfWeek);
            var countMonth = query.Count(r => r.CreatedOn >= startOfMonth);

            return new GeneticDashboardStatsVM
            {
                TotalRegistrations = total,
                DeletedRegistrations = deleted,
                DraftRegistrations = drafts,
                CompletedRegistrations = completed,
                MonthlyStats = monthlyData,
                TopBranches = topBranches,
                TopLocations = topLocations,
                GenderStats = genderStats,
                RegistrationsToday = countToday,
                RegistrationsThisWeek = countWeek,
                RegistrationsThisMonth = countMonth
            };
        }

        // ------------------------------------------------------------
        // Helper: Safe substring to avoid exceptions
        // ------------------------------------------------------------
        private static string SafeSubstring(string input, int length)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "UNK"; // unknown
            return input.Length >= length
                ? input.Substring(0, length)
                : input.PadRight(length, 'X');
        }
    }
}
