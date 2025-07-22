using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.TimeTable;
using SchoolManagement.ViewModel.Topic;
using System.Collections.Generic;
using System.Net;

// Developed by Mohith

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _service;

        public TopicController(ITopicService service)
        {
            _service = service;
        }


        // GET: api/topic
        // Get all topics without tenant filter
        [HttpGet]
        public ResponseResult<List<TopicResponseVM>> GetAll()
        {
            var result = _service.GetAll();
            return new ResponseResult<List<TopicResponseVM>>(HttpStatusCode.OK, result, "All topics fetched successfully");
        }

        // GET: api/topic/{id}
        // Get topic by ID only (no tenant)
        [HttpGet("{id}")]
        public ResponseResult<TopicResponseVM> GetById(int id)
        {
            var result = _service.GetById(id);
            if (result == null)
                return new ResponseResult<TopicResponseVM>(HttpStatusCode.NotFound, null, "Topic not found");

            return new ResponseResult<TopicResponseVM>(HttpStatusCode.OK, result, "Topic fetched successfully");
        }

        // Get all topics by tenant
        // GET: api/topic/tenant/{tenantId}
        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<TopicResponseVM>> GetAll(int tenantId)
        {
            var result = _service.GetAll(tenantId);
            return new ResponseResult<List<TopicResponseVM>>(HttpStatusCode.OK, result, "Topics fetched successfully");
        }

        // Get topic by ID and tenant
        // GET: api/topic/{id}/tenant/{tenantId}
        [HttpGet("{id}/tenant/{tenantId}")]
        public ResponseResult<TopicResponseVM> GetById(int id, int tenantId)
        {
            var result = _service.GetById(id, tenantId);
            if (result == null)
                return new ResponseResult<TopicResponseVM>(HttpStatusCode.NotFound, null, "Topic not found");

            return new ResponseResult<TopicResponseVM>(HttpStatusCode.OK, result, "Topic fetched successfully");
        }

        // Create a new topic
        // POST: api/topic
        [HttpPost]
        public ResponseResult<TopicResponseVM> Create([FromBody] TopicRequestVM request)
        {
            if (request == null)
                return new ResponseResult<TopicResponseVM>(HttpStatusCode.BadRequest, null, "Invalid topic data");

            var result = _service.Create(request);
            return new ResponseResult<TopicResponseVM>(HttpStatusCode.Created, result, "Topic created successfully");
        }

        // Update topic
        // PUT: api/topic/{id}/tenant/{tenantId}
        [HttpPut("{id}/tenant/{tenantId}")]
        public ResponseResult<TopicResponseVM> Update(int id, int tenantId, [FromBody] TopicUpdateVM request)
        {
            if (request == null)
                return new ResponseResult<TopicResponseVM>(HttpStatusCode.BadRequest, null, "Invalid topic data");

            var result = _service.Update(id, tenantId, request);
            if (result == null)
                return new ResponseResult<TopicResponseVM>(HttpStatusCode.NotFound, null, "Topic not found");

            return new ResponseResult<TopicResponseVM>(HttpStatusCode.OK, result, "Topic updated successfully");
        }

        // Delete topic (soft delete)
        // DELETE: api/topic/{id}/tenant/{tenantId}
        [HttpDelete("{id}/tenant/{tenantId}")]
        public ResponseResult<string> Delete(int id, int tenantId)
        {
            var success = _service.Delete(id, tenantId);
            if (!success)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Topic not found");

            return new ResponseResult<string>(HttpStatusCode.OK, "Deleted", "Topic deleted successfully");
        }

        [HttpGet("Topic-full-data/{tenantId}")]
        public ResponseResult<TopicFullResponseVM> GetResolvedTopics(int tenantId)
        {
            var result = _service.GetResolvedTopics(tenantId);

            if (result == null || result.TDataTopic.Count == 0)
            {
                return new ResponseResult<TopicFullResponseVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "No resolved topics found for the given tenant"
                );
            }

            return new ResponseResult<TopicFullResponseVM>(
                HttpStatusCode.OK,
                result,
                "Resolved topics fetched successfully"
            );
        }

        [HttpGet("timetable-dropdown/{tenantId}")]
        public ResponseResult<TimeTableDropDown> GetTimeTableDropDown(int tenantId)
        {
            var result = _service.GetTimeTableDropDown(tenantId);
            if (result == null)
            {
                return new ResponseResult<TimeTableDropDown>(HttpStatusCode.NotFound, null, "No courses or subjects found");
            }
            return new ResponseResult<TimeTableDropDown>(HttpStatusCode.OK, result, "Timetable dropdown data fetched successfully");
        }

    }
}
