namespace SchoolManagement.ViewModel.Topic
{
    public class TopicUpdateVM
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int SubjectId { get; set; }
        public int TopicTypeId { get; set; }
        public int UpdatedBy { get; set; }
    }
}
