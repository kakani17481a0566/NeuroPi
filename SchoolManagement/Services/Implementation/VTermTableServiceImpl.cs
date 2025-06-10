using Dapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.VTimeTable;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class VTermTableServiceImpl : IMVTermTableService
    {
        private readonly SchoolManagementDb _dbContext;

        public VTermTableServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }

        public List<MVTermTableVM> GetVTermTableData(int tenantId, int courseId, int termId)
        {
            var query = _dbContext.VTermTable
                .Where(x => x.TenantId == tenantId && x.CourseId == courseId);

            if (termId != 0)
                query = query.Where(x => x.TermId == termId);

            var data = query
                .Select(x => new MVTermTableVM
                {
                    TermId = x.TermId,
                    TermName = x.TermName,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Date = x.Date,
                    DayName = x.DayName,
                    CourseId = x.CourseId,
                    CourseName = x.CourseName,
                    SubjectName = x.SubjectName,
                    SubjectCode = x.SubjectCode,
                    TopicName = x.TopicName,
                    TopicDescription = x.TopicDescription,
                    TopicTypeName = x.TopicTypeName,
                    TopicTypeCode = x.TopicTypeCode,
                    PeriodName = x.PeriodName,
                    Status = x.Status,
                    TenantId=tenantId,
                    HolidayId=x.HolidayId,
                    TimeTableId = x.TimeTableId,
                    TopicId= x.TopicId,
                    TimeTableDetailId = x.TimeTableDetailId,
                    PeriodId= x.PeriodId,
                    SubjectId=x.SubjectId,
                    TimeTableTopicId=x.TimeTableTopicId,
                    
                    WeekStart = x.StartDate,
                    WeekEnd = x.EndDate

                })
                .ToList();

            return data;
        }


        public MVTermTableWeeklyMatrixVM GetTermTableWeeklyMatrix(int tenantId, int courseId, int termId)
        {
            var result = _dbContext.VTermTable
                .Where(x => x.TenantId == tenantId && x.CourseId == courseId && x.TermId == termId)
                .OrderBy(x => x.Date)
                .ToList();

            if (!result.Any())
                return new MVTermTableWeeklyMatrixVM();

            var term = result.First();

            var vm = new MVTermTableWeeklyMatrixVM
            {
                Month = $"{term.TermName} {term.StartDate?.ToString("yyyy-MM-dd")} - {term.EndDate?.ToString("yyyy-MM-dd")}",
                Week = $"{term.WeekName} ({term.WeekStart?.ToString("yyyy-MM-dd")} - {term.WeekEnd?.ToString("yyyy-MM-dd")})",
                //Week = $"{term.WeekName} ({term.WeekStart?.ToString("dd")} - {term.WeekEnd?.ToString("dd")})",


                //Week = term.WeekName,
                Course = term.CourseName,
                Headers = new List<string>
        {
            "TERM",
            "Fairytale/ActionSong/NurseryRhyme/Event",
            "Monday", "Tuesday", "Wednesday", "Thursday", "Friday"
                    //--"Saturday", "Sunday"
        },
                DataTerm = new List<WeeklyTopicMatrixRow>()
            };

            var groupedByWeek = result
                .GroupBy(x => new { x.WeekId, x.WeekName })
                .OrderBy(g => g.Key.WeekId);
       

            foreach (var weekGroup in groupedByWeek)
            {
                var weekStart = weekGroup.Min(x => x.Date)?.ToString("dd") ?? "";
                var weekEnd = weekGroup.Max(x => x.Date)?.ToString("dd") ?? "";
                var row = new WeeklyTopicMatrixRow
                {
                    COLUM1 = $"{weekGroup.Key.WeekName} ({weekStart} - {weekEnd})",
                    COLUM2 = "",
                    COLUM3 = "",
                    COLUM4 = "",
                    COLUM5 = "",
                    COLUM6 = "",
                    COLUM7 = ""
                    //COLUM8 = "",
                    //COLUM9 = ""
                };

                // Fill COLUM2 ONLY with FT, AS, NR, ET Topic Names grouped by TopicTypeCode
                var groupedHeaderTopics = weekGroup
                    .Where(x => new[] { "FT", "AS", "NR" }.Contains(x.TopicTypeCode))
                    .GroupBy(x => x.TopicTypeCode)
                    .Select(g =>
                    {
                        var topicNames = g
                            .Select(x => x.TopicName)
                            .Where(name => !string.IsNullOrWhiteSpace(name))
                            .Distinct();

                        return $"{g.Key}: {string.Join(", ", topicNames)}";
                    });

                row.COLUM2 = string.Join(", ", groupedHeaderTopics);

                // Now populate weekdays COLUM3 - COLUM9 with all topic names (no TopicTypeCode filter)
                foreach (var dayGroup in weekGroup.GroupBy(x => x.DayName))
                {
                    var cellTextList = new List<string>();
                    foreach (var period in dayGroup.OrderBy(x => x.PeriodId))
                    {
                        //if (!string.IsNullOrWhiteSpace(period.TopicName))
                        //    cellTextList.Add(period.TopicName);

                        if (!string.IsNullOrWhiteSpace(period.TopicName) && !new[] { "FT", "AS", "NR"}.Contains(period.TopicTypeCode))
                            cellTextList.Add(period.TopicName);

                    }

                    var joined = string.Join(", ", cellTextList);
                    switch (dayGroup.Key?.Trim().ToLower())
                    {
                        case "monday": row.COLUM3 = joined; break;
                        case "tuesday": row.COLUM4 = joined; break;
                        case "wednesday": row.COLUM5 = joined; break;
                        case "thursday": row.COLUM6 = joined; break;
                        case "friday": row.COLUM7 = joined; break;
                        //case "saturday": row.COLUM8 = joined; break;
                        //case "sunday": row.COLUM9 = joined; break;
                    }
                }

                vm.DataTerm.Add(row);
            }

            return vm;
        }



    }
}

