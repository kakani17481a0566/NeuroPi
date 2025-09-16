using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table(("test"))]
    public class MTest
    {
        [Column("id")]
        public int id { get; set; }
        [Column("name")]
        public string name { get; set; }
        [Column("tenant_id")]
        public int tenant_id { get; set; }
        [Column("is_deleted")]
        public bool isDeleted { get; set; }

        [Column("master_type_id")]
        public int MasterTypeId { get; set; }
    }
}
