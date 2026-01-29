namespace SchoolManagement.ViewModel.LibraryBookTitle
{
    public class LibraryBookTitleRequestVM
    {
        public int? CategoryId { get; set; }
        public int? MinAge { get; set; }
        public int? LocId { get; set; }

        // Added for Create/Update
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; } // Matches frontend "category" string
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public string? Status { get; set; }
        public int TenantId { get; set; }
    }
}
