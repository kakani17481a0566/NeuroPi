using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.UserManagment.Model
{
    [Table("books")]
    public class MBook : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("parent_id")]
        public int? ParentId { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [Column("instution_id")]
        public int? InstitutionId { get; set; }

        [Column("books_type_id")]
        public int? BooksTypeId { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }
    }
}
