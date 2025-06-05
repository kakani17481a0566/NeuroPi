using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Grade
{
    public class GradeRequestVM
    {
        public string Name { get; set; }
        public decimal MinPercentage { get; set; }
        public decimal MaxPercentage { get; set; }
        public string Description { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } 

        public static MGrade ToModel(GradeRequestVM grade)
        {
            return new MGrade()
            {
                Name = grade.Name,
                MinPercentage = grade.MinPercentage,
                MaxPercentage = grade.MaxPercentage,
                Description = grade.Description,
                TenantId = grade.TenantId,
                CreatedBy = grade.CreatedBy,
                CreatedOn = grade.CreatedOn
            };
        }

    } 
}
