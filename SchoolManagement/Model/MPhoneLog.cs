using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("phonelog")]
    public class MPhoneLog : MBaseModel
    {
        [Key]
        [Column("id")]
        public int id { get; set; }   // <- PK

        [Required, Column("from_name"), MaxLength(100)]
        public string from_name  {get; set; }

        [Required, Column("to_name"), MaxLength(100)]
        public string to_name { get; set; }

        [Required, Column("from_number"), MaxLength(10)]
        public string from_number { get; set; }

        [Required, Column("to_number"), MaxLength(10)]
        public string to_number { get; set; }

        [Required, Column("purpose"), MaxLength(100)]
        public string purpose { get; set; }

        [Required, Column("call_duration"), MaxLength(100)]
        public string call_duration { get; set; }

        [Required, Column("branch_id")]
        public int branch_id { get; set; }

        [Required, Column("tenant_id")]
        public int tenant_id { get; set; }

        [ForeignKey(nameof(branch_id))]
        public virtual MBranch Branch { get; set; }

        [ForeignKey(nameof(tenant_id))]
        public virtual MTenant Tenant { get; set; }
    }
}
