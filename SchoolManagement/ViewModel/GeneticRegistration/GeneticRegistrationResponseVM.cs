using System;

namespace SchoolManagement.ViewModel.GeneticRegistration
{
    public class GeneticRegistrationResponseVM
    {
        // -------------------- Basic Info --------------------
        public string? RegistrationNumber { get; set; }
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? GeneticId { get; set; }

        // -------------------- Class / Branch --------------------
        public string? ClassName { get; set; }
        public string? Branch { get; set; }

        // -------------------- Parent Info --------------------
        public string? FatherName { get; set; }
        public string? FatherOccupation { get; set; }
        public string? MotherName { get; set; }
        public string? MotherOccupation { get; set; }
        public DateTime? FatherDateOfBirth { get; set; }
        public DateTime? MotherDateOfBirth { get; set; }

        // -------------------- Contact --------------------
        public string? CountryCode { get; set; } = "+91";
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }

        // -------------------- Address --------------------
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }

        // -------------------- Biological Address --------------------
        public string? BiologicalCountry { get; set; }
        public string? BiologicalState { get; set; }
        public string? BiologicalCity { get; set; }
        public bool IsBiologicalSame { get; set; }

        // -------------------- Guardian Info --------------------
        public bool HasGuardian { get; set; }
        public string? GuardianFirstName { get; set; }
        public string? GuardianMiddleName { get; set; }
        public string? GuardianLastName { get; set; }
        public string? GuardianOccupation { get; set; }
        public string? GuardianRelationship { get; set; }
        public string? GuardianContactNumber { get; set; }
        public string? GuardianEmail { get; set; }

        // -------------------- Dates --------------------
        public DateTime? DateOfBirth { get; set; }

        // -------------------- Health --------------------
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }

        // -------------------- Lifestyle --------------------
        public string? Consanguinity { get; set; }
        public string? ParentsOccupation { get; set; }
        public string? DietType { get; set; }
        public string? Activity { get; set; }
        public string? SleepDuration { get; set; }
        public string? SleepQuality { get; set; }
        public string? ScreenTime { get; set; }
        public string? FoodTiming { get; set; }

        // -------------------- Nutrition --------------------
        public string? Fruits { get; set; }
        public string? Vegetables { get; set; }
        public string? PlantBasedProtein { get; set; }
        public string? AnimalBasedProtein { get; set; }
        public string? FoodFrequency { get; set; }

        // -------------------- Family --------------------
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

        // -------------------- System Metadata --------------------
        public int? TenantId { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        // -------------------- Audit Fields --------------------
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDraft { get; set; }

        // -------------------- Convenience (optional) --------------------
        public string? CreatedOnFormatted => CreatedOn.ToString("dd-MMM-yyyy HH:mm");
        public string? UpdatedOnFormatted => UpdatedOn.ToString("dd-MMM-yyyy HH:mm");
    }
}
