namespace SchoolManagement.ViewModel.PhoneLog
{
    public class PhoneLogCreateVM
    {
        public string from_name { get; set; }
        public string to_name { get; set; }
        public string from_number { get; set; }
        public string to_number { get; set; }
        public string purpose { get; set; }
        public string call_duration { get; set; }
        public int branch_id { get; set; }
        public int tenant_id { get; set; }
        public int CreatedBy { get; set; }
    }
}
