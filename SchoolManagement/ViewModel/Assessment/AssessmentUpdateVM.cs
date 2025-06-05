namespace SchoolManagement.ViewModel.Assessment
{
    public class AssessmentUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TopicId { get; set; }
        public int AssessmentSkillId { get; set; }
        public int UpdatedBy { get; set; }
        
    }
}
