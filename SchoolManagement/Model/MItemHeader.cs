using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model

{

    [Table("item_header", Schema = "public")]

    public class MItemHeader : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("author_name")]
        public string AuthorName { get; set; }

        [Column("published_on")]
        public DateTime? PublishedOn { get; set; }

        [Column("published_by")]
        public string? PublishedBy { get; set; }

        [ForeignKey("Category")]
        [Column("category_id")]
        public int? CategoryId { get; set; }


        [Column("min_age")]
        public int? MinAge { get; set; }

        [ForeignKey("Genre")]
        [Column("genre_id")]
        public int? GenreId { get; set; }

        [Column("name")]
        public MGenere Genre { get; set; }

        [ForeignKey("Location")]
        [Column("loc_id")]
        public int? LocId { get; set; }

        [Column("price")]
        public decimal? Price { get; set; }



        [ForeignKey("Tenant")]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        public virtual MTenant Tenant { get; set; }
    }
}
