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

        // Added for Profile Update
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? BloodGroup { get; set; }
        public string? AdmissionNumber { get; set; }
        public string? AdmissionGrade { get; set; }
        public DateTime? DateOfJoining { get; set; }

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
