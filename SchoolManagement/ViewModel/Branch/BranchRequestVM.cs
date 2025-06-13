using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Branch
{
    public class BranchRequestVM
    {
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }


        public static MBranch ToModel(BranchRequestVM requestVm)
        {
            return new MBranch()
            {
                Name = requestVm.Name,
                Contact = requestVm.Contact,
                Address = requestVm.Address,
                Pincode = requestVm.Pincode,
                District = requestVm.District,
                State = requestVm.State,
                TenantId = requestVm.TenantId,
                CreatedBy = requestVm.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };

        }
    }
}
