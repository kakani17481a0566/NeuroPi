using NeuroPi.UserManagment.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("term")] 
    public class MTerm : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }


        [Column("name")]
        public string Name { get; set; }


        [Column("start_date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        public virtual MTenant Tenant { get; set; }
    }
}
