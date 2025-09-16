using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Test
{
    public class TestResponseVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static List<TestResponseVM> FromModel(List<MTest> test)
        {
           return  test.Select
                (t => new TestResponseVM()
                {
                    Id = t.id,
                    Name = t.name,
                }).ToList();
        }
    }
}
