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
        private readonly SchoolManagementDb _db;

        public TimeTableTopicsServiceImpl(SchoolManagementDb db)
        {
            _db = db;
        }

        public TimeTableTopicResponseVM Create(TimeTableTopicRequestVM request)
        {
            var entity = new MTimeTableTopic
            {
                SubjectId = request.SubjectId,
                TimeTableId = request.TimeTableId,
                TopicId = request.TopicId,
                TenantId = request.TenantId,
                CreatedBy = request.CreatedBy,
                CreatedOn = DateTime.UtcNow,
            };

            _db.TimeTableTopics.Add(entity);
            _db.SaveChanges();

            return MapToResponse(entity);
        }

        public List<TimeTableTopicResponseVM> GetAll()
        {
            return _db.TimeTableTopics
                .Where(x => !x.IsDeleted)
                .Select(MapToResponse)
                .ToList();
        }

        public List<TimeTableTopicResponseVM> GetAllByTenantId(int tenantId)
        {
            return _db.TimeTableTopics
                .Where(x => x.TenantId == tenantId && !x.IsDeleted)
                .Select(MapToResponse)
                .ToList();
        }

        public TimeTableTopicResponseVM GetById(int id)
        {
            var entity = _db.TimeTableTopics.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            return entity == null ? null : MapToResponse(entity);
        }

        public TimeTableTopicResponseVM GetByIdAndTenantId(int id, int tenantId)
        {
            var entity = _db.TimeTableTopics.FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            return entity == null ? null : MapToResponse(entity);
        }

        public TimeTableTopicResponseVM UpdateByIdAndTenantId(int id, int tenantId, TimeTableTopicUpdateVM request)
        {
            var entity = _db.TimeTableTopics.FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            if (entity == null) return null;

            entity.SubjectId = request.SubjectId;
            entity.TimeTableId = request.TimeTableId;
            entity.TopicId = request.TopicId;
            entity.UpdatedBy = request.UpdatedBy;
            entity.UpdatedOn = DateTime.UtcNow;

            _db.SaveChanges();
            return MapToResponse(entity);
        }

        public TimeTableTopicResponseVM DeleteByIdAndTenantId(int id, int tenantId)
        {
            var entity = _db.TimeTableTopics.FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            if (entity == null) return null;

            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;
            _db.SaveChanges();

            return MapToResponse(entity);
        }

        private TimeTableTopicResponseVM MapToResponse(MTimeTableTopic model)
        {
            return new TimeTableTopicResponseVM
            {
                Id = model.Id,
                SubjectId = model.SubjectId,
                TimeTableId = model.TimeTableId,
                TopicId = model.TopicId,
                TenantId = model.TenantId
            };
        }
    }
}
