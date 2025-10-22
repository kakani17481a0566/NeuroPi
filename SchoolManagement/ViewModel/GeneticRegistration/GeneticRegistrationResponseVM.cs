namespace SchoolManagement.ViewModel.GeneticRegistration
{
    public class GeneticRegistrationResponseVM
    {
        public string RegistrationNumber { get; set; }

        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string GeneticId { get; set; }

        public string ClassName { get; set; }

        public string Branch { get; set; }

        public string FatherName { get; set; }

        public string FatherOccupation { get; set; }

        public string MotherName { get; set; }

        public string MotherOccupation { get; set; }

        public string CountryCode { get; set; } = "+91";

        public string ContactNumber { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public decimal Height { get; set; }

        public decimal Weight { get; set; }

        public string FoodFrequency { get; set; }

        public string Consanguinity { get; set; }

        public string ParentsOccupation { get; set; }

        public string DietType { get; set; }

        public string Activity { get; set; }

        public string SleepDuration { get; set; }

        public string SleepQuality { get; set; }

        public string ScreenTime { get; set; }

        public string FoodTiming { get; set; }

        public string Fruits { get; set; }

        public string Vegetables { get; set; }

        public string FamilyType { get; set; }

        public int Siblings { get; set; }

        public string Vaccination { get; set; }

        public string NatureAccess { get; set; }

        public string PollutionAir { get; set; }

        public string PollutionNoise { get; set; }

        public string PollutionWater { get; set; }

        public string TravelTime { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
