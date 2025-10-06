using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.FeeStructure;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeStructureController : ControllerBase
    {
        private readonly IFeeStructure _service;

        public FeeStructureController(IFeeStructure service)
        {
            _service = service;
        }

        // Create Fee Structure
        [HttpPost("create", Name = "CreateFeeStructure")]
        public IActionResult Create([FromBody] FeeStructureRequestVM vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return _service.CreateFeeStructure(vm);
        }

        // Get Fee Structure by Id + Tenant
        [HttpGet("{id}/{tenantId}", Name = "GetFeeStructureById")]
        public IActionResult GetById(int id, int tenantId)
        {
            return _service.GetFeeStructureById(id, tenantId);
        }

        // Get All Fee Structures for a Tenant
        [HttpGet("tenant/{tenantId}", Name = "GetAllFeeStructures")]
        public IActionResult GetAll(int tenantId)
        {
            return _service.GetAllFeeStructures(tenantId);
        }

        // Update Fee Structure
        [HttpPut("{id}", Name = "UpdateFeeStructure")]
        public IActionResult Update(int id, [FromBody] FeeStructureRequestVM vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return _service.UpdateFeeStructure(id, vm);
        }

        // Soft Delete Fee Structure
        [HttpDelete("{id}/{tenantId}", Name = "DeleteFeeStructure")]
        public IActionResult Delete(int id, int tenantId)
        {
            return _service.DeleteFeeStructure(id, tenantId);
        }
    }
}
