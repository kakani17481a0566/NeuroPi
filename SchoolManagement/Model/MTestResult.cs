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
        public int Student_Id { get; set; }

        [Column("test_content_id")]
        public int Test_Content_Id { get; set; }

        [Column("result")]
        public string Result { get; set; }

    }
}
