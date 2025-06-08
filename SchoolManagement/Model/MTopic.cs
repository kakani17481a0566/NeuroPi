using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("topics")]
    public class MTopic : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Required]
        [Column("subject_id")]
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual MSubject Subject { get; set; }

        [Column("topic_type_id")]
        public int? TopicTypeId { get; set; }

        [Required]
        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
