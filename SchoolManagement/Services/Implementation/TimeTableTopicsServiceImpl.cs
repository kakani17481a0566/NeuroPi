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
    }
}
