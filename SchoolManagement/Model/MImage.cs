using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("images")]
    public class MImage
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("url")]
        public byte[] url { get; set; }
    }
}
