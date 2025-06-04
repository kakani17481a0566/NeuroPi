using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.TimeTableTopics;
using SchoolManagement.Response;
using System.Collections.Generic;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTableTopicsController : ControllerBase
    {
        private readonly ITimeTableTopicsService _service;

        public TimeTableTopicsController(ITimeTableTopicsService service)
        {
            _service = service;
        }

        [HttpGet]
        public ResponseResult<List<TimeTableTopicResponseVM>> GetAll()
        {
            var data = _service.GetAll();
            return new ResponseResult<List<TimeTableTopicResponseVM>>(HttpStatusCode.OK, data);
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<TimeTableTopicResponseVM>> GetAllByTenantId(int tenantId)
        {
            var data = _service.GetAllByTenantId(tenantId);
            return new ResponseResult<List<TimeTableTopicResponseVM>>(HttpStatusCode.OK, data);
        }

        [HttpGet("{id}")]
        public ResponseResult<TimeTableTopicResponseVM> GetById(int id)
        {
            var result = _service.GetById(id);
            if (result == null)
                return new ResponseResult<TimeTableTopicResponseVM>(HttpStatusCode.NotFound, null, "TimeTableTopic not found");

            return new ResponseResult<TimeTableTopicResponseVM>(HttpStatusCode.OK, result);
        }

        [HttpGet("{id}/tenant/{tenantId}")]
        public ResponseResult<TimeTableTopicResponseVM> GetByIdAndTenantId(int id, int tenantId)
        {
            var result = _service.GetByIdAndTenantId(id, tenantId);
            if (result == null)
                return new ResponseResult<TimeTableTopicResponseVM>(HttpStatusCode.NotFound, null, "TimeTableTopic not found");

            return new ResponseResult<TimeTableTopicResponseVM>(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public ResponseResult<TimeTableTopicResponseVM> Create(TimeTableTopicRequestVM request)
        {
            var created = _service.Create(request);
            return new ResponseResult<TimeTableTopicResponseVM>(HttpStatusCode.Created, created, "Created successfully");
        }

        [HttpPut("{id}/tenant/{tenantId}")]
        public ResponseResult<TimeTableTopicResponseVM> Update(int id, int tenantId, TimeTableTopicUpdateVM request)
        {
            var updated = _service.UpdateByIdAndTenantId(id, tenantId, request);
            if (updated == null)
                return new ResponseResult<TimeTableTopicResponseVM>(HttpStatusCode.NotFound, null, "TimeTableTopic not found");

            return new ResponseResult<TimeTableTopicResponseVM>(HttpStatusCode.OK, updated, "Updated successfully");
        }

        [HttpDelete("{id}/tenant/{tenantId}")]
        public ResponseResult<TimeTableTopicResponseVM> Delete(int id, int tenantId)
        {
            var deleted = _service.DeleteByIdAndTenantId(id, tenantId);
            if (deleted == null)
                return new ResponseResult<TimeTableTopicResponseVM>(HttpStatusCode.NotFound, null, "TimeTableTopic not found");

            return new ResponseResult<TimeTableTopicResponseVM>(HttpStatusCode.OK, deleted, "Deleted successfully");
        }
    }
}
