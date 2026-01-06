using SchoolManagement.Model;
using System;

namespace SchoolManagement.ViewModel.GeneticRegistration
{
    public class GeneticRegistrationRequestVM
    {
        // -------------------- Primary Keys --------------------
        public string? RegistrationNumber { get; set; } // reg_number (PK)
        public string? GeneticId { get; set; }          // unique genetic id

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
        public DateTime? FatherDateOfBirth { get; set; }
        public DateTime? MotherDateOfBirth { get; set; }

        // -------------------- Contact Info --------------------
        public string CountryCode { get; set; } = "+91";
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }

        // -------------------- Location --------------------
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }

        // -------------------- Biological Address --------------------
        public string? BiologicalCountry { get; set; }
        public string? BiologicalState { get; set; }
        public string? BiologicalCity { get; set; }
        public bool IsBiologicalSame { get; set; } = false;

        // -------------------- Guardian Info --------------------
        public bool HasGuardian { get; set; } = false;
        public string? GuardianFirstName { get; set; }
        public string? GuardianMiddleName { get; set; }
        public string? GuardianLastName { get; set; }
        public string? GuardianOccupation { get; set; }
        public string? GuardianRelationship { get; set; }
        public string? GuardianContactNumber { get; set; }
        public string? GuardianEmail { get; set; }

        // -------------------- Date of Birth --------------------
        public DateTime? DateOfBirth { get; set; }

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
        public bool IsDraft { get; set; } = false;

        // ------------------------------------------------------------
        // Mapper: Converts ViewModel → Database Entity
        // ------------------------------------------------------------
        public static MGeneticRegistration? ToModel(GeneticRegistrationRequestVM request)
        {
            if (request == null) return null;

            return new MGeneticRegistration
            {
                // Core Identifiers
                RegistrationNumber = string.IsNullOrWhiteSpace(request.RegistrationNumber)
                    ? Guid.NewGuid().ToString()
                    : request.RegistrationNumber,
                GeneticId = request.GeneticId,
                UserId = request.UserId,
                UserName = request.UserName,
                ClassName = request.ClassName,
                Branch = request.Branch,

                // Parent Info
                FatherName = request.FatherName,
                FatherOccupation = request.FatherOccupation,
                MotherName = request.MotherName,
                MotherOccupation = request.MotherOccupation,
                FatherDateOfBirth = request.FatherDateOfBirth,
                MotherDateOfBirth = request.MotherDateOfBirth,

                // Contact & Address
                CountryCode = request.CountryCode,
                ContactNumber = request.ContactNumber,
                Email = request.Email,
                Country = request.Country,
                State = request.State,
                City = request.City,

                // Biological Address
                BiologicalCountry = request.BiologicalCountry,
                BiologicalState = request.BiologicalState,
                BiologicalCity = request.BiologicalCity,
                IsBiologicalSame = request.IsBiologicalSame,

                // Guardian Info
                HasGuardian = request.HasGuardian,
                GuardianFirstName = request.GuardianFirstName,
                GuardianMiddleName = request.GuardianMiddleName,
                GuardianLastName = request.GuardianLastName,
                GuardianOccupation = request.GuardianOccupation,
                GuardianRelationship = request.GuardianRelationship,
                GuardianContactNumber = request.GuardianContactNumber,
                GuardianEmail = request.GuardianEmail,

                // Health & Lifestyle
                DateOfBirth = request.DateOfBirth,
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

                // Nutrition
                Fruits = request.Fruits,
                Vegetables = request.Vegetables,
                PlantBasedProtein = request.PlantBasedProtein,
                AnimalBasedProtein = request.AnimalBasedProtein,
                FoodFrequency = request.FoodFrequency,

                // Family & Medical
                FamilyType = request.FamilyType,
                Siblings = request.Siblings,
                Vaccination = request.Vaccination,

                // Environment
                NatureAccess = request.NatureAccess,
                PollutionAir = request.PollutionAir,
                PollutionNoise = request.PollutionNoise,
                PollutionWater = request.PollutionWater,
                PassiveSmoking = request.PassiveSmoking,
                TravelTime = request.TravelTime,

                // Audit / System Fields
                TenantId = request.TenantId,
                CreatedBy = request.CreatedBy,
                UpdatedBy = request.UpdatedBy,
                IsDeleted = request.IsDeleted,
                IsDraft = request.IsDraft,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
        }
    }
}
