using System;

namespace SchoolManagement.ViewModel.GeneticRegistration
{
    public class GeneticRegistrationResponseVM
    {
        // -------------------- Basic Info --------------------
        public string RegistrationNumber { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string GeneticId { get; set; }

        // -------------------- Class / Branch --------------------
        public string? ClassName { get; set; }
        public string? Branch { get; set; }

        // -------------------- Parent Info --------------------
        public string FatherName { get; set; }
        public string FatherOccupation { get; set; }
        public string MotherName { get; set; }
        public string MotherOccupation { get; set; }

        // -------------------- Contact --------------------
        public string CountryCode { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        // -------------------- Location --------------------
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        // -------------------- Dates --------------------
        public DateTime? DateOfBirth { get; set; }
        public DateTime? FatherDateOfBirth { get; set; }
        public DateTime? MotherDateOfBirth { get; set; }

        // -------------------- Health --------------------
        public int? Age { get; set; }
        public string Gender { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }

        // -------------------- Lifestyle --------------------
        public string Consanguinity { get; set; }
        public string ParentsOccupation { get; set; }
        public string DietType { get; set; }
        public string Activity { get; set; }
        public string SleepDuration { get; set; }
        public string SleepQuality { get; set; }
        public string ScreenTime { get; set; }
        public string FoodTiming { get; set; }

        // -------------------- Nutrition --------------------
        public string Fruits { get; set; }
        public string Vegetables { get; set; }
        public string PlantBasedProtein { get; set; }
        public string AnimalBasedProtein { get; set; }
        public string FoodFrequency { get; set; }

        // -------------------- Family --------------------
        public string FamilyType { get; set; }
        public int? Siblings { get; set; }

        // -------------------- Medical --------------------
        public string Vaccination { get; set; }

        // -------------------- Environment --------------------
        public string NatureAccess { get; set; }
        public string PollutionAir { get; set; }
        public string PollutionNoise { get; set; }
        public string PollutionWater { get; set; }
        public string PassiveSmoking { get; set; }
        public string TravelTime { get; set; }

        // -------------------- Audit Fields --------------------
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
