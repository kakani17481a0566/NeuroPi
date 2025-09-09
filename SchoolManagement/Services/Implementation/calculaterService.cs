using SchoolManagement.Services.Interface;

namespace SchoolManagement.Services.Implementation
{
    public class calculaterService : claculater
    {
        public int Add(int A, int B)
        {
            int C= A+B;
            Console.WriteLine("Adding of two numbers " + C);
            return C;
        }

        public int mul(int A, int B)
        {
            int C = A*B;
            Console.WriteLine(C);
            return C;
        }

        public int sub(int A, int B)
        {
            int C = A - B;
            Console.WriteLine(C);
            return C;
        }
        public int div(int A ,int B)
        {
            int C = A / B;
            Console.WriteLine(C);
            return C;
        }
        public string fullName(string A,string B)
        {
            string fullName = A + " " + B;

            Console.WriteLine(fullName);
            return fullName;
        }
    }
}
