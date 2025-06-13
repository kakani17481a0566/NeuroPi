namespace SchoolManagement.ViewModel.Branch
{
    public class BranchUpdateVM
    {
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }


    }
}
