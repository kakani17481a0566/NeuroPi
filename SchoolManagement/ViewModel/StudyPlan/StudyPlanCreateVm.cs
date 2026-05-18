namespace SchoolManagement.ViewModel.StudyPlan
{
    public class StudyPlanCreateVm
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
    }
}
