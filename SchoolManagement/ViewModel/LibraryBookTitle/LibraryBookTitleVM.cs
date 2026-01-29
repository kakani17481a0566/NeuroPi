using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.ViewModel.LibraryBookTitle
{
    public class LibraryBookTitleVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public DateTime? PublishedOn { get; set; }
        public string PublishedBy { get; set; }

        public int? CategoryId { get; set; }

        public int? MinAge { get; set; }

        public int? GenreId { get; set; }
        public int? LocId { get; set; }

        public decimal? Price { get; set; }
        public int TenantId { get; set; }

    }
}
