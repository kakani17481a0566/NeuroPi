using System;
using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.TimeTable;

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
            var records = _dbContext.VTimeTable
                .Where(x => x.WeekId == weekId && x.TenantId == tenantId && x.CourseId == courseId)
                      .ToList();

            if (!records.Any()) return null;

            var first = records.First();

            var timetableData = new List<TData>();

            var groupedByDate = records.GroupBy(x => x.Date.Value.Date).ToList();

            for (int g = 0; g < groupedByDate.Count; g++)
            {
                var group = groupedByDate[g];

                var tData = new TData
                {
                    Column1 = group.First().Date?.ToString("dddd")
                };

                var periods = new string[6];

                for (int i = 1; i <= 6; i++)
                {
                    var periodItems = new List<string>();

                    foreach (var x in group)
                    {
                        if (x.PeriodId == i)
                        {
                            var content = string.IsNullOrWhiteSpace(x.Description) ? x.TopicName : x.Description;
                            periodItems.Add(content);
                        }
                    }

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

            // Build event list using for loop
            var eventList = new List<EventInfo>();
            var eventGroups = records
                .Where(x => x.TopicTypeName == "Event")
                .GroupBy(x => new { x.TopicName, x.Date })
                .ToList();

            for (int i = 0; i < eventGroups.Count; i++)
            {
                var group = eventGroups[i];
                var item = new EventInfo
                {
                    Name = group.Key.TopicName,
                    Date = group.Key.Date?.ToString("yyyy-MM-dd")
                };
                eventList.Add(item);
            }

            // Build headers dynamically using for loop
            var headers = new List<string> { "Days" };
            for (int i = 1; i <= 6; i++)
            {
                string header = $"Period {i}";

                for (int j = 0; j < records.Count; j++)
                {
                    var record = records[j];
                    if (record.PeriodId == i && !string.IsNullOrWhiteSpace(record.SubjectName))
                    {
                        header = $"{record.SubjectName}\n{record.SubjectCode}";
                        break;
                    }
                }

                headers.Add(header);
            }

            var response = new WeekTimeTableData
            {
                Month = $"{first.Date?.ToString("MMMM")} {first.StartDate?.ToString("dd")} - {first.EndDate?.ToString("dd")}",
                WeekName = first.WeekName,
                Course = first.CourseName,
                Events = eventList,
                Headers = headers,
                TimeTableData = timetableData
            };

            return response;
        }



    }
}
