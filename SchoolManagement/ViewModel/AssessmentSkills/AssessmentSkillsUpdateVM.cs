namespace SchoolManagement.ViewModel.AssessmentSkills
{
    public class AssessmentSkillsUpdateVM
    {
        public string Name { get; set; }

        public string Code { get; set; }
        public string Description { get; set; }
        public int SubjectId { get; set; }

        public int TenantId { get; set; }

        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
