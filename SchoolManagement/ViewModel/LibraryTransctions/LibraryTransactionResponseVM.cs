using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.LibraryTransctions
{
    public class LibraryTransactionResponseVM
    {
        public int Id { get; set; }

        public int? StudentId { get; set; }

        public int BookId { get; set; }

        public string BookName { get; set; }

        public string AuthorName { get; set; }

        public DateOnly CheckIn { get; set; }

        public DateOnly CheckOut { get; set; }

        public int CheckInBy { get; set; }

        public int CheckOutBy { get; set; }

        public string Status { get; set; }

        public static LibraryTransactionResponseVM ToViewModel(MLibraryTransaction transaction)
        {
            if (transaction == null) return null;
            return new LibraryTransactionResponseVM
            {
                Id = transaction.Id,
                StudentId = transaction.StudentId,
                BookId = transaction.BookId ?? 0,
                BookName = transaction.Book.Title,
                AuthorName = transaction.Book.AuthorName,
                CheckIn = transaction.CheckIn,
                Status = transaction.Status
            };
        }

        public static  List<LibraryTransactionResponseVM> ToViewModelList(List<MLibraryTransaction> transactionList)
        {
            List<LibraryTransactionResponseVM> result = new List<LibraryTransactionResponseVM>();
            foreach (var transaction in transactionList)
            {
                result.Add(ToViewModel(transaction));
            }
            return result;
        }
    }
}
