namespace SchoolManagement.ViewModel.TimeTableDetail
{
    public class TimeTableDetailTableResponseVM
    {
        public List<TimeTableDetailTableHeaderVM> TableHeaders { get; set; }
        public List<TimeTableDetailTableRowVM> Data { get; set; }

        public class TimeTableDetailTableHeaderVM
        {
            public string Key { get; set; }          // e.g. "period"
            public string Label { get; set; }        // e.g. "Period"
            public string Type { get; set; }         // e.g. "object"
            public string DisplayField { get; set; } // e.g. "name"
        }

        public class TimeTableDetailTableRowVM
        {
            public int Id { get; set; }
            public TimeTableDetailIdNameVM Period { get; set; }
            public TimeTableDetailIdNameVM Subject { get; set; }
            public TimeTableDetailIdNameVM TimeTable { get; set; }
            public TimeTableDetailIdNameVM Teacher { get; set; }
            public TimeTableDetailIdNameVM Week { get; set; }     // <-- Added
            public TimeTableDetailIdNameVM Course { get; set; }   // <-- Added
        }

        public class TimeTableDetailIdNameVM
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
