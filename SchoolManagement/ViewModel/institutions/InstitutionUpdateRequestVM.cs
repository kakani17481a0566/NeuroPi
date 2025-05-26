namespace SchoolManagement.ViewModel.Institutions
{
    public class InstitutionUpdateRequestVM
    {
        public string Name { get; set; }
        public int? ContactId { get; set; }
        //public int TenantId { get; set; }

        public int UpdatedBy { get; set; } 
    }
}
