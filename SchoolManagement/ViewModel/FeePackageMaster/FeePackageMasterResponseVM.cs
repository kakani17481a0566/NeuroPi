namespace SchoolManagement.ViewModel.FeePackage
{
    public class FeePackageMasterResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int BranchId { get; set; }
        public string BranchName { get; set; }

        public int TenantId { get; set; }
        public string TenantName { get; set; }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }
}
