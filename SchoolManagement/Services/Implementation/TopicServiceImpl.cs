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
    }
}
