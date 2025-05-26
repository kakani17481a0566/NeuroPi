using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    public class MItemHeader : MBaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public DateTime? PublishedOn { get; set; }
        public string PublishedBy { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }

        public int? MinAge { get; set; }

        [ForeignKey("Genre")]
        public int? GenreId { get; set; }

        [ForeignKey("Location")]
        public int? LocId { get; set; }

        public decimal? Price { get; set; }

        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        public virtual MTenant Tenant { get; set; }
    }
}
