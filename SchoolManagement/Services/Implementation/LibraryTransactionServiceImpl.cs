using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.LibraryTransctions;

namespace SchoolManagement.Services.Implementation
{
    public class LibraryTransactionServiceImpl : ILibraryTransactionsService
    {
        private readonly SchoolManagementDb _db;

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
                    Status = "checkedIn"
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
            if (list!= null)
            {
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
            return new LibraryTransactionResponseVM();




        }

        public string UpdateLibraryTransaction(LibraryTransactionRequestVM ltRequestVm)
        {

            //var transactions = _db.LibraryTransaction
            //    .Where(l => l.StudentId == ltRequestVm.StudentId && l.Status == "checkedIn").ToList();

            
            //if (transactions == null) return null;
            //List<>


            //for (int i = ltRequestVm.BookIds.Count - 1; i >= 0; i--)
            //{
            //    foreach (var bookId in transactions)
            //    {
            //        if (transactions.BookId == bookId)
            //        {
            //            transactions.CheckOutBy = ltRequestVm.CheckOutBy;
            //            transactions.Status = "checkedOut";
            //            transactions.CheckOut = DateOnly.FromDateTime(DateTime.Now);
            //        }
            //    }

            //}
            
            return "updated";




        }
    }
}
