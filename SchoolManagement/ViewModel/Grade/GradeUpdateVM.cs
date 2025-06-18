namespace SchoolManagement.ViewModel.Grade
{
    public class GradeUpdateVM
    {
        public string Name { get; set; }

        public decimal MinPercentage { get; set; }


        public decimal MaxPercentage { get; set; }


        public string Description { get; set; }

        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
