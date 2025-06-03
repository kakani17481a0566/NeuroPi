using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Topic;
using System.Collections.Generic;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet("{tenantId}")]
        public ResponseResult<List<TopicResponseVM>> GetAll(int tenantId)
        {
            var topics = _topicService.GetAll(tenantId);
            return new ResponseResult<List<TopicResponseVM>>(HttpStatusCode.OK, topics, "Topics fetched successfully");
        }

        [HttpGet("{tenantId}/{id}")]
        public ResponseResult<TopicResponseVM> GetById(int tenantId, int id)
        {
            var topic = _topicService.GetById(id, tenantId);
            if (topic == null)
                return new ResponseResult<TopicResponseVM>(HttpStatusCode.NotFound, null, "Topic not found");

            return new ResponseResult<TopicResponseVM>(HttpStatusCode.OK, topic, "Topic fetched successfully");
        }

        [HttpPost]
        public ResponseResult<TopicResponseVM> Create([FromBody] TopicRequestVM request)
        {
            var created = _topicService.Create(request);
            return new ResponseResult<TopicResponseVM>(HttpStatusCode.Created, created, "Topic created successfully");
        }

        [HttpPut("{tenantId}/{id}")]
        public ResponseResult<TopicResponseVM> Update(int tenantId, int id, [FromBody] TopicUpdateVM request)
        {
            var updated = _topicService.Update(id, tenantId, request);
            if (updated == null)
                return new ResponseResult<TopicResponseVM>(HttpStatusCode.NotFound, null, "Topic not found or not updated");

            return new ResponseResult<TopicResponseVM>(HttpStatusCode.OK, updated, "Topic updated successfully");
        }

        [HttpDelete("{tenantId}/{id}")]
        public ResponseResult<string> Delete(int tenantId, int id)
        {
            var deleted = _topicService.Delete(id, tenantId);
            if (!deleted)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Topic not found or already deleted");

            return new ResponseResult<string>(HttpStatusCode.OK, "Deleted", "Topic deleted successfully");
        }
    }
}
