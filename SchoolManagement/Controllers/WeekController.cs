using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Week;
using SchoolManagement.Response;   // <-- Your ResponseResult<T>
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeekController : ControllerBase
    {
        private readonly IWeekService _weekService;

        public WeekController(IWeekService weekService)
        {
            _weekService = weekService;
        }

        // GET: api/week/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllWeeks()
        {
            var weeks = _weekService.GetAllWeeks();
            var response = new ResponseResult<List<WeekVm>>(HttpStatusCode.OK, weeks);
            return await Task.FromResult(response);
        }


        // GET: api/week/tenant/{tenantId}
        [HttpGet("tenant/{tenantId}")]
        public async Task<IActionResult> GetWeeksByTenantId(int tenantId)
        {
            var weeks = _weekService.GetWeeksByTenantId(tenantId);
            var response = new ResponseResult<List<WeekVm>>(HttpStatusCode.OK, weeks);
            return await Task.FromResult(response);
        }

        // GET: api/week/{id}/tenant/{tenantId}
        [HttpGet("{id}/tenant/{tenantId}")]
        public async Task<IActionResult> GetWeekByIdAndTenantId(int id, int tenantId)
        {
            var week = _weekService.GetWeekByIdAndTenantId(id, tenantId);
            if (week == null)
            {
                return await Task.FromResult(new ResponseResult<WeekVm>(HttpStatusCode.NotFound, null, "Week not found"));
            }
            return await Task.FromResult(new ResponseResult<WeekVm>(HttpStatusCode.OK, week));
        }

        // GET: api/week/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWeekById(int id)
        {
            var week = _weekService.GetWeekById(id);
            if (week == null)
            {
                return await Task.FromResult(new ResponseResult<WeekVm>(HttpStatusCode.NotFound, null, "Week not found"));
            }
            return await Task.FromResult(new ResponseResult<WeekVm>(HttpStatusCode.OK, week));
        }

        // POST: api/week
        [HttpPost]
        public async Task<IActionResult> CreateWeek([FromBody] WeekCreateVm request)
        {
            var createdWeek = _weekService.CreateWeek(request);
            var response = new ResponseResult<WeekVm>(HttpStatusCode.Created, createdWeek, "Week created successfully");
            return await Task.FromResult(response);
        }

        // PUT: api/week/{id}/tenant/{tenantId}
        [HttpPut("{id}/tenant/{tenantId}")]
        public async Task<IActionResult> UpdateWeek(int id, int tenantId, [FromBody] WeekUpdateVm request)
        {
            var updatedWeek = _weekService.UpdateWeek(id, tenantId, request);
            if (updatedWeek == null)
            {
                return await Task.FromResult(new ResponseResult<WeekVm>(HttpStatusCode.NotFound, null, "Week not found"));
            }
            return await Task.FromResult(new ResponseResult<WeekVm>(HttpStatusCode.OK, updatedWeek, "Week updated successfully"));
        }

        // DELETE: api/week/{id}/tenant/{tenantId}
        [HttpDelete("{id}/tenant/{tenantId}")]
        public async Task<IActionResult> DeleteWeek(int id, int tenantId)
        {
            var success = _weekService.DeleteWeek(id, tenantId);
            if (!success)
            {
                return await Task.FromResult(new ResponseResult<string>(HttpStatusCode.NotFound, null, "Week not found"));
            }
            return await Task.FromResult(new ResponseResult<string>(HttpStatusCode.NoContent, null, "Week deleted successfully"));
        }

        // GET: api/week/tenant/{tenantId}/term/{termId}
        [HttpGet("week/tenant/term/{tenantId}/term/{termId}")]
        public async Task<IActionResult> GetWeeksByTenantIdAndTermId(int tenantId, int termId)
        {
            var weeks = _weekService.GetWeeksByTenantIdAndTermId(tenantId, termId);
            var response = new ResponseResult<List<WeekVm>>(HttpStatusCode.OK, weeks);
            return await Task.FromResult(response);
        }

    }
}
