using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.PostTransactionMaster
{
    public class PosTransactionMasterRequestVM
    {
        public int StudentId { get; set; }

        public int TenantId { get; set; }

        public List<Items> Items { get; set; }


        public MPosTransactionMaster ToModel(PosTransactionMasterRequestVM requestVM)
        {
            return new MPosTransactionMaster
            {
                StudentId = requestVM.StudentId,
                TenantId = requestVM.TenantId,
                
            };
        }
    }

    public class Items 
    { 
        public int Itemid { get; set; }

        public double UnitPrice { get; set; }

        public int Quantity { get; set; }

        public int GstPercentage { get; set; }

        public double GstValue { get; set; }

    }


}
