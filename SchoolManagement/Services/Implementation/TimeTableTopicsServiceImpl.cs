using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.TimeTableTopics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class TimeTableTopicsServiceImpl : ITimeTableTopicsService
    {
        private readonly SchoolManagementDb _dbContext;

        public TimeTableTopicsServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TimeTableTopicResponseVM> GetAll()
        {
            return _dbContext.TimeTableTopics
                .Where(x => !x.IsDeleted)
                .Select(TimeTableTopicResponseVM.FromModel)
                .ToList();
        }

        public List<TimeTableTopicResponseVM> GetAllByTenantId(int tenantId)
        {
            return _dbContext.TimeTableTopics
                .Where(x => x.TenantId == tenantId && !x.IsDeleted)
                .Select(TimeTableTopicResponseVM.FromModel)
                .ToList();
        }

        public TimeTableTopicResponseVM GetById(int id)
        {
            var entity = _dbContext.TimeTableTopics.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            return TimeTableTopicResponseVM.FromModel(entity);
        }

        public TimeTableTopicResponseVM GetByIdAndTenantId(int id, int tenantId)
        {
            var entity = _dbContext.TimeTableTopics
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            return TimeTableTopicResponseVM.FromModel(entity);
        }

        public TimeTableTopicResponseVM Create(TimeTableTopicRequestVM request)
        {
            var model = request.ToModel();
            _dbContext.TimeTableTopics.Add(model);
            _dbContext.SaveChanges();
            return TimeTableTopicResponseVM.FromModel(model);
        }

        public TimeTableTopicResponseVM UpdateByIdAndTenantId(int id, int tenantId, TimeTableTopicUpdateVM request)
        {
            var entity = _dbContext.TimeTableTopics
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null)
                return null;

            entity.TopicId = request.TopicId;
            entity.TimeTableDetailId = request.TimeTableDetailId;
            entity.UpdatedBy = request.UpdatedBy;
            entity.UpdatedOn = DateTime.UtcNow;

            _dbContext.SaveChanges();
            return TimeTableTopicResponseVM.FromModel(entity);
        }

        public bool Delete(int id, int tenantId)
        {
            var entity = _dbContext.TimeTableTopics
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null)
                return false;

            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;
            _dbContext.SaveChanges();
            return true;
        }


        public TimeTableTopicsVM GetStructured(int tenantId)
        {
            var timeTableTopics = _dbContext.TimeTableTopics
                .Where(x => !x.IsDeleted && x.TenantId == tenantId)
                .Include(x => x.Topic)
                    .ThenInclude(t => t.Subject)
                        .ThenInclude(s => s.Course)
                .Include(x => x.TimeTableDetail)
                    .ThenInclude(ttd => ttd.Period)
                .Include(x => x.TimeTableDetail)
                    .ThenInclude(ttd => ttd.TimeTable)
                .ToList();

            var data = timeTableTopics.Select(x => new TDataTimeTableTopic
            {
                Id = x.Id,
                TopicId = x.TopicId,
                TimeTableDetailId = x.TimeTableDetailId,
                TopicName = x.Topic?.Name ?? "",
                SubjectName = x.Topic?.Subject?.Name ?? "",
                CourseName = x.Topic?.Subject?.Course?.Name ?? "",
                PeriodId = x.TimeTableDetail?.PeriodId,
                PeriodName = x.TimeTableDetail?.Period?.Name ?? "",
                TimeTableName = x.TimeTableDetail?.TimeTable?.Name ?? "",
                TimeTableDate = x.TimeTableDetail?.TimeTable?.Date
            }).ToList();

            return new TimeTableTopicsVM
            {
                TDataTimeTableTopics = data
                // Headers uses default dictionary in the VM class
            };
        }


        public TimeTableTopicDropdown GetDropdownMapped(int tenantId)
        {
            var courses = _dbContext.Courses
                .Where(c => !c.IsDeleted && c.TenantId == tenantId)
                .Include(c => c.Subjects)
                    .ThenInclude(s => s.Topics)
                .Include(c => c.Subjects)
                    .ThenInclude(s => s.TimeTableDetails)
                        .ThenInclude(ttd => ttd.Period)
                .Include(c => c.Subjects)
                    .ThenInclude(s => s.TimeTableDetails)
                        .ThenInclude(ttd => ttd.TimeTable)
                .ToList();

            var result = new TimeTableTopicDropdown { Courses = new List<CourseDropdownVM>() };

            foreach (var course in courses)
            {
                var courseVm = new CourseDropdownVM
                {
                    Id = course.Id,
                    Name = course.Name,
                    Subjects = new List<SubjectDropdownVM>()
                };

                foreach (var subject in course.Subjects.Where(s => !s.IsDeleted))
                {
                    var subjectVm = new SubjectDropdownVM
                    {
                        Id = subject.Id,
                        Name = subject.Name,
                        Topics = new List<TopicDropdownVM>(),
                        TimeTableDetails = new List<TimeTableDetailDropdownVM>()
                    };

                    foreach (var topic in subject.Topics.Where(t => !t.IsDeleted))
                    {
                        subjectVm.Topics.Add(new TopicDropdownVM
                        {
                            Id = topic.Id,
                            Name = topic.Name
                        });
                    }

                    if (subject.TimeTableDetails != null)
                    {
                        foreach (var ttd in subject.TimeTableDetails.Where(ttd => !ttd.IsDeleted))
                        {
                            subjectVm.TimeTableDetails.Add(new TimeTableDetailDropdownVM
                            {
                                Id = ttd.Id,
                                Name = (ttd.Period != null ? ttd.Period.Name : "") +
                                       (ttd.TimeTable != null ? " - " + ttd.TimeTable.Name : ""),
                                Date = ttd.TimeTable?.Date  // <-- ADDED HERE
                            });
                        }
                    }

                    courseVm.Subjects.Add(subjectVm);
                }

                result.Courses.Add(courseVm);
            }

            return result;
        }


        public List<TimeTableTopicByDetailResponseVM> GetTopicsByTimeTableDetailId(int tenantId, int timeTableDetailId)
        {
            var topics = _dbContext.TimeTableTopics
                .Where(x => x.TenantId == tenantId &&
                            x.TimeTableDetailId == timeTableDetailId &&
                            !x.IsDeleted)
                .Include(x => x.Topic)
                    .ThenInclude(t => t.Subject)
                .Include(x => x.Topic)
                    .ThenInclude(t => t.TopicType)
                .ToList();

            var dtos = new List<TimeTableTopicByDetailResponseVM>();

            foreach (var x in topics)
            {
                dtos.Add(new TimeTableTopicByDetailResponseVM
                {
                    Id = x.Id,
                    TopicId = x.TopicId,

                    TopicName = x.Topic?.Name ?? "",
                    SubjectName = x.Topic?.Subject?.Name ?? "",

                    TopicTypeId = x.Topic?.TopicTypeId,
                    TopicTypeName = x.Topic?.TopicType?.Name ?? "",

                    TimeTableDetailId = x.TimeTableDetailId ?? 0

                });
            }

            return dtos;
        }




    }
}
