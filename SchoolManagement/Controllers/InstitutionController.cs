using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Institutions;
using System;
using System.Collections.Generic;

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

        [HttpGet]
        public List<InstitutionResponseVM> GetAll() => _service.GetAll();

        [HttpGet("{id}")]
        public InstitutionResponseVM GetById(int id) => _service.GetById(id);

        [HttpPost]
        public InstitutionResponseVM Create([FromBody] InstitutionCreateRequestVM request) => _service.Create(request);

        [HttpPut("{id}/tenant/{tenantId}")]
        public InstitutionResponseVM UpdateByIdAndTenantId(int id, int tenantId, [FromBody] InstitutionUpdateRequestVM request)
            => _service.UpdateByIdAndTenantId(id, tenantId, request);

        [HttpDelete("{id}")]
        public bool Delete(int id) => _service.Delete(id);

        [HttpGet("{id}/tenant/{tenantId}")]
        public InstitutionResponseVM GetByIdAndTenantId(int id, int tenantId)
            => _service.GetByIdAndTenantId(id, tenantId);

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
