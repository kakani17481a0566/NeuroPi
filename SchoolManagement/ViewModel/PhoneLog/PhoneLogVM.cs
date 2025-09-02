using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.PhoneLog
{
    public class PhoneLogVM
    {
        public int tenant_id { get; set; }
        public int branch_id { get; set; }
        public string from_name { get; set; }
        public string to_name { get; set; }
        public string from_number { get; set; }
        public string to_number { get; set; }
        public string purpose { get; set; }
        public string call_duration { get; set; }
        public int CreatedBy { get; set; }

        // Service expects this
        public MPhoneLog ToModel() => new MPhoneLog
        {
            tenant_id = tenant_id,
            branch_id = branch_id,
            from_name = from_name,
            to_name = to_name,
            from_number = from_number,
            to_number = to_number,
            purpose = purpose,
            call_duration = call_duration,
            CreatedBy = CreatedBy
        };

        // Optional helper (not used by your service currently)
        public static PhoneLogVM FromModel(MPhoneLog p) => new PhoneLogVM
        {
            tenant_id = p.tenant_id,
            branch_id = p.branch_id,
            from_name = p.from_name,
            to_name = p.to_name,
            from_number = p.from_number,
            to_number = p.to_number,
            purpose = p.purpose,
            call_duration = p.call_duration,
            CreatedBy = p.CreatedBy
        };
    }
}
