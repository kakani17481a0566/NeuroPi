using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    public class MItemLocation : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        
        [Column("branch_id")]
        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public MBranch Branch { get; set; }

        [Required]
        [ForeignKey("Tenant")]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        public virtual MTenant Tenant { get; set; }
    }

}
