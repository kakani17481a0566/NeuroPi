using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("time_table_topics")]
    public class MTimeTableTopic : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("subject_id")]
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual MSubject Subject { get; set; }

        [Column("time_table_id")]
        [ForeignKey("TimeTable")]
        public int TimeTableId { get; set; }
        public virtual MTimeTable TimeTable { get; set; }

        [Column("topic_id")]
        [ForeignKey("Topic")]
        public int TopicId { get; set; }
        public virtual MTopic Topic { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
