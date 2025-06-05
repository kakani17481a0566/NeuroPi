using System;

namespace SchoolManagement.ViewModel.institutions
{
    public class InstitutionResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ContactId { get; set; }
        public int TenantId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
