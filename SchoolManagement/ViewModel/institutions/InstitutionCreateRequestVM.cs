


namespace SchoolManagement.ViewModel.institutions
{
    public class InstitutionCreateRequestVM
    {
        public string Name { get; set; }
        public int? ContactId { get; set; }
        public int TenantId { get; set; }

        public int CreatedBy { get; set; } 
    }
}
