using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.WorkSheets;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkSheetController : ControllerBase
    {
        private readonly IWorkSheetService _workSheetService;
        public WorkSheetController(IWorkSheetService workSheetService)
        {
            _workSheetService = workSheetService;
        }

        [HttpGet]
        public ResponseResult<List<WorkSheetResponseVM>> GetAllWorkSheets()
        {
            var workSheets = _workSheetService.GetAllWorkSheets();
            return new ResponseResult<List<WorkSheetResponseVM>>(HttpStatusCode.OK, workSheets, "All worksheets retrieved successfully");
        }

        [HttpGet("{id}")]
        public ResponseResult<WorkSheetResponseVM> GetWorkSheetById(int id)
        {
            var workSheet = _workSheetService.GetWorkSheetsById(id);
            return workSheet == null
                ? new ResponseResult<WorkSheetResponseVM>(HttpStatusCode.NotFound, null, "Worksheet not found")
                : new ResponseResult<WorkSheetResponseVM>(HttpStatusCode.OK, workSheet, "Worksheet retrieved successfully");
        }

        [HttpGet("tenant/{tenantId}/{id}")]
        public ResponseResult<WorkSheetResponseVM> GetWorkSheetByTenantIdAndId(int tenantId, int id)
        {
            var workSheet = _workSheetService.GetWorkSheetByTenantIdAndId(tenantId, id);
            return workSheet == null
                ? new ResponseResult<WorkSheetResponseVM>(HttpStatusCode.NotFound, null, "Worksheet not found for the specified tenant")
                : new ResponseResult<WorkSheetResponseVM>(HttpStatusCode.OK, workSheet, "Worksheet retrieved successfully");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<WorkSheetResponseVM>> GetWorkSheetsByTenantId(int tenantId)
        {
            var workSheets = _workSheetService.GetAllWorkSheets()
                .Where(ws => ws.TenantId == tenantId)
                .ToList();
            return new ResponseResult<List<WorkSheetResponseVM>>(HttpStatusCode.OK, workSheets, "Worksheets retrieved successfully for the specified tenant");
        }

        [HttpPost]
        public ResponseResult<WorkSheetResponseVM> CreateWorkSheet(WorkSheetRequestVM workSheet)
        {
            var createdWorkSheet = _workSheetService.CreateWorkSheet(workSheet);
            return new ResponseResult<WorkSheetResponseVM>(HttpStatusCode.Created, createdWorkSheet, "Worksheet created successfully");
        }

        [HttpPut("{id}/{tenantId}")]
        public ResponseResult<WorkSheetResponseVM> UpdateWorkSheet(int id, int tenantId, [FromBody]WorkSheetUpdateVM workSheet)
        {
            var updatedWorkSheet = _workSheetService.UpdateWorkSheet(id, tenantId, workSheet);
            return updatedWorkSheet == null
                ? new ResponseResult<WorkSheetResponseVM>(HttpStatusCode.NotFound, null, "Worksheet not found for update")
                : new ResponseResult<WorkSheetResponseVM>(HttpStatusCode.OK, updatedWorkSheet, "Worksheet updated successfully");
        }

        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<bool> DeleteWorkSheet(int id, int tenantId)
        {
            var isDeleted = _workSheetService.DeleteWorkSheetByIdAndTenantId(id, tenantId);
            return isDeleted
                ? new ResponseResult<bool>(HttpStatusCode.OK, true, "Worksheet deleted successfully")
                : new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Worksheet not found for deletion");
        }
    }
}
