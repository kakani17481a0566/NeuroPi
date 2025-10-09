using SchoolManagement.ViewModel.LibraryTransctions;

namespace SchoolManagement.Services.Interface
{
    public interface ILibraryTransactionsService
    {
        LibraryTransactionResponseVM GetAllLibraryTransactions( int studentId);

        string  CreateLibraryTransaction(LibraryTransactionRequestVM ltRequestVm);
    }
}
