using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{ 

    [Table("pos_transaction_details")]
    public class MPostTransactionDetails
    {

    [Key]
    public int Id { get; set; }
        // Foreign key to
    [Column("master_transaction_id")]
    public int MasterTransactionId { get; set; }
    [ForeignKey("MasterTransactionId")]
    public MPostTransactionMaster MasterTransaction { get; set; }
        // Foreign key to Items
    [Column("item_id")]
    public int ItemId { get; set; }
    [ForeignKey("ItemId")]
    public MItem Item { get; set; }
    [Column("unit_price")]
    public double UnitPrice { get; set; }
    [Column("quantity")]
    public int Quantity { get; set; }
    [Column("gst_percentage")]
    public int GstPercentage { get; set; }
        [Column("gst_value")]
    public int GstValue { get; set; }
}
}
