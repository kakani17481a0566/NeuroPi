using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.TimeTableTopics;
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
            var result = _service.Delete(id, tenantId);
            if (!result)
                return new ResponseResult<TimeTableTopicResponseVM>(HttpStatusCode.NotFound, null, "TimeTableTopic not found");

            return new ResponseResult<TimeTableTopicResponseVM>(HttpStatusCode.OK, null, "Deleted successfully");
        }

        [Authorize]

        [HttpGet("structured/{tenantId}")]
        public ResponseResult<TimeTableTopicsVM> GetStructured(int tenantId)
        {
            var data = _service.GetStructured(tenantId);
            return new ResponseResult<TimeTableTopicsVM>(HttpStatusCode.OK, data, "Structured data loaded");
        }

        [HttpGet("dropdown-mapped/{tenantId}")]
        public ResponseResult<TimeTableTopicDropdown> GetDropdownMapped(int tenantId)
        {
            var data = _service.GetDropdownMapped(tenantId);
            return new ResponseResult<TimeTableTopicDropdown>(HttpStatusCode.OK, data, "Mapped dropdowns fetched successfully");
        }

        [HttpGet("TimetableDetailTopicsController/{tenantId}/detail/{timeTableDetailId}")]
        public ResponseResult<List<TimeTableTopicByDetailResponseVM>>
    GetTopicsByDetail(int tenantId, int timeTableDetailId)
        {
            var data = _service.GetTopicsByTimeTableDetailId(tenantId, timeTableDetailId);

            if (data == null || data.Count == 0)
                return new ResponseResult<List<TimeTableTopicByDetailResponseVM>>
                    (HttpStatusCode.NotFound, null, "No topics found");

            return new ResponseResult<List<TimeTableTopicByDetailResponseVM>>
                (HttpStatusCode.OK, data, "Topics fetched successfully");
        }


    }
}
