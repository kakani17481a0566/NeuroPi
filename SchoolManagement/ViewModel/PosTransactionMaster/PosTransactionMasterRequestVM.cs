using SchoolManagement.Model;
using SchoolManagement.ViewModel.PosTransactionMaster;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.ViewModel.PosTransactionMaster
{
    public class PosTransactionMasterRequestVM
    {
        [Required(ErrorMessage = "The Student Id is required")]
        public int StudentId { get; set; }

        public int TenantId { get; set; }


        public List<PosItem> Items { get; set; }

        public string PaymentMethod { get; set; }


        


        public MPosTransactionMaster ToModel(PosTransactionMasterRequestVM requestVM)
        {
            return new MPosTransactionMaster
            {
                StudentId = requestVM.StudentId,
                TenantId = requestVM.TenantId
            };
        }
    }

    //public class Items
    //{
    //    public int Itemid { get; set; }

    //    public double UnitPrice { get; set; }

    //    public int Quantity { get; set; }

    //    public int GstPercentage { get; set; }

    //    public double GstValue { get; set; }

    //}


}
