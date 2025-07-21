namespace SchoolManagement.ViewModel.Topic
{
    public class TopicFullResponseVM
    {
        public List<string> Headers { get; set; }

        // ✅ Full list of enriched topics
        public List<TopicDetailVM> TDataTopic { get; set; }

        // ✅ courseId → courseName
        public Dictionary<int, string> Courses { get; set; }

        // ✅ subjectId → subjectName
        public Dictionary<int, string> Subjects { get; set; }

        // ✅ subjectId → courseId (relationship map)
        public Dictionary<int, int> SubjectCourseMap { get; set; }
    }
}
