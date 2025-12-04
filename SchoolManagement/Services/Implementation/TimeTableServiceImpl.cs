using Microsoft.EntityFrameworkCore;
using NeuroPi.UserManagment.Response;
using NodaTime;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.TableFile;
using SchoolManagement.ViewModel.TimeTable;
using SchoolManagement.ViewModel.VTimeTable;
using SchoolManagement.ViewModel.Week;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using static SchoolManagement.ViewModel.TableFile.TableFileResponse;

namespace SchoolManagement.Services.Implementation
{
    public class TimeTableServiceImpl : ITimeTableServices
    {
        private readonly SchoolManagementDb _dbContext;
        private readonly ILogger _logger;


        public TimeTableServiceImpl(SchoolManagementDb dbContext, ILogger<TimeTableServiceImpl> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
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



        // Time table


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
            entity.CourseId = vm.CourseId; 
            entity.AssessmentStatusCode = vm.AssessmentStatusCode;
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


        // weekly time table if 

        public WeekTimeTableData GetWeeklyTimeTable(int weekId, int tenantId, int courseId)
        {
            var query = _dbContext.VTimeTable
                .Where(x => x.TenantId == tenantId && x.CourseId == courseId);
            _logger.LogInformation("query is", query);
           
            

            if (weekId > 0)
            {
                query = query.Where(x => x.WeekId == weekId);
            }
            else if (weekId == -1)
            {
                var today = DateTime.UtcNow.Date;
                query = query.Where(x => x.Date.HasValue && x.Date.Value.Date == today);
            }

            var records = query.ToList();
            if (!records.Any()) return null;

            var first = records.First();
            var groupedByDate = records.GroupBy(x => x.Date.Value.Date);
            var timetableData = new List<TData>();

            // Group course files by type
            var courseFiles = _dbContext.TableFiles
                .Where(f => f.CourseId == courseId && f.TimeTableId==first.TimeTableId && !f.IsDeleted)
                .ToList();

            var groupedResources = courseFiles
                .GroupBy(f => f.Type?.ToLower() ?? "unknown")
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(f => new TableFileResponse
                    {
                        Name = f.Name,
                        Link = f.Link,
                        Type = f.Type
                    }).ToList()
                );
            var periodStart = _dbContext.Periods.Where(p => p.CourseId == courseId && !p.IsDeleted).Min(p => p.Id);
            var periodEnd = _dbContext.Periods.Where(p => p.CourseId == courseId && !p.IsDeleted).Max(p => p.Id);

            // Build timetable data per day
            foreach (var group in groupedByDate)
            {
                var date = group.Key;
                var timeTableId = group.First().TimeTableId;
                var assessmentId = group.First().assessmentId;

                var tData = new TData
                {
                    Column1 = group.First().Date?.ToString("dddd")
                };

                var periods = new string[6];
                int index = 0;
               


                for (int i = periodStart; i <= periodEnd; i++)
                {
                    var periodItems = group
                        .Where(x => x.PeriodId == i)
                        .Select(x =>
                        {
                            var topic = string.IsNullOrWhiteSpace(x.TopicTypeCode)
                                ? x.TopicName
                                : $"{x.TopicTypeCode}: {x.TopicName}";

                            return string.IsNullOrWhiteSpace(x.Description)
                                ? topic
                                : $"{topic} - {x.Description}";
                        })
                        .ToList();

                    periods[index] = string.Join("\n", periodItems);
                    index++;
                }

                tData.Column2 = periods[0];
                tData.Column3 = periods[1];
                tData.Column4 = periods[2];
                tData.Column5 = periods[3];
                tData.Column6 = periods[4];
                tData.Column7 = periods[5];
                tData.Column7 = periods[5];
                tData.timeTableId = (int)timeTableId;
                tData.assessmentStausCodeId = assessmentId;

                // Column8: First PDF link
                tData.Column8 = _dbContext.TableFiles
                    .Where(f => f.TimeTableId == timeTableId && f.Type == "pdf" && !f.IsDeleted && f.CourseId == courseId)
                    .Select(f => f.Link)
                    .FirstOrDefault();

                // Column9: First worksheet link
                // Column9: First worksheet link
                tData.Column9 = string.Join("\n",
    (from ttw in _dbContext.TimeTableWorksheets
     join w in _dbContext.Worksheets on ttw.WorksheetId equals w.Id
     join t in _dbContext.TimeTables on ttw.TimeTableId equals t.Id
     where ttw.TimeTableId == timeTableId && t.CourseId == courseId
     orderby w.Id
     select w.Location).ToList());


                timetableData.Add(tData);

            }

            // Events
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
            for (int i = periodStart; i <=periodEnd ; i++)
            {
                var subjectRecord = records
                    .FirstOrDefault(r => r.PeriodId == i && !string.IsNullOrWhiteSpace(r.SubjectName));

                string header = subjectRecord != null
                    ? $"{subjectRecord.SubjectName}\n{subjectRecord.SubjectCode}"
                    : $"Period {i}";

                headers.Add(header);
            }

            return new WeekTimeTableData
            {
                Month = $"{first.Date?.ToString("MMMM")} {first.StartDate?.ToString("dd")} - {first.EndDate?.ToString("dd")}",
                WeekName = first.WeekName,
                Course = first.CourseName,
                courseId=(int)first.CourseId,
                Events = eventList,
                Headers = headers,
                TimeTableData = timetableData,
                Resources = groupedResources,
                CurrentDate = DateTime.UtcNow.ToString("dd/MM/yyyy")
            };
        }

        public TimeTableFullResponseVM GetWeekTimeTable(int courseId, int tenantId)
        {
            var model = _dbContext.TimeTables
        .Include(x => x.Week)
        .Include(x => x.Course)
        .Include(x => x.AssessmentStatus)
        .Include(x => x.PublicHoliday)   
        .FirstOrDefault(x =>
            x.CourseId == courseId &&
            x.TenantId == tenantId &&
            !x.IsDeleted);

            if (model == null)
                return null;

            return new TimeTableFullResponseVM
            {
                Id = model.Id,
                Name = model.Name,
                Date = model.Date,

                WeekId = model.WeekId,
                WeekName = model.Week?.Name,

                HolidayId = model.HolidayId,
                HolidayName = model.PublicHoliday?.Name,

                Status = model.Status,

                CourseId = model.CourseId,
                CourseName = model.Course?.Name,

                AssessmentStatusCode = model.AssessmentStatusCode,
                AssessmentStatusName = model.AssessmentStatus?.Name,

                TenantId = model.TenantId
            };



        }




        //public MTableFileResponseVM GetAllByCourseId(int courseId)
        //{
        //    var result = _dbContext.TableFiles.Where(f => f.CourseId == courseId).ToList();
        //    List<TableFileResponse> pdfs = new List<TableFileResponse>();
        //    List<TableFileResponse> videos = new List<TableFileResponse>();

        //    if (result != null && result.Count > 0)
        //    {
        //        foreach (var file in result)
        //        {
        //            if (file.Type.Equals("pdf"))
        //            {
        //                pdfs.Add(TableFileResponse.ToViewModel(file));
        //            }
        //            if (file.Type.Equals("mp4"))
        //            {
        //                videos.Add(TableFileResponse.ToViewModel(file));
        //            }


        //        }
        //        return new MTableFileResponseVM()
        //        {
        //            pdfs = pdfs,
        //            videos = videos,
        //        };
        //    }
        //    return null;





        public TimeTableData GetAllStructured(int tenantId)
        {
            // Project only required fields directly into the ViewModel
            var dataList = _dbContext.TimeTables
                .Where(x => !x.IsDeleted && x.TenantId == tenantId)
                .AsNoTracking()
                .Select(r => new TDataTimeTable
                {
                    Id = r.Id,
                    Name = r.Name,
                    Date = r.Date,
                    WeekId = r.WeekId,
                    WeekName = r.Week != null ? r.Week.Name : null,
                    CourseId = r.CourseId,
                    CourseName = r.Course != null ? r.Course.Name : null,
                    AssessmentStatusCode = r.AssessmentStatusCode,
                    AssessmentStatusName = r.AssessmentStatus != null ? r.AssessmentStatus.Name : null,
                    TenantId = r.TenantId,
                    TenantName = r.Tenant != null ? r.Tenant.Name : null,
                    Status = r.Status           // <-- Ensure Status is included!
                })
                .ToList();

            var headers = new Dictionary<string, string>
    {
        { "Id", "Time Table ID" },
        { "Name", "Title" },
        { "Date", "Scheduled Date" },
        { "WeekId", "Week ID" },
        { "WeekName", "Week Name" },
        { "CourseId", "Course ID" },
        { "CourseName", "Course" },
        { "AssessmentStatusCode", "Assessment Status ID" },
        { "AssessmentStatusName", "Assessment Status" },
        { "TenantId", "Tenant ID" },
        { "TenantName", "Tenant" },
        { "Status", "Status" } // <-- ADD Status to headers!
    };

            // Build filters from dataList (no extra DB queries or object graphs)
            var weeks = dataList
                .Where(x => x.WeekId.HasValue && !string.IsNullOrWhiteSpace(x.WeekName))
                .GroupBy(x => x.WeekId.Value)
                .ToDictionary(g => g.Key, g => g.First().WeekName);

            var courses = dataList
                .Where(x => x.CourseId.HasValue && !string.IsNullOrWhiteSpace(x.CourseName))
                .GroupBy(x => x.CourseId.Value)
                .ToDictionary(g => g.Key, g => g.First().CourseName);

            var assessmentStatuses = dataList
                .Where(x => x.AssessmentStatusCode.HasValue && !string.IsNullOrWhiteSpace(x.AssessmentStatusName))
                .GroupBy(x => x.AssessmentStatusCode.Value)
                .ToDictionary(g => g.Key, g => g.First().AssessmentStatusName);

            return new TimeTableData
            {
                Headers = headers,
                TimeTableDataList = dataList,
                Filters = new FilterOptions
                {
                    Weeks = weeks,
                    Courses = courses,
                    AssessmentStatuses = assessmentStatuses
                }
            };
        }


        public TimeTableInsertTableOptionsVM GetInsertOptions(int tenantId)
        {
           
            var result = new TimeTableInsertTableOptionsVM
            {
                Weeks = _dbContext.Weeks
                   .Where(w => !w.IsDeleted && w.TenantId==tenantId) // ✅ safer than !w.IsDeleted
                    .Select(w => new IdNameVM { Id = w.Id, Name = w.Name })
                   .ToList(),


                Holidays = _dbContext.PublicHolidays
                    .Where(h => !h.IsDeleted && h.TenantId == tenantId)
                    .Select(h => new IdNameVM { Id = h.Id, Name = h.Name })
                    .ToList(),

                Courses = _dbContext.Courses
                    .Where(c => !c.IsDeleted && c.TenantId == tenantId)
                    .Select(c => new IdNameVM { Id = c.Id, Name = c.Name })
                    .ToList(),

                Tenants = _dbContext.Tenants
                    .Where(t => !t.IsDeleted && t.TenantId == tenantId)
                    .Select(t => new IdNameVM { Id = t.TenantId, Name = t.Name })
                    .ToList(),

                AssessmentStatuses = _dbContext.Masters
                    .Where(m => !m.IsDeleted && m.MasterTypeId == 40 && m.TenantId==tenantId)
                    .Select(m => new CodeNameVM
                    {
                        Code = m.Id,
                        Name = m.Name
                    })
                    .ToList(),

                StatusOptions = new List<string> { "working", "holiday" }
            };

            return result;
           
        }





    }
}
