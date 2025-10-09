using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
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
        public List<LibraryTransactionResponseVM> GetAllLibraryTransactions(int studentId)
        {
            var list = _db.LibraryTransaction
                .Where(e=>e.Status=="checkedIn" && e.StudentId==studentId).Include(e=>e.Book).Include(e=>e.Student)
                .ToList();
            return  LibraryTransactionResponseVM.ToViewModelList(list);


        }
    }
}
