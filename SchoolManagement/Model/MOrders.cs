using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("orders")]
    public class MOrders
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required]
        [Column("supplier_id")]
        public int supplier_id { get; set; }

        [Required]
        [Column("order_date")]
        public DateTime order_date { get; set; } 

        [Column("exp_date")]
        public DateTime? exp_date { get; set; }

        [Column("delivery_address")]
        public string? delivery_address { get; set; } 

        [Column("delivered_date")]
        public DateTime? delivered_date { get; set; }

        [Column("order_status")]
        public string? order_status { get; set; } 

        [Column("trx_id")]
        public string? trx_id { get; set; } 

        [Column("order_type_id")]
        public int? order_type_id { get; set; }

        [Column("tenant_id")]
        public int? tenant_id { get; set; }

        [Column("created_on")]
        public DateTime created_on { get; set; } 
        [Column("created_by")]
        public int? created_by { get; set; }

        [Column("updated_on")]
        public DateTime updated_on { get; set; }

        [Column("updated_by")]
        public int? updated_by { get; set; }

        [Column("is_deleted")]
        public bool is_deleted { get; set; } 
    }
}
