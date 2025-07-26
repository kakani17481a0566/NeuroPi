using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("worksheets")]
    public class MWorksheet : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("location")]
        public string Location { get; set; }

        [ForeignKey("Tenant")]
        [Column("tenant_id")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }

        [ForeignKey("Subject")]
        [Column("subject_id")]
        public int? SubjectId { get; set; }
        public virtual MSubject Subject { get; set; }
    }
}
