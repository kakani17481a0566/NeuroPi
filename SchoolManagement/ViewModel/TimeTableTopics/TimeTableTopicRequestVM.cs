using System;
using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.TimeTableTopics
{
    public class TimeTableTopicRequestVM
    {
        public int TopicId { get; set; }
        public int? TimeTableDetailId { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }

        public MTimeTableTopic ToModel()
        {
            return new MTimeTableTopic
            {
                TopicId = this.TopicId,
                TimeTableDetailId = this.TimeTableDetailId,
                TenantId = this.TenantId,
                CreatedBy = this.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    }
}
