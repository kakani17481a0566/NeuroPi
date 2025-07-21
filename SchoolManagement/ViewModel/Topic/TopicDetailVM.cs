namespace SchoolManagement.ViewModel.Topic
{
    public class TopicDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        // Resolved values
        public string SubjectName { get; set; }
        public string CourseName { get; set; } // ✅ Added Course Name
        public string TopicTypeName { get; set; }
        public string TenantName { get; set; }
    }
}
