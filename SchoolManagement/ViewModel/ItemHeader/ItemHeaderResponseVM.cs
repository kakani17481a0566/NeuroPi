using Microsoft.Extensions.Primitives;

namespace SchoolManagement.ViewModel.ItemHeader
{
    public class ItemHeaderResponseVM
    {
        public int BookId { get; set; }

        public DateTime CreatedOn { get; set; }

        public Book Book { get; set; }

        public decimal? Price { get; set; }

        public int Stock { get; set; }

        public string Status { get; set; }

        public string category { get; set; }

        public string Author { get; set; }

        public PublisherAddress PublisherAddress { get; set; }



    }
   public  class Book
    {
        public  string title { get; set; }

        public string coverImg { get; set; }
    }

    public class PublisherAddress 
    { 
        public string street { get; set; }

        public string line { get; set; }

    }

}
