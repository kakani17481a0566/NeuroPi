using System;
using System.Collections.Generic;

namespace SchoolManagement.ViewModel.TimeTableTopics
{
    public class TimeTableTopicsVM
    {
        public Dictionary<string, string> Headers { get; set; } = new()
        {
            { "id", "ID" },
            { "topicId", "Topic ID" },
            { "timeTableDetailId", "Time Table Detail ID" },
            { "topicName", "Topic Name" },
            { "subjectName", "Subject Name" },
            { "courseName", "Course Name" },
            { "periodId", "Period ID" },
            { "periodName", "Period Name" },
            { "timeTableName", "Time Table Name" },
            { "timeTableDate", "Time Table Date" }
        };

        public List<TDataTimeTableTopic> TDataTimeTableTopics { get; set; } = new();
    }

    public class TDataTimeTableTopic
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public int? TimeTableDetailId { get; set; }
        public string TopicName { get; set; }
        public string SubjectName { get; set; }
        public string CourseName { get; set; }
        public int? PeriodId { get; set; }
        public string PeriodName { get; set; }
        public string TimeTableName { get; set; }
        public DateTime? TimeTableDate { get; set; }
    }
}
