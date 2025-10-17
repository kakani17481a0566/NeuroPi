using SchoolManagement.ViewModel.PosTransactionMaster;
//using SchoolManagement.ViewModel.PostTransactionMasterRequest.Items;

namespace SchoolManagement.ViewModel.PosTransactionDetail
{
    public class PosInvoiceVM
    {
        public int tenantId { get; set; }

        public string invoiceNumber { get; set; }

        public DateOnly date { get; set; }

        public string status { get; set; }

        public double subtotal { get; set; }

        public double gst { get; set; }

        public double total { get; set; }

        public Student student { get; set; }

        public Payment payment { get; set; } = new Payment();
        public Footer footer { get; set; } = new Footer();

        public List<PosItem> items { get; set; }




    }
    public class Student
    {
        public int studentId { get; set; }
        public string studentName { get; set; }
        public string className { get; set; }
        public string rollNo { get; set; }

        public string branch { get; set; }
    }
    public class Payment
    {
        public string method { get; set; } = "Cash";

        public string transactionId { get; set; } = "TRXN :1234";

        public string remarks { get; set; } = "No remarks";
    }
    public class Footer
    {
        public string thankYouNote { get; set; } = "Thank you for shopping with My School Italy International School!";

        public string supportEmail { get; set; } = "support@neuropi.edu.in";
        public string supportPhone { get; set; } = "+91-9876543210";
    }
    

}
