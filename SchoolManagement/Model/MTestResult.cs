using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("test_result")]
    public class MTestResult
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("student_id")]
        public int StudentId { get; set; }

        [Column("test_content_id")]
        public int TestContentId { get; set; }

        [ForeignKey("TestContentId")]
        public MTestContent testContent { get; set; }

        [Column("result")]
        public string Result { get; set; }

        [Column("test_id")]
        public int TestId {  get; set; }

        [Column("relation_id")]
        public int RelationId { get; set; }

        

    }
}
