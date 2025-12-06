namespace SchoolManagement.ViewModel.TimeTableTopics
{
    public class TimeTableTopicByDetailResponseVM
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public string SubjectName { get; set; }

        public int? TopicTypeId { get; set; }   // <-- nullable

        public string TopicTypeName { get; set; }

        public int TimeTableDetailId { get; set; }
    }
}
