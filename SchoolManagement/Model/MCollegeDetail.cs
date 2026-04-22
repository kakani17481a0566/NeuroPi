using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Model
{
    [Table("college_details")]
    public class MCollegeDetail : NeuroPi.UserManagment.Model.MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("college_name")]
        public string CollegeName { get; set; }

        [Column("address_line_1")]
        public string AddressLine1 { get; set; }

        [Column("address_line_2")]
        public string AddressLine2 { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("state")]
        public string State { get; set; }

        [Column("pincode")]
        public string Pincode { get; set; }

        [Column("contact_number")]
        public string ContactNumber { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual NeuroPi.UserManagment.Model.MTenant Tenant { get; set; }
    }
}
