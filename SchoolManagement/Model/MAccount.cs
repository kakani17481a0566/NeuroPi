using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.UserManagment.Model
{
    [Table("accounts")]
    public class MAccount : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("book_id")]
        public int? BookId { get; set; }

        [Column("acc_name")]
        public string AccName { get; set; }

        [Column("upi_phone_no")]
        public string UpiPhoneNo { get; set; }

        [Column("acc_number")]
        public string AccNumber { get; set; }

        [Column("cc_number")]
        public string CcNumber { get; set; }

        [Column("acc_type")]
        public string AccType { get; set; }

        [Column("branch")]
        public string Branch { get; set; }

        [Column("bank_name")]
        public string BankName { get; set; }

        [Column("ifsc_code")]
        public string IfscCode { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }
    }
}
