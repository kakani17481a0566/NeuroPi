using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.TimeTable;
using SchoolManagement.ViewModel.Topic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class TopicServiceImpl : ITopicService
    {
        private readonly SchoolManagementDb _dbContext;

        public TopicServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TopicResponseVM> GetAll()
        {
            return _dbContext.Topics
                .Where(t => !t.IsDeleted)
                .Select(TopicResponseVM.FromModel)
                .ToList();
        }

        public TopicResponseVM GetById(int id)
        {
            var topic = _dbContext.Topics
                .FirstOrDefault(t => t.Id == id && !t.IsDeleted);
            return TopicResponseVM.FromModel(topic);
        }

        public List<TopicResponseVM> GetAll(int tenantId)
        {
            return _dbContext.Topics
                .Where(t => !t.IsDeleted && t.TenantId == tenantId)
                .Select(TopicResponseVM.FromModel)
                .ToList();
        }

        public TopicResponseVM GetById(int id, int tenantId)
        {
            var topic = _dbContext.Topics
                .FirstOrDefault(t => t.Id == id && t.TenantId == tenantId && !t.IsDeleted);
            return TopicResponseVM.FromModel(topic);
        }

        public TopicResponseVM Create(TopicRequestVM request)
        {
            var topic = request.ToModel();
            _dbContext.Topics.Add(topic);
            _dbContext.SaveChanges();
            return TopicResponseVM.FromModel(topic);
        }

        public TopicResponseVM Update(int id, int tenantId, TopicUpdateVM request)
        {
            var topic = _dbContext.Topics
                .FirstOrDefault(t => t.Id == id && t.TenantId == tenantId && !t.IsDeleted);

            if (topic == null) return null;

            topic.Name = request.Name;
            topic.Code = request.Code;
            topic.Description = request.Description;
            topic.SubjectId = request.SubjectId;
            topic.TopicTypeId = request.TopicTypeId;
            topic.UpdatedOn = DateTime.UtcNow;
            topic.UpdatedBy = request.UpdatedBy;

            _dbContext.SaveChanges();
            return TopicResponseVM.FromModel(topic);
        }

        public bool Delete(int id, int tenantId)
        {
            var topic = _dbContext.Topics
                .FirstOrDefault(t => t.Id == id && t.TenantId == tenantId && !t.IsDeleted);

            if (topic == null) return false;

            topic.IsDeleted = true;
            topic.UpdatedOn = DateTime.UtcNow;
            _dbContext.SaveChanges();

            return true;
        }

        // ✅ New method: Resolved details

        public TopicFullResponseVM GetResolvedTopics(int tenantId)
        {
            var masters = _dbContext.Masters
                .Where(m => !m.IsDeleted)
                .ToDictionary(m => m.Id, m => m.Name);

            var topics = _dbContext.Topics
                .Where(t => !t.IsDeleted && t.TenantId == tenantId)
                .Include(t => t.Subject).ThenInclude(s => s.Course)
                .Include(t => t.Tenant)
                .ToList();

            var topicDetails = topics.Select(t => new TopicDetailVM
            {
                Id = t.Id,
                Name = t.Name,
                Code = t.Code,
                Description = t.Description,
                SubjectId = t.Subject?.Id,
                CourseId = t.Subject?.Course?.Id,
                SubjectName = t.Subject?.Name,
                CourseName = t.Subject?.Course?.Name,
                TopicTypeName = t.TopicTypeId.HasValue && masters.ContainsKey(t.TopicTypeId.Value)
                    ? masters[t.TopicTypeId.Value]
                    : null,
                TenantName = t.Tenant?.Name
            }).ToList();

            var courseDict = topics
                .Where(t => t.Subject?.Course != null)
                .Select(t => t.Subject.Course)
                .Distinct()
                .ToDictionary(c => c.Id, c => c.Name);

            var subjectDict = topics
                .Where(t => t.Subject != null)
                .Select(t => t.Subject)
                .Distinct()
                .ToDictionary(s => s.Id, s => s.Name);

            var subjectCourseMap = topics
                .Where(t => t.Subject != null && t.Subject.Course != null)
                .Select(t => new { SubjectId = t.Subject.Id, CourseId = t.Subject.Course.Id })
                .Distinct()
                .ToDictionary(x => x.SubjectId, x => x.CourseId);

            return new TopicFullResponseVM
            {
                Headers = new List<string>
        {
            "Id", "Name", "Code", "Description", "SubjectName", "CourseName", "TopicTypeName", "TenantName"
        },
                TDataTopic = topicDetails,
                Courses = courseDict,
                Subjects = subjectDict,
                SubjectCourseMap = subjectCourseMap // ✅ Include subject → course mapping
            };
        }


        public TimeTableDropDown GetTimeTableDropDown(int tenantId)
        {
            var courses = _dbContext.Courses
                .Include(c => c.Subjects)
                .Where(c => c.TenantId == tenantId && !c.IsDeleted)
                .ToList();

            // ✅ Updated type to TopicTypeOptionVM
            var topicTypes = _dbContext.Masters
                .Where(m => !m.IsDeleted && m.TenantId == tenantId && m.MasterTypeId == 37)
                .Select(m => new TopicTypeOptionVM
                {
                    Id = m.Id,
                    Name = m.Name
                })
                .ToList();

            var vm = new TimeTableDropDown
            {
                Courses = courses.ToDictionary(
                    c => c.Id,
                    c => new CourseInfo
                    {
                        Name = c.Name,
                        Subjects = c.Subjects
                            .Where(s => !s.IsDeleted)
                            .ToDictionary(s => s.Id, s => s.Name)
                    }
                ),
                TopicTypes = topicTypes
            };

            return vm;
        }


    }
}
