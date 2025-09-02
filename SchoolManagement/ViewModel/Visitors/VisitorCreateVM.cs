namespace SchoolManagement.ViewModel.visitors
{
    public class VisitorCreateVM
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string mobilenumber { get; set; }
        public string purpose { get; set; }
        public DateTime in_time { get; set; }
        public DateTime out_time { get; set; }

    }
}
