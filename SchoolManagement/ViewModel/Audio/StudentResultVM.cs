namespace SchoolManagement.ViewModel.Audio
{
    public class StudentResultVM
    {

        public string name {  get; set; }

        public string result { get; set;}

        public string url { get; set; }
        public StudentResultVM(string name, string result, string url)
        {
            this.name = name;
            this.result = result;
            this.url = url;
        }
    }
}
