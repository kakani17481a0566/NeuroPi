using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.PhoneLog
{
    public class PhoneLogRequestVM
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
        public MPhoneLog ToModel()
        {
            return new MPhoneLog
            {
                from_name = from_name,
                to_name = to_name,
                from_number = from_number,
                to_number = to_number,
                purpose = purpose,
                call_duration = call_duration,
                branch_id = branch_id,
                tenant_id = tenant_id,
                CreatedBy = CreatedBy, // comes from MBaseModel
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };


            
         }
        public  MPhoneLog FromModel()
        {
            return new MPhoneLog
            {
                from_name = from_name,
                to_name = to_name,
                from_number = from_number,
                to_number = to_number,
                purpose = purpose,
                call_duration = call_duration,
                branch_id = branch_id,
                tenant_id = tenant_id,
                CreatedBy = CreatedBy, // comes from MBaseModel
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    } 
}
