using System;

namespace SchoolManagement.ViewModel.Book
{
    public class BookUpdateVM
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? UserId { get; set; }
        public int? InstitutionId { get; set; }
        public int? BooksTypeId { get; set; }
        public int UpdatedBy { get; set; }
    }
}
