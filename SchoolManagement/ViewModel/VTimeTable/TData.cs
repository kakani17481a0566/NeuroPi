using SchoolManagement.ViewModel.TableFile;
using System.Collections.Generic;

namespace SchoolManagement.ViewModel.VTimeTable
{
    public class TData
    {
        public string Column1 { get; set; } // Day
        public string Column2 { get; set; } // Period 1
        public string Column3 { get; set; } // Period 2
        public string Column4 { get; set; } // Period 3
        public string Column5 { get; set; } // Period 4
        public string Column6 { get; set; } // Period 5
        public string Column7 { get; set; } // Period 6
        //public string Column8 { get; set; }
        public List<string> Column8 { get; set; } // or string[] Column8


        public string Column9 { get; set; }
        public int timeTableId { get; set; }
        public int assessmentStausCodeId{ get; set; }

        // ⬅️ New list for video resources

    }

    public class EventInfo
    {
        public string Name { get; set; }
        public string Date { get; set; } // Format: yyyy-MM-dd
    }

    public class WeekTimeTableData
    {

        public string CurrentDate { get; set; }
        public string Month { get; set; }
        public string WeekName { get; set; }
        public string Course { get; set; }
        public int courseId { get; set; }

        public List<EventInfo> Events { get; set; }

        public List<string> Headers { get; set; }
        public List<TData> TimeTableData { get; set; } 



        public Dictionary<string, List<TableFileResponse>> Resources { get; set; } = new();

       


    }
}
