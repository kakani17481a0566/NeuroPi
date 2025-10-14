namespace SchoolManagement.ViewModel.PostTransactionMaster
{
    public class PosTransactionMasterResponseVM
    {
        public int Id { get; set; }
        public int StudentId { get; set; }

        public int TenantId { get; set; }   

        public DateOnly Date { get; set; }

        public string StudentName { get; set; }

        public string BranchName { get; set; }
    }
}
