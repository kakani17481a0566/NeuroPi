using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.CountingTestContent;
using System.Net;
using System.Collections.Generic;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountingTestContentController : ControllerBase
    {
        private readonly ICountingTestContentService _service;

        public CountingTestContentController(ICountingTestContentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves all counting test content records for a given test.
        /// </summary>
        /// <param name="testId">The ID of the parent test.</param>
        [HttpGet("test/{testId}")]
        public IActionResult GetByTestId(int testId)
        {
            var contents = _service.GetByTestId(testId);

            return new ResponseResult<List<CountingTestContentRespounceVM>>(
                HttpStatusCode.OK,
                contents,
                contents.Count > 0
                    ? "Counting test content retrieved successfully"
                    : "No content found for the given test"
            );
        }

        /// <summary>
        /// Retrieves a specific counting test content record by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the counting test content.</param>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var content = _service.GetById(id);

            if (content == null)
            {
                return new ResponseResult<CountingTestContentRespounceVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "Counting test content not found"
                );
            }

            return new ResponseResult<CountingTestContentRespounceVM>(
                HttpStatusCode.OK,
                content,
                "Counting test content retrieved successfully"
            );
        }

        [HttpPost]
        public ResponseResult<CountingTestContentRespounceVM> Create(CountingTestContentRequestVM model)
        {
            var createdContent = _service.Create(model);
            if (createdContent == null)
            {
                return new ResponseResult<CountingTestContentRespounceVM>(
                    HttpStatusCode.BadRequest,
                    null,
                    "Failed to create counting test content"
                );
            }
            return new ResponseResult<CountingTestContentRespounceVM>(
                HttpStatusCode.OK,
                createdContent,
                "Counting test content created successfully"
            );
        }
    }
}
