using NeuroPi.UserManagment.Model;
using System;

namespace SchoolManagement.ViewModel.Book
{
    public class BookResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? UserId { get; set; }
        public int? InstitutionId { get; set; }
        public int? BooksTypeId { get; set; }
        public int TenantId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public static BookResponseVM FromModel(MBook book)
        {
            if (book == null) return null;

            return new BookResponseVM
            {
                Id = book.Id,
                Name = book.Name,
                ParentId = book.ParentId,
                UserId = book.UserId,
                InstitutionId = book.InstitutionId,
                BooksTypeId = book.BooksTypeId,
                TenantId = book.TenantId,
                CreatedOn = book.CreatedOn,
                CreatedBy = book.CreatedBy,
                UpdatedOn = book.UpdatedOn,
                UpdatedBy = book.UpdatedBy,
                IsDeleted = book.IsDeleted
            };
        }
        public static List<BookResponseVM> FromModelList(List<MBook> books)
        {
            if (books == null || books.Count == 0) return new List<BookResponseVM>();
            return books.Select(FromModel).ToList();
        }
    }
}
