using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.TimeTable;
using SchoolManagement.ViewModel.VTimeTable;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class TimeTableServiceImpl : ITimeTableServices
    {
        private readonly SchoolManagementDb _dbContext;

        public TimeTableServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TimeTableResponseVM> GetAll()
        {
            return _dbContext.TimeTables
                .Where(x => !x.IsDeleted)
                .Select(TimeTableResponseVM.FromModel)
                .ToList();
        }

        public List<TimeTableResponseVM> GetAll(int tenantId)
        {
            return _dbContext.TimeTables
                .Where(x => !x.IsDeleted && x.TenantId == tenantId)
                .Select(TimeTableResponseVM.FromModel)
                .ToList();
        }

        public TimeTableResponseVM GetById(int id)
        {
            var model = _dbContext.TimeTables.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            return TimeTableResponseVM.FromModel(model);
        }

        public TimeTableResponseVM GetById(int id, int tenantId)
        {
            var model = _dbContext.TimeTables
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            return TimeTableResponseVM.FromModel(model);
        }

        public TimeTableResponseVM Create(TimeTableRequestVM vm)
        {
            var entity = vm.ToModel();
            _dbContext.TimeTables.Add(entity);
            _dbContext.SaveChanges();
            return TimeTableResponseVM.FromModel(entity);
        }

        public TimeTableResponseVM Update(int id, int tenantId, TimeTableUpdateVM vm)
        {
            var entity = _dbContext.TimeTables
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null) return null;

            entity.Name = vm.Name;
            entity.Date = vm.Date;
            entity.WeekId = vm.WeekId;
            entity.HolidayId = vm.HolidayId;
            entity.Status = vm.Status;
            entity.UpdatedOn = DateTime.UtcNow;
            entity.UpdatedBy = vm.UpdatedBy;

            _dbContext.SaveChanges();
            return TimeTableResponseVM.FromModel(entity);
        }

        public bool Delete(int id, int tenantId)
        {
            var entity = _dbContext.TimeTables
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null) return false;

            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;

            _dbContext.SaveChanges();
            return true;
        }


        public WeekTimeTableData GetWeeklyTimeTable(int weekId, int tenantId, int courseId)
        {
            var query = _dbContext.VTimeTable
                .Where(x => x.TenantId == tenantId && x.CourseId == courseId);

            if (weekId > 0)
            {
                query = query.Where(x => x.WeekId == weekId);
            }
            else if (weekId == -1)
            {
                var today = DateTime.UtcNow.Date;
                query = query.Where(x => x.Date.Value.Date == today);
            }
            // weekId == 0 => get all weeks, no need to filter

            var records = query.ToList();
            if (!records.Any()) return null;

            var first = records.First();
            var timetableData = new List<TData>();

            var groupedByDate = records.GroupBy(x => x.Date.Value.Date).ToList();

            foreach (var group in groupedByDate)
            {
                var tData = new TData
                {
                    Column1 = group.First().Date?.ToString("dddd")
                };

                var periods = new string[6];

                for (int i = 1; i <= 6; i++)
                {
                    var periodItems = group
                        .Where(x => x.PeriodId == i && !new[] { "FT", "AS", "NR", "ET" }.Contains(x.TopicTypeCode))
                        .Select(x => string.IsNullOrWhiteSpace(x.Description) ? x.TopicName : x.Description)
                        .ToList();

                    periods[i - 1] = string.Join("\n", periodItems);
                }

                tData.Column2 = periods[0];
                tData.Column3 = periods[1];
                tData.Column4 = periods[2];
                tData.Column5 = periods[3];
                tData.Column6 = periods[4];
                tData.Column7 = periods[5];

                timetableData.Add(tData);
            }

            // Event Info
            var eventList = records
                .Where(x => x.TopicTypeName == "Event")
                .GroupBy(x => new { x.TopicName, x.Date })
                .Select(g => new EventInfo
                {
                    Name = g.Key.TopicName,
                    Date = g.Key.Date?.ToString("yyyy-MM-dd")
                })
                .ToList();

            // Headers
            var headers = new List<string> { "Days" };
            for (int i = 1; i <= 6; i++)
            {
                string header = $"Period {i}";

                foreach (var record in records)
                {
                    if (record.PeriodId == i && !string.IsNullOrWhiteSpace(record.SubjectName))
                    {
                        header = $"{record.SubjectName}\n{record.SubjectCode}";
                        break;
                    }
                }

                headers.Add(header);
            }

            return new WeekTimeTableData
            {
                Month = $"{first.Date?.ToString("MMMM")} {first.StartDate?.ToString("dd")} - {first.EndDate?.ToString("dd")}",
                WeekName = first.WeekName,
                Course = first.CourseName,
                Events = eventList,
                Headers = headers,
                TimeTableData = timetableData
            };
        }



    }
}
