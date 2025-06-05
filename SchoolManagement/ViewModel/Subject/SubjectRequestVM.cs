using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Subject
{
    public class SubjectRequestVM
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public MSubject ToModel() => new MSubject
        {
            Name = this.Name,
            Code = this.Code,
            Description = this.Description,
            TenantId = this.TenantId,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = this.CreatedBy
        };
    }
}
