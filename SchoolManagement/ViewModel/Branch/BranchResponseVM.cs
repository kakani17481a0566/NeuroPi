using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Branch
{
    public class BranchResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }


        public static BranchResponseVM ToViewModel(MBranch branch)
        {
            return new BranchResponseVM
            {
                Id = branch.Id,
                Name = branch.Name,
                Contact = branch.Contact,
                Address = branch.Address,
                Pincode = branch.Pincode,
                District = branch.District,
                State = branch.State,
                TenantId = branch.TenantId,
                CreatedBy = branch.CreatedBy,
                CreatedOn = branch.CreatedOn,
                UpdatedBy = branch.UpdatedBy,
                UpdatedOn = branch.UpdatedOn
            };
        }


        public static List<BranchResponseVM> ToViewModelList(List<MBranch> branchList)
        {
            List<BranchResponseVM> result = new List<BranchResponseVM>();
            foreach (var branch in branchList)
            {
                result.Add(ToViewModel(branch));
            }
            return result;
        }
    }





}
