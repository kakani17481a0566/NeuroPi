using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Genres
{
    public class GenresResponseVM
    {

        public int id { get; set; }

        public string Name { get; set; }

        public string image { get; set; }


        public static GenresResponseVM ToViewModel(MGenere genere)
        {
            return new GenresResponseVM()
            {
                id = genere.Id,
                Name = genere.Name,

                image = genere.imageUrl
            };
        }

        public  static List<GenresResponseVM> toViewModelList(List<MGenere> result)
        {
           return  result.Select(x => ToViewModel(x)).ToList();
            
        }
    }
}
