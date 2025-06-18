using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
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
        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public static GradeResponseVM ToViewModel(MGrade Grade)
        {
            return new GradeResponseVM()
            {
                Id = Grade.Id,
                Name = Grade.Name,
                MinPercentage = Grade.MinPercentage,
                MaxPercentage = Grade.MaxPercentage,
                Description = Grade.Description,
                TenantId = Grade.TenantId,
                CreatedBy = Grade.CreatedBy,
                CreatedOn = Grade.CreatedOn,
                UpdatedBy = Grade.UpdatedBy,
                UpdatedOn = Grade.UpdatedOn,

            };

        }
        public static List<GradeResponseVM> ToViewModelList(List<MGrade> Grades)
        {
            List<GradeResponseVM> responseVMs = new List<GradeResponseVM>();
            foreach (var Grade in Grades)
            {
                responseVMs.Add(ToViewModel(Grade));

            }
            return responseVMs;

        }
    }
}
