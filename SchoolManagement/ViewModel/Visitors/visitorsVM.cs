namespace SchoolManagement.ViewModel.visitors
{
    public class visitorsVM
    { 
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string mobilenumber { get; set; }   
        public string purpose { get; set; }
        public DateTime in_time{ get; set; }
        public DateTime out_time { get; set; }
        public int tenant_id { get; set; }
        public int created_by { get; set; }
        public DateTime created_on { get; set; }
    }
}
