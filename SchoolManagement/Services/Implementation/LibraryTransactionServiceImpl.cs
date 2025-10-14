using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.LibraryTransctions;
using System.Runtime.ConstrainedExecution;


namespace SchoolManagement.Services.Implementation
{
    public class LibraryTransactionServiceImpl : ILibraryTransactionsService
    {
        private readonly SchoolManagementDb _db;
        private readonly string CHECKEDOUT = "checkedOut";
        private readonly string CHECKEDIN = "checkedIn";

        public LibraryTransactionServiceImpl(SchoolManagementDb dbcontext)
        {
            _db = dbcontext;
        }

        public string CreateLibraryTransaction(LibraryTransactionRequestVM ltRequestVm)
        {
           List<MLibraryTransaction> mLibraryTransactions = new List<MLibraryTransaction>();
            foreach (var bookId in ltRequestVm.BookIds)
            {
                MLibraryTransaction libraryTransaction = new MLibraryTransaction()
                {
                    StudentId = ltRequestVm.StudentId,
                    BookId = bookId,
                    CheckIn = DateOnly.FromDateTime(DateTime.Now),
                    CheckInBy = ltRequestVm.CheckInBy,
                    Status = CHECKEDIN
                };
                mLibraryTransactions.Add(libraryTransaction);
            }
            _db.LibraryTransaction.AddRange(mLibraryTransactions);
            _db.SaveChanges();

            return "inserted";

        }

        public LibraryTransactionResponseVM GetAllLibraryTransactions(int studentId)
        {
            var list = _db.LibraryTransaction
                .Where(e => e.Status == "checkedIn" && e.StudentId == studentId).Include(e => e.Book).Include(e => e.Student)
                .ToList();

            if (list == null || !list.Any())
            {
                return new LibraryTransactionResponseVM();
            }

                LibraryTransactionResponseVM result = new LibraryTransactionResponseVM();
                result.StudentId = list[0].StudentId;
                result.StudentName = list[0].Student.Name;
                List<Books> books = new List<Books>();
                foreach (var book in list)
                {
                    Books resultBook = new Books() 
                    { 
                        Id = book.Id,
                        BookId = book.BookId,
                        BookName = book.Book.Title,
                        AuthorName = book.Book.AuthorName,
                        CheckIn = book.CheckIn,
                        Status = book.Status
                    };
                    books.Add(resultBook);

                }
                result.Book = books;
                return result;
            




        }

        public string UpdateLibraryTransaction(LibraryTransactionUpdateVM ltRequestVm)
        {

            var transactions = _db.LibraryTransaction
                .Where(l => l.StudentId == ltRequestVm.StudentId && ltRequestVm.BookIds.Contains(l.BookId)).ToList();
            if (transactions == null) return "no books were found";
            List<MLibraryTransaction> updatedBooks=new List<MLibraryTransaction>();
            foreach (var book in transactions)
            {
                book.Status = CHECKEDOUT;
                book.CheckOut = DateOnly.FromDateTime(DateTime.Now);
                book.CheckOutBy = 1;
                updatedBooks.Add(book);
            }
            _db.UpdateRange(updatedBooks);
           int result= _db.SaveChanges();
            if (result > 0) return "updated";
         return "Something went wrong";
        }
    }
}
