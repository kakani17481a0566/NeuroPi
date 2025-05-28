using System;
using System.Collections.Generic;
using System.Linq;
using NeuroPi.UserManagment.Model;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Book;

namespace SchoolManagement.Services.Implementation
{
    public class BooksServiceImpl : IBooksService
    {
        private readonly SchoolManagementDb _dbContext;

        public BooksServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }

        // Retrieves all books for a specific tenant
        // Developed by: Mohith
        public List<BookResponseVM> GetAllBooks(int tenantId)
        {
            return _dbContext.Books
                .Where(b => !b.IsDeleted && b.TenantId == tenantId)
                .Select(BookResponseVM.FromModel)
                .ToList();
        }

        // Retrieves a book by its ID for a specific tenant
        // Developed by: Mohith
        public BookResponseVM GetBookById(int id, int tenantId)
        {
            var book = _dbContext.Books
                .FirstOrDefault(b => b.Id == id && b.TenantId == tenantId && !b.IsDeleted);
            return BookResponseVM.FromModel(book);
        }

        // Creates a new book and saves it to the database
        // Developed by: Mohith
        public BookResponseVM CreateBook(BookRequestVM bookVM)
        {
            var book = bookVM.ToModel();
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
            return BookResponseVM.FromModel(book);
        }

        // Updates an existing book by its ID
        // Developed by: Mohith
        public BookResponseVM UpdateBook(int id, int tenantId, BookUpdateVM bookVM)
        {
            var existingBook = _dbContext.Books
                .FirstOrDefault(b => b.Id == id && b.TenantId == tenantId && !b.IsDeleted);

            if (existingBook == null)
                return null;

            existingBook.Name = bookVM.Name;
            existingBook.ParentId = bookVM.ParentId;
            existingBook.UserId = bookVM.UserId;
            existingBook.InstitutionId = bookVM.InstitutionId;
            existingBook.BooksTypeId = bookVM.BooksTypeId;
            existingBook.UpdatedBy = bookVM.UpdatedBy;
            existingBook.UpdatedOn = DateTime.UtcNow;

            _dbContext.SaveChanges();

            return BookResponseVM.FromModel(existingBook);
        }

        // Deletes a book by marking it as deleted
        // Developed by: Mohith
        public bool DeleteBook(int id, int tenantId)
        {
            var book = _dbContext.Books
                .FirstOrDefault(b => b.Id == id && b.TenantId == tenantId && !b.IsDeleted);

            if (book == null)
                return false;

            book.IsDeleted = true;
            book.UpdatedOn = DateTime.UtcNow;

            _dbContext.SaveChanges();
            return true;
        }
    }
}
