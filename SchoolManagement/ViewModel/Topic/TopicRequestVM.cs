using SchoolManagement.Model;
using System;

namespace SchoolManagement.ViewModel.Topic
{
    public class TopicRequestVM
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int SubjectId { get; set; }
        public int TopicTypeId { get; set; }
        public int TenantId { get; set; }

        public MTopic ToModel()
        {
            return new MTopic
            {
                Name = this.Name,
                Code = this.Code,
                Description = this.Description,
                SubjectId = this.SubjectId,
                TopicTypeId = this.TopicTypeId,
                TenantId = this.TenantId,
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}
