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

        [Required]
        [Column("topic_id")]
        [ForeignKey(nameof(Topic))]
        public int TopicId { get; set; }
        public virtual MTopic Topic { get; set; }

        [Column("time_table_detail_id")]
        [ForeignKey(nameof(TimeTableDetail))]
        public int? TimeTableDetailId { get; set; }
        public virtual MTimeTableDetail TimeTableDetail { get; set; }

        [Required]
        [Column("tenant_id")]
        [ForeignKey(nameof(Tenant))]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }

}
