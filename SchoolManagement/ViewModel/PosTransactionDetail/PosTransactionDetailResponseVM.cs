using SchoolManagement.Model;
using System.Collections.Generic;

namespace SchoolManagement.ViewModel.PosTransactionDetail
{
    public class PosTransactionDetailResponseVM
    {
        public int MasterTransactionId { get; set; }

        public string StudentName { get; set; }

        public int StudentId { get; set; }

        public string BranchName { get; set; }

        public string CourseName { get; set; }

        public List<Item> items { get; set; }
    }

    public class Item
    {
        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public double UnitPrice { get; set; }

        public int Quantity { get; set; }

        public int GstPercentage { get; set; }

        public double GstValue { get; set; }
    }
}
