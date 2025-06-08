namespace SchoolManagement.ViewModel.VTimeTable
{
    public class TData
    {
        public String Column1 { get; set; }


        public String Column2 { get; set; }

        public String Column3 { get; set; }

        public String Column4 { get; set; }

        public String Column5 { get; set; }
        public String Column6 { get; set; }

        public String Column7 { get; set; }

        public String Column8 { get; set; }

    }


    public class WeekTimeTableData
    {
        public string Month { get; set; }
        public string WeekName { get; set; }

        public string Course { get; set; }

        public string Event { get; set; }

        public string EventDate { get; set; }

        public List<TData> TimeTableData { get; set; }
    }

}
