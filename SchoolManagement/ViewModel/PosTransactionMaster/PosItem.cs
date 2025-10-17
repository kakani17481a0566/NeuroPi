namespace SchoolManagement.ViewModel.PosTransactionMaster
{
    public class PosItem
    {
        public int Itemid { get; set; }

        public string itemName { get; set; }

        public double UnitPrice { get; set; }

        public int Quantity { get; set; }

        public int GstPercentage { get; set; }

        public double GstValue { get; set; }
    }
}
