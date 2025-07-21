using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
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
        public List<TopicDetailVM> GetResolvedTopics(int tenantId)
        {
            var masters = _dbContext.Masters.ToDictionary(m => m.Id, m => m.Name);

            var topics = _dbContext.Topics
                .Where(t => !t.IsDeleted && t.TenantId == tenantId)
                .Include(t => t.Subject)
                    .ThenInclude(s => s.Course) // ✅ include Course
                .Include(t => t.Tenant)
                .ToList()
                .Select(t => new TopicDetailVM
                {
                    Id = t.Id,
                    Name = t.Name,
                    Code = t.Code,
                    Description = t.Description,
                    SubjectName = t.Subject?.Name,
                    CourseName = t.Subject?.Course?.Name, // ✅ get course name via subject
                    TopicTypeName = t.TopicTypeId.HasValue && masters.ContainsKey(t.TopicTypeId.Value)
                        ? masters[t.TopicTypeId.Value]
                        : null,
                    TenantName = t.Tenant?.Name
                })
                .ToList();

            return topics;
        }


    }
}
