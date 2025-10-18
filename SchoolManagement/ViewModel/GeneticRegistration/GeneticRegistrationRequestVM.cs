using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.GeneticRegistration
{
    public class GeneticRegistrationRequestVM
    {
        public string studentName { get; set; }
        public int studentId { get; set; }

        public string ClassName { get; set; }
        public string Branch { get; set; }
        public string FatherName { get; set; }
        public string FatherOccupation { get; set; }

        public string MotherName { get; set; }
        public string MotherOccupation { get; set; }
        public string CountryCode { get; set; }
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

        public static MGeneticRegistration ToModel(GeneticRegistrationRequestVM request)
        {
            return new MGeneticRegistration()
            {
                StudentName=request.studentName,
                StudentId=request.studentId,
                ClassName=request.ClassName,
                Branch=request.Branch,
                FatherName=request.FatherName,
                FatherOccupation=request.FatherOccupation,
                MotherName=request.MotherName,
                MotherOccupation=request.MotherOccupation,
                CountryCode=request.CountryCode,
                ContactNumber=request.ContactNumber,
                Age=request.Age,
                Gender=request.Gender,
                Height=request.Height,  
                Weight=request.Weight,
                FoodFrequency=request.FoodFrequency,
                Consanguinity=request.Consanguinity,
                ParentsOccupation=request.ParentsOccupation,
                DietType=request.DietType,
                Activity=request.Activity,
                SleepDuration=request.SleepDuration,
                SleepQuality =request.SleepQuality,
                ScreenTime=request.ScreenTime,
                FoodTiming=request.FoodTiming,
                Fruits=request.Fruits,
                Vegetables=request.Vegetables,
                FamilyType=request.FamilyType,
                Siblings=request.Siblings,
                Vaccination=request.Vaccination,
                NatureAccess=request.NatureAccess,
                PollutionAir=request.PollutionAir,
                PollutionNoise=request.PollutionNoise,
                PollutionWater=request.PollutionWater,
                TravelTime=request.TravelTime,
            };
        }

    }
}
