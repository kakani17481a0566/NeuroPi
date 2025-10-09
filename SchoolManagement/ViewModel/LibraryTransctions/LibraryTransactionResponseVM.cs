using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.LibraryTransctions
{
    public class LibraryTransactionResponseVM
    {
        

        public int? StudentId { get; set; }

        public string StudentName { get; set; }


        public List<Books> Book { get; set; }

        
    }

    public class Books
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string BookName { get; set; }

        public string AuthorName { get; set; }

        public DateOnly CheckIn { get; set; }

        public string Status { get; set; }



    }

}
