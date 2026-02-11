using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Nutrition.Model
{
    [Table("carpidum")]
    public class MCarpedium
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("gender")]
        public string Gender { get; set; }
        [Column("studentname")]
        public string StudentName { get; set; }
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        [Column("qrcode")]
        public  Guid QrCode { get; set; }
        [Column("email")]
        public string gmail { get; set; }
    }
}
