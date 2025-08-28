using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("test_content")]
    public class MTestContent
    {
        [Column("id")]
        public int id { get; set; }
        [Column("name")]
        public string name { get; set; }
        [Column("relatedid")]
        public int relationId { get; set; }
        [Column("url")]
        public byte[]? url { get; set; }
        
        [Column("test_id")]
        public int testId { get; set; }
        [ForeignKey("testId")]
        public MTest Test { get; set; }

        [Column("tenant_id")]
        public int tenantId { get; set; }

        [ForeignKey("tenantId")]
        public MTenant tenant { get; set; }
        [Column("is_deleted")]
        public bool isDeleted { get; set; }
    }
}
