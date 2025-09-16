using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.CountingTest;

namespace SchoolManagement.Services.Implementation
{
    public class CountingTestServiceImpl : ICountingTestInterface
    {
        public List<CountingResponseVM> GetResult(string label, string shape)
        {
            List<CountingResponseVM> list = new List<CountingResponseVM>(5);
            for (int i = 0; i < 5; i++)
            {
                CountingResponseVM count = new CountingResponseVM();
                count.Shape = shape;
                count.label = label;
                Random random = new Random();
                int randomNumber = random.Next(1, 21);
                count.count = randomNumber;
                list.Add(count);

            }
            return list;
        }
    }
}
