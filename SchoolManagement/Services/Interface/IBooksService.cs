using System.Collections.Generic;
using SchoolManagement.ViewModel.Book;

namespace SchoolManagement.Services.Interface
{
    public interface IBooksService
    {
        List<BookResponseVM> GetAllBooks(int tenantId);
        BookResponseVM GetBookById(int id, int tenantId);
        BookResponseVM CreateBook(BookRequestVM book);
        BookResponseVM UpdateBook(int id, int tenantId, BookUpdateVM book);

        bool DeleteBook(int id, int tenantId);

        List<BookResponseVM> GetAllBooks();
        BookResponseVM GetBookById(int id);

    }
}
