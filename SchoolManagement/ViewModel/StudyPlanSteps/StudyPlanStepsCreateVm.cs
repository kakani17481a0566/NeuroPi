namespace SchoolManagement.ViewModel.StudyPlanSteps
{
    public class StudyPlanStepsCreateVm
    {
        public int StudyPlanId { get; set; }
        public int? SeqOrd { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
    }
}
