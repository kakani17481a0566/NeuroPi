namespace SchoolManagement.ViewModel.Students
{
    public class StudentRequestVM
    {
        public string Name { get; set; }
        public int CourseId { get; set; }
        public int TenantId { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        public Model.MStudent ToModel()
        {
            return new Model.MStudent
            {
                Name = this.Name,
                CourseId = this.CourseId,
                TenantId = this.TenantId,
                BranchId = this.BranchId,
                CreatedBy = this.CreatedBy,
                UpdatedBy = this.UpdatedBy,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    }
}
