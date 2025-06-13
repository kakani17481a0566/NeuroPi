using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("branch")]
    public class MBranch : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("contact")]
        public string Contact { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("pincode")]
        public string Pincode { get; set; }

        [Column("district")]
        public string District { get; set; }

        [Column("state")]
        public string State { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }

       
    }
}
