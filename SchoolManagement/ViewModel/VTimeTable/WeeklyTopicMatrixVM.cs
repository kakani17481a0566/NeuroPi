namespace SchoolManagement.ViewModel.VTimeTable
{


    public class WeeklyTopicMatrixVM
    {
        public string? Month { get; set; }
        public string? Week { get; set; }
        public string? Course { get; set; }
        public List<string> Headers { get; set; } = new();
        public List<WeeklyTopicMatrixRow> DataTerm { get; set; } = new();
    }


}
