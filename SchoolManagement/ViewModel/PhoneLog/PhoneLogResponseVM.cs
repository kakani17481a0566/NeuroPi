using System;
using System.ComponentModel;
using System.Linq.Expressions;
using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.PhoneLog
{
    public class PhoneLogResponseVM
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

        // Use inside IQueryable.Select(...)
        public static readonly Expression<Func<MPhoneLog, PhoneLogResponseVM>> Map =
            p => new PhoneLogResponseVM
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

        // Use when you already have an entity instance
        public static PhoneLogResponseVM FromEntity(MPhoneLog p) => new PhoneLogResponseVM
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

        public static PhoneLogResponseVM FromModel(MPhoneLog model)
        {
            return new PhoneLogResponseVM
            {
               
                tenant_id = model.tenant_id,
                branch_id = model.branch_id,
                from_name = model.from_name,
                to_name = model.to_name,
                from_number = model.from_number,
                to_number = model.to_number,
                purpose = model.purpose,
                call_duration = model.call_duration,
                CreatedBy = model.CreatedBy
            };
        }
    }
}
