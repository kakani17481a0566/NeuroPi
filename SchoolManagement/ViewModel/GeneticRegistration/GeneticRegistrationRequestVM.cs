using SchoolManagement.Model;
using System;

namespace SchoolManagement.ViewModel.GeneticRegistration
{
    public class GeneticRegistrationRequestVM
    {
        // -------------------- Primary Key --------------------
        public string? RegistrationNumber { get; set; }   // maps to reg_number (PK)

        // -------------------- Personal Info --------------------
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? ClassName { get; set; }
        public string? Branch { get; set; }

        // -------------------- Parent Info --------------------
        public string? FatherName { get; set; }
        public string? FatherOccupation { get; set; }
        public string? MotherName { get; set; }
        public string? MotherOccupation { get; set; }

        // -------------------- Contact Info --------------------
        public string CountryCode { get; set; } = "+91";
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }

        // -------------------- Location --------------------
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }

        // -------------------- Date of Births --------------------
        public DateTime? DateOfBirth { get; set; }
        public DateTime? FatherDateOfBirth { get; set; }
        public DateTime? MotherDateOfBirth { get; set; }

        // -------------------- Health & Lifestyle --------------------
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }

        public string? Consanguinity { get; set; }
        public string? ParentsOccupation { get; set; }
        public string? DietType { get; set; }
        public string? Activity { get; set; }
        public string? SleepDuration { get; set; }
        public string? SleepQuality { get; set; }
        public string? ScreenTime { get; set; }
        public string? FoodTiming { get; set; }

        // -------------------- Food & Nutrition --------------------
        public string? Fruits { get; set; }
        public string? Vegetables { get; set; }
        public string? PlantBasedProtein { get; set; }
        public string? AnimalBasedProtein { get; set; }
        public string? FoodFrequency { get; set; }

        // -------------------- Family Info --------------------
        public string? FamilyType { get; set; }
        public int? Siblings { get; set; }

        // -------------------- Medical --------------------
        public string? Vaccination { get; set; }

        // -------------------- Environment --------------------
        public string? NatureAccess { get; set; }
        public string? PollutionAir { get; set; }
        public string? PollutionNoise { get; set; }
        public string? PollutionWater { get; set; }
        public string? PassiveSmoking { get; set; }
        public string? TravelTime { get; set; }

        // -------------------- System Fields --------------------
        public int? TenantId { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;

        // ------------------------------------------------------------
        // Mapper: Converts ViewModel → Database Entity
        // ------------------------------------------------------------
        public static MGeneticRegistration? ToModel(GeneticRegistrationRequestVM request)
        {
            if (request == null) return null;

            return new MGeneticRegistration
            {
                RegistrationNumber = string.IsNullOrWhiteSpace(request.RegistrationNumber)
                    ? Guid.NewGuid().ToString()   // auto-generate if missing
                    : request.RegistrationNumber,

                UserId = request.UserId,
                UserName = request.UserName,
                ClassName = request.ClassName,
                Branch = request.Branch,

                FatherName = request.FatherName,
                FatherOccupation = request.FatherOccupation,
                MotherName = request.MotherName,
                MotherOccupation = request.MotherOccupation,

                CountryCode = request.CountryCode,
                ContactNumber = request.ContactNumber,
                Email = request.Email,

                Country = request.Country,
                State = request.State,
                City = request.City,

                DateOfBirth = request.DateOfBirth,
                FatherDateOfBirth = request.FatherDateOfBirth,
                MotherDateOfBirth = request.MotherDateOfBirth,

                Age = request.Age,
                Gender = request.Gender,
                Height = request.Height,
                Weight = request.Weight,

                Consanguinity = request.Consanguinity,
                ParentsOccupation = request.ParentsOccupation,
                DietType = request.DietType,
                Activity = request.Activity,
                SleepDuration = request.SleepDuration,
                SleepQuality = request.SleepQuality,
                ScreenTime = request.ScreenTime,
                FoodTiming = request.FoodTiming,

                Fruits = request.Fruits,
                Vegetables = request.Vegetables,
                PlantBasedProtein = request.PlantBasedProtein,
                AnimalBasedProtein = request.AnimalBasedProtein,
                FoodFrequency = request.FoodFrequency,

                FamilyType = request.FamilyType,
                Siblings = request.Siblings,
                Vaccination = request.Vaccination,

                NatureAccess = request.NatureAccess,
                PollutionAir = request.PollutionAir,
                PollutionNoise = request.PollutionNoise,
                PollutionWater = request.PollutionWater,
                PassiveSmoking = request.PassiveSmoking,
                TravelTime = request.TravelTime,

                TenantId = request.TenantId,
                CreatedBy = request.CreatedBy,
                UpdatedBy = request.UpdatedBy,
                IsDeleted = request.IsDeleted,

                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
        }
    }
}
