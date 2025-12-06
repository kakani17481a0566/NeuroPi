using SchoolManagement.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.ViewModel.Topic
{
    public class TopicRequestVM
    {
        [Required(ErrorMessage = "Topic Name is required")]
        public string Name { get; set; }

        public string Code { get; set; }
        public string Description { get; set; }

        // 🔥 REQUIRED FIX
        [Required(ErrorMessage = "SubjectId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid SubjectId")]
        public int SubjectId { get; set; }

        // Topic Type (optional depending on use-case)
        [Required(ErrorMessage = "TopicTypeId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid TopicTypeId")]
        public int TopicTypeId { get; set; }

        [Required(ErrorMessage = "TenantId is required")]
        public int TenantId { get; set; }

        [Required(ErrorMessage = "CreatedBy is required")]
        public int CreatedBy { get; set; }

        public MTopic ToModel()
        {
            return new MTopic
            {
                Name = this.Name,
                Code = this.Code,
                Description = this.Description,

                // 🔥 ENSURED VALID NOW
                SubjectId = this.SubjectId,
                TopicTypeId = this.TopicTypeId,

                TenantId = this.TenantId,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = this.CreatedBy,
                IsDeleted = false
            };
        }
    }
}
