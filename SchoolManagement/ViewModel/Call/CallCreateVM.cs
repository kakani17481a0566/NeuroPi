namespace SchoolManagement.ViewModel.Call
{
    public class CallCreateVM
    {
        public int ContactId { get; set; }
        public int? StageId { get; set; }
        public string Remarks { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
        public IFormFile AudioFile { get; set; }
    }
}
