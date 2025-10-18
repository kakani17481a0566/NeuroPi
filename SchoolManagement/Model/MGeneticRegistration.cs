using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("genetic_reg_table")]
    public class MGeneticRegistration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("reg_number")]
        public string RegistrationNumber { get; set; }

        [Column("country")]
        public string Country {  get; set; }
        [Column("state")]
        public string State { get; set; }
        [Column("city")]
        public string City { get; set; }
        [Column("genetic_id")]
        public string GeneticId { get; set; }
        [Column("student_id")]
        public int StudentId { get; set; }
        [Column("student_name")]
        public string StudentName { get; set; }
        [Column("class_name")]
        public string ClassName { get; set; }
        [Column("branch")]
        public string Branch { get; set; }
        [Column("father_name")]
        public string FatherName { get; set; }
        [Column("father_occupation")]
        public string FatherOccupation { get; set; }
        [Column("mother_name")]
        public string MotherName { get; set; }
        [Column("mother_occupation")]
        public string MotherOccupation { get; set; }
        [Column("country_code")]
        public string CountryCode { get; set; } = "+91";
        [Column("contact_number")]
        public string ContactNumber { get; set; }
        [Column("age")]
        public int Age { get; set; }
        [Column("gender")]
        public string Gender { get; set; }
        [Column("height")]
        public decimal Height { get; set; }
        [Column("weight")]
        public decimal Weight { get; set; }
        [Column("food_frequency")]
        public string FoodFrequency { get; set; }
        [Column("consanguinity")]
        public string Consanguinity { get; set; }
        [Column("parents_occupation")]
        public string ParentsOccupation { get; set; }
        [Column("diet_type")]
        public string DietType { get; set; }
        [Column("activity")]
        public string Activity { get; set; }
        [Column("sleep_duration")]
        public string SleepDuration { get; set; }
        [Column("sleep_quality")]
        public string SleepQuality { get; set; }
        [Column("screen_time")]
        public string ScreenTime { get; set; }
        [Column("food_timing")]
        public string FoodTiming { get; set; }
        [Column("fruits")]
        public string Fruits { get; set; }
        [Column("vegetables")]
        public string Vegetables { get; set; }
        [Column("family_type")]
        public string FamilyType { get; set; }
        [Column("siblings")]
        public int Siblings { get; set; }
        [Column("vaccination")]
        public string Vaccination { get; set; }
        [Column("nature_access")]
        public string NatureAccess { get; set; }
        [Column("pollution_air")]
        public string PollutionAir { get; set; }
        [Column("pollution_noise")]
        public string PollutionNoise { get; set; }
        [Column("pollution_water")]
        public string PollutionWater { get; set; }
        [Column("travel_time")]
        public string TravelTime { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}


