using SchoolManagement.ViewModel.LibraryTransctions;

namespace SchoolManagement.Services.Interface
{
    public interface ILibraryTransactionsService
    {
        List<LibraryTransactionResponseVM> GetAllLibraryTransactions( int studentId);
    }
}
