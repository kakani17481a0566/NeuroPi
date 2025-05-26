using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Institutions;
using System;
using System.Collections.Generic;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitutionService _service;

        public InstitutionController(IInstitutionService service)
        {
            _service = service;
        }

        // Get all institutions
        [HttpGet]
        public List<InstitutionResponseVM> GetAll() => _service.GetAll();

        // Get a single institution by ID
        [HttpGet("{id}")]
        public InstitutionResponseVM GetById(int id) => _service.GetById(id);

        // Create a new institution
        [HttpPost]
        public InstitutionResponseVM Create([FromBody] InstitutionCreateRequestVM request) => _service.Create(request);

        // Update an institution using its ID and tenant ID
        [HttpPut("{id}/tenant/{tenantId}")]
        public InstitutionResponseVM UpdateByIdAndTenantId(int id, int tenantId, [FromBody] InstitutionUpdateRequestVM request)
            => _service.UpdateByIdAndTenantId(id, tenantId, request);

        // Delete an institution by ID and tenant ID, with optional contact deletion
        [HttpDelete("{id}/tenant/{tenantId}")]
        public async Task<IActionResult> DeleteByIdAndTenantId(int id, int tenantId, [FromQuery] bool deleteContact = false)
        {
            var success = _service.DeleteByIdAndTenantId(id, tenantId, deleteContact);
            if (!success)
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, $"Institution with id {id} and tenant {tenantId} not found");

            var message = deleteContact
                ? "Institution and related contact deleted successfully"
                : "Institution deleted successfully";

            return new ResponseResult<bool>(HttpStatusCode.OK, true, message);
        }

        // Get an institution by ID and tenant ID, including contact info
        [HttpGet("{id}/tenant/{tenantId}")]
        public IActionResult GetByIdAndTenantId(int id, int tenantId)
        {
            var result = _service.GetByIdAndTenantId(id, tenantId);
            if (result == null)
                return NotFound($"Institution with id {id} and tenant {tenantId} not found.");

            return Ok(result);
        }

        // Create an institution along with its contact information
        [HttpPost("with-contact")]
        public IActionResult CreateWithContact([FromBody] InstitutionWithContactRequestVM request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var response = _service.CreateWithContact(request);
                return CreatedAtAction(nameof(GetById), new { id = response.Institution.Id }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
