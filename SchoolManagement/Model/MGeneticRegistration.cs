using NeuroPi.UserManagment.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("genetic_reg_table")]
    public class MGeneticRegistration
    {
        // 🔹 Primary Key
        [Key]
        [Column("reg_number")]
        [Required]
        public string RegistrationNumber { get; set; } = Guid.NewGuid().ToString();

        // 🔹 Core Identifiers
        [Column("genetic_id")]
        public string? GeneticId { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [Column("user_name")]
        public string? UserName { get; set; }

        [Column("class_name")]
        public string? ClassName { get; set; }

        [Column("branch")]
        public string? Branch { get; set; }

        // 🔹 Parent Details
        [Column("father_name")]
        public string? FatherName { get; set; }

        [Column("father_occupation")]
        public string? FatherOccupation { get; set; }

        [Column("mother_name")]
        public string? MotherName { get; set; }

        [Column("mother_occupation")]
        public string? MotherOccupation { get; set; }

        [Column("father_date_of_birth")]
        public DateTime? FatherDateOfBirth { get; set; }

        [Column("mother_date_of_birth")]
        public DateTime? MotherDateOfBirth { get; set; }

        // 🔹 Contact & Location
        [Column("country_code")]
        public string CountryCode { get; set; } = "+91";

        [Column("contact_number")]
        public string? ContactNumber { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("country")]
        public string? Country { get; set; }

        [Column("state")]
        public string? State { get; set; }

        [Column("city")]
        public string? City { get; set; }

        // 🔹 Biological Address
        [Column("biological_country")]
        public string? BiologicalCountry { get; set; }

        [Column("biological_state")]
        public string? BiologicalState { get; set; }

        [Column("biological_city")]
        public string? BiologicalCity { get; set; }

        [Column("is_biological_same")]
        public bool IsBiologicalSame { get; set; } = false;

        // 🔹 Guardian Details
        [Column("has_guardian")]
        public bool HasGuardian { get; set; } = false;

        [Column("guardian_first_name")]
        public string? GuardianFirstName { get; set; }

        [Column("guardian_middle_name")]
        public string? GuardianMiddleName { get; set; }

        [Column("guardian_last_name")]
        public string? GuardianLastName { get; set; }

        [Column("guardian_occupation")]
        public string? GuardianOccupation { get; set; }

        [Column("guardian_relationship")]
        public string? GuardianRelationship { get; set; }

        [Column("guardian_contact_number")]
        public string? GuardianContactNumber { get; set; }

        [Column("guardian_email")]
        public string? GuardianEmail { get; set; }

        // 🔹 Personal Info
        [Column("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }

        [Column("age")]
        public int? Age { get; set; }

        [Column("gender")]
        public string? Gender { get; set; }

        [Column("height")]
        public decimal? Height { get; set; }

        [Column("weight")]
        public decimal? Weight { get; set; }

        // 🔹 Health & Lifestyle
        [Column("food_frequency")]
        public string? FoodFrequency { get; set; }

        [Column("consanguinity")]
        public string? Consanguinity { get; set; }

        [Column("parents_occupation")]
        public string? ParentsOccupation { get; set; }

        [Column("diet_type")]
        public string? DietType { get; set; }

        [Column("plant_based_protein")]
        public string? PlantBasedProtein { get; set; }

        [Column("animal_based_protein")]
        public string? AnimalBasedProtein { get; set; }

        [Column("activity")]
        public string? Activity { get; set; }

        [Column("sleep_duration")]
        public string? SleepDuration { get; set; }

        [Column("sleep_quality")]
        public string? SleepQuality { get; set; }

        [Column("screen_time")]
        public string? ScreenTime { get; set; }

        [Column("food_timing")]
        public string? FoodTiming { get; set; }

        [Column("fruits")]
        public string? Fruits { get; set; }

        [Column("vegetables")]
        public string? Vegetables { get; set; }

        [Column("family_type")]
        public string? FamilyType { get; set; }

        [Column("siblings")]
        public int? Siblings { get; set; }

        [Column("vaccination")]
        public string? Vaccination { get; set; }

        [Column("nature_access")]
        public string? NatureAccess { get; set; }

        [Column("pollution_air")]
        public string? PollutionAir { get; set; }

        [Column("pollution_noise")]
        public string? PollutionNoise { get; set; }

        [Column("pollution_water")]
        public string? PollutionWater { get; set; }

        [Column("passive_smoking")]
        public string? PassiveSmoking { get; set; }

        [Column("travel_time")]
        public string? TravelTime { get; set; }

        // 🔹 Audit & Tenant Fields
        [Column("created_by")]
        public int? CreatedBy { get; set; }

        [Column("updated_by")]
        public int? UpdatedBy { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [Column("created_on")]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Column("updated_on")]
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;

        // 🔹 Navigation Properties
        [ForeignKey(nameof(CreatedBy))]
        public virtual MUser? CreatedByUser { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public virtual MUser? UpdatedByUser { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant? Tenant { get; set; }
    }
}
