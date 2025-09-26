namespace SchoolManagement.ViewModel.TestResult
{
    public class TestResultResponseVM
    {
        public int relationId { get; set; }

        public int testId { get; set; }
        public int testContentId { get; set; }


        //public int testContentId { get; set; }

        public string name { get; set; }

        public  string  result { get; set; }
        public byte[] url { get; set; }

        
    }
}
