using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Grade
{
    public class GraderequestVM
    {
        public string Name { get; set; }

        public decimal MinPercentage { get; set; }

        
        public decimal MaxPercentage { get; set; }

      
        public string Description { get; set; }

       
        public int TenantId { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }


        public static MGrade ToModel(GraderequestVM model)
        {
            return new MGrade()
            {
                Name = model.Name,
                MinPercentage = model.MinPercentage,
                MaxPercentage = model.MaxPercentage,
                Description = model.Description,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
            };



        }

    }
}
