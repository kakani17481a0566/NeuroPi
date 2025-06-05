using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Grade
{
    public class GradeResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal MinPercentage { get; set; }

        public decimal MaxPercentage { get; set; }

        public string Description { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }


        public static GradeResponseVM ToViewModel(MGrade grade)
        {
            return new GradeResponseVM
            {
                Id = grade.Id,
                Name = grade.Name,
                MinPercentage = grade.MinPercentage,
                MaxPercentage = grade.MaxPercentage,
                Description = grade.Description,
                TenantId = grade.TenantId,
                CreatedBy = grade.CreatedBy,
                UpdatedBy = grade.UpdatedBy,
                CreatedOn = grade.CreatedOn,
                UpdatedOn = DateTime.UtcNow
            };
        }
        public static List<GradeResponseVM> ToViewModelList(List<MGrade> models)
        {
           List<GradeResponseVM> result = new List<GradeResponseVM>();
            foreach (var model in models)
            {
                result.Add(ToViewModel(model));
            }
            return result;
        }
    }
}