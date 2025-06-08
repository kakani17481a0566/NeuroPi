using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.TimeTableTopics
{
    public class TimeTableTopicResponseVM
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public int? TimeTableDetailId { get; set; }
        public int TenantId { get; set; }

        public static TimeTableTopicResponseVM FromModel(MTimeTableTopic model)
        {
            if (model == null) return null;

            return new TimeTableTopicResponseVM
            {
                Id = model.Id,
                TopicId = model.TopicId,
                TimeTableDetailId = model.TimeTableDetailId,
                TenantId = model.TenantId
            };
        }
    }
}
