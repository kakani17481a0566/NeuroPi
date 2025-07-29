using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.TimeTableDetail;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class TimeTableDetailServiceImpl : ITimeTableDetailService
    {
        private readonly SchoolManagementDb _dbContext;

        public TimeTableDetailServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TimeTableDetailResponseVM> GetAll()
        {
            return _dbContext.TimeTableDetails
                .Where(x => !x.IsDeleted)
                .Select(TimeTableDetailResponseVM.FromModel)
                .ToList();
        }

        public List<TimeTableDetailResponseVM> GetAll(int tenantId)
        {
            return _dbContext.TimeTableDetails
                .Where(x => !x.IsDeleted && x.TenantId == tenantId)
                .Select(TimeTableDetailResponseVM.FromModel)
                .ToList();
        }

        public TimeTableDetailResponseVM GetById(int id)
        {
            var item = _dbContext.TimeTableDetails
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            return TimeTableDetailResponseVM.FromModel(item);
        }

        public TimeTableDetailResponseVM GetById(int id, int tenantId)
        {
            var item = _dbContext.TimeTableDetails
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            return TimeTableDetailResponseVM.FromModel(item);
        }

        public TimeTableDetailResponseVM Create(TimeTableDetailRequestVM vm)
        {
            var entity = vm.ToModel();
            _dbContext.TimeTableDetails.Add(entity);
            _dbContext.SaveChanges();

            return TimeTableDetailResponseVM.FromModel(entity);
        }

        public TimeTableDetailResponseVM Update(int id, int tenantId, TimeTableDetailUpdateVM vm)
        {
            var entity = _dbContext.TimeTableDetails
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null)
                return null;

            entity.PeriodId = vm.PeriodId;
            entity.SubjectId = vm.SubjectId;
            entity.TimeTableId = vm.TimeTableId;
            //entity.TeacherId = vm.TeacherId;
            entity.UpdatedOn = DateTime.UtcNow;
            entity.UpdatedBy = vm.UpdatedBy;

            _dbContext.SaveChanges();

            return TimeTableDetailResponseVM.FromModel(entity);
        }

        public bool Delete(int id, int tenantId)
        {
            var entity = _dbContext.TimeTableDetails
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null)
                return false;

            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;

            _dbContext.SaveChanges();
            return true;
        }





        public TimeTableDetailTableResponseVM GetTableDetails(int tenantId)
        {
            // 1. Define headers (add week and course)
            var headers = new List<TimeTableDetailTableResponseVM.TimeTableDetailTableHeaderVM>
    {
        new TimeTableDetailTableResponseVM.TimeTableDetailTableHeaderVM
        {
            Key = "period", Label = "Period", Type = "object", DisplayField = "name"
        },
        new TimeTableDetailTableResponseVM.TimeTableDetailTableHeaderVM
        {
            Key = "subject", Label = "Subject", Type = "object", DisplayField = "name"
        },
        new TimeTableDetailTableResponseVM.TimeTableDetailTableHeaderVM
        {
            Key = "timeTable", Label = "Time Table", Type = "object", DisplayField = "name"
        },
        new TimeTableDetailTableResponseVM.TimeTableDetailTableHeaderVM
        {
            Key = "teacher", Label = "Teacher", Type = "object", DisplayField = "name"
        },
        new TimeTableDetailTableResponseVM.TimeTableDetailTableHeaderVM
        {
            Key = "week", Label = "Week", Type = "object", DisplayField = "name"
        },
        new TimeTableDetailTableResponseVM.TimeTableDetailTableHeaderVM
        {
            Key = "course", Label = "Course", Type = "object", DisplayField = "name"
        }
    };

            // 2. Query with navigation properties included
            var data = _dbContext.TimeTableDetails
                .Include(x => x.Period)
                .Include(x => x.Subject)
                .Include(x => x.TimeTable)
                    .ThenInclude(tt => tt.Week)
                .Include(x => x.TimeTable)
                    .ThenInclude(tt => tt.Course)
                .Include(x => x.Teacher)
                .Where(x => !x.IsDeleted && x.TenantId == tenantId)
                .Select(x => new TimeTableDetailTableResponseVM.TimeTableDetailTableRowVM
                {
                    Id = x.Id,
                    Period = x.Period == null
                        ? null
                        : new TimeTableDetailTableResponseVM.TimeTableDetailIdNameVM
                        { Id = x.Period.Id, Name = x.Period.Name },
                    Subject = x.Subject == null
                        ? null
                        : new TimeTableDetailTableResponseVM.TimeTableDetailIdNameVM
                        { Id = x.Subject.Id, Name = x.Subject.Name },
                    TimeTable = x.TimeTable == null
                        ? null
                        : new TimeTableDetailTableResponseVM.TimeTableDetailIdNameVM
                        { Id = x.TimeTable.Id, Name = x.TimeTable.Name },
                    Teacher = x.Teacher == null
                        ? null
                        : new TimeTableDetailTableResponseVM.TimeTableDetailIdNameVM
                        {
                            Id = x.Teacher.UserId,
                            Name = (x.Teacher.FirstName ?? "") + " " + (x.Teacher.LastName ?? "")
                        },
                    Week = x.TimeTable != null && x.TimeTable.Week != null
                        ? new TimeTableDetailTableResponseVM.TimeTableDetailIdNameVM
                        {
                            Id = x.TimeTable.Week.Id,
                            Name = x.TimeTable.Week.Name
                        }
                        : null,
                    Course = x.TimeTable != null && x.TimeTable.Course != null
                        ? new TimeTableDetailTableResponseVM.TimeTableDetailIdNameVM
                        {
                            Id = x.TimeTable.Course.Id,
                            Name = x.TimeTable.Course.Name
                        }
                        : null
                })
                .ToList();

            // 3. Return the response
            return new TimeTableDetailTableResponseVM
            {
                TableHeaders = headers,
                Data = data
            };
        }


        public TimeTableDetailInsertOptionsVM GetInsertOptions(int tenantId)
        {
            var weeks = _dbContext.Weeks
                .Where(w => !w.IsDeleted && w.TenantId == tenantId)
                .Select(w => new TimeTableDetailDropDownVM
                {
                    Id = w.Id,
                    Name = w.Name,
                    CourseId = null,
                    WeekId = w.Id,
                    PeriodId = null
                })
                .ToList();

            var coursesDb = _dbContext.Courses
                .Where(c => !c.IsDeleted && c.TenantId == tenantId)
                .Include(c => c.Subjects.Where(s => !s.IsDeleted && s.TenantId == tenantId))
                .ToList();

            var courses = coursesDb.Select(c => new CourseWithSubjectsVM
            {
                Id = c.Id,
                Name = c.Name,
                Subjects = c.Subjects.Select(s => new SubjectWithWeeksVM
                {
                    Id = s.Id,
                    Name = s.Name,
                    Weeks = weeks.Select(w => new TimeTableDetailDropDownVM
                    {
                        Id = w.Id,
                        Name = w.Name,
                        CourseId = c.Id,
                        WeekId = w.Id,
                        PeriodId = null
                    }).ToList()
                }).ToList()
            }).ToList();

            var periods = _dbContext.Periods
                .Where(p => !p.IsDeleted && p.TenantId == tenantId)
                .Select(p => new TimeTableDetailDropDownVM
                {
                    Id = p.Id,
                    Name = p.Name,
                    CourseId = p.CourseId,
                    WeekId = null,
                    PeriodId = p.Id
                })
                .ToList();

            var timeTables = _dbContext.TimeTables
                .Where(t => !t.IsDeleted && t.TenantId == tenantId)
                .Select(t => new TimeTableDetailDropDownVM
                {
                    Id = t.Id,
                    Name = t.Name,
                    CourseId = t.CourseId,
                    WeekId = t.WeekId,
                    PeriodId = null
                })
                .ToList();

            var teachers = _dbContext.Users
                .Where(u => !u.IsDeleted && u.TenantId == tenantId)
                .Select(u => new TimeTableDetailDropDownVM
                {
                    Id = u.UserId,
                    Name = $"{u.FirstName} {u.LastName}".Trim(),
                    CourseId = null,
                    WeekId = null,
                    PeriodId = null
                })
                .ToList();

            return new TimeTableDetailInsertOptionsVM
            {
                Courses = courses,
                Periods = periods,
                TimeTables = timeTables,
                Teachers = teachers
            };
        }


    }
}
