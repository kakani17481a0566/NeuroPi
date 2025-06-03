using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("parent_student")]
    public class MParentStudent : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("parent_id")]
        public int ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual MParent Parent { get; set; }

        [Column("student_id")]
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public virtual MStudent Student { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey("TenantId")]
        public virtual MTenant Tenant { get; set; }
    }
}
