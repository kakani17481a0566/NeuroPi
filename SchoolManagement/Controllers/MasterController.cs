using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using NeuroPi.UserManagment.Response;
using SchoolManagement.ViewModel;
using SchoolManagement.ViewModel.Master;
using System.ComponentModel;



namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IMasterService masterService;
        private readonly IUtilitesService _utilitesService;

        public MasterController(IMasterService _masterService, IUtilitesService utilitesService)
        {
            masterService = _masterService;
            _utilitesService = utilitesService;
            
        }
        // Get all master types
        // GET: api/master
        // Developed by: Vardhan
        [HttpGet]
        public ResponseResult<List<MasterResponseVM>> GetAllMasterTypes()
        {
            var response = masterService.GetAll();
            if (response == null)
            {
                return new ResponseResult<List<MasterResponseVM>>(HttpStatusCode.NotFound, response, "No data Found");
            }
            return new ResponseResult<List<MasterResponseVM>>(HttpStatusCode.OK, response, "Master types fetched successfully");
        }
        // Get master type by ID
        // GET: api/master/{id}
        // Developed by: Vardhan
        [HttpGet("{id}")]
        public ResponseResult<MasterResponseVM> GetById([FromRoute] int id)
        {
            var response = masterService.GetById(id);
            if (response != null)
            {
                return new ResponseResult<MasterResponseVM>(HttpStatusCode.OK, response, "master type is fetched successfully");
            }
            return new ResponseResult<MasterResponseVM>(HttpStatusCode.BadGateway, response, $" MasterType not found with id {id}");
        }

        // Get master type by ID and Tenant ID
        // GET: api/master/tenantId?id={id}&tenantId={tenantId}
        // Developed by: Vardhan
        [HttpGet("master/tenantId")]
        public ResponseResult<MasterResponseVM> GetByIdAndTenantId([FromQuery(Name = "id")] int id, [FromQuery(Name = "tenantId")] int tenantId)
        {
            var response = masterService.GetByIdAndTenantId(id, tenantId);
            if (response != null)
            {
                return new ResponseResult<MasterResponseVM>(HttpStatusCode.OK, response, "master type is fetched successfully");
            }
            return new ResponseResult<MasterResponseVM>(HttpStatusCode.NotFound, response, $" master type not found with id {id} and tenantid {tenantId}");
        }

        // Get all master types by Tenant ID
        // GET: api/master/master/tenant/{id}
        // Developed by: Vardhan
        [HttpGet("master/tenant/{id}")]
        public ResponseResult<List<MasterResponseVM>> GetAllMasters([FromRoute] int id)
        {
            var response = masterService.GetAllByTenantId(id);

            if (response == null)
            {
                return new ResponseResult<List<MasterResponseVM>>(HttpStatusCode.NotFound, response, "No data Found");
            }
            return new ResponseResult<List<MasterResponseVM>>(HttpStatusCode.OK, response, "Master types fetched successfully");
        }
        // Delete master type by ID and Tenant ID
        // DELETE: api/master/{id}/{tenantId}
        // Developed by: Vardhan
        [HttpDelete("/master/{id}/{tenantId}")]
        public ResponseResult<MasterResponseVM> DeleteByIdAndTenantId([FromRoute] int id, [FromRoute] int tenantId)
        {
            var response = masterService.DeleteById(id, tenantId);
            if (response != null)
            {
                return new ResponseResult<MasterResponseVM>(HttpStatusCode.OK, response, "Deleted Successfully");
            }
            return new ResponseResult<MasterResponseVM>(HttpStatusCode.NoContent, response, $"No Data found with Id {id}");

        }
        // Add a new master type
        // POST: api/master
        // Developed by: Vardhan
        [HttpPost]
        public ResponseResult<MasterResponseVM> AddMaster([FromBody] MasterRequestVM request)
        {
            var response = masterService.CreateMasterType(request);
            if (response != null)
            {
                return new ResponseResult<MasterResponseVM>(HttpStatusCode.OK, response, "master type created  successfully");
            }
            return new ResponseResult<MasterResponseVM>(HttpStatusCode.BadRequest, response, "master type not created");
        }
        // Update master type by ID and Tenant ID
        // PUT: api/master/{id}/tenant/{tenantId}
        // Developed by: Vardhan    
        [HttpPut("/master/{id}/tenant/{tenantId}")]
        public ResponseResult<MasterResponseVM> UpdateMaster(int id, int tenantId, [FromBody] MasterUpdateVM request)
        {
            var response = masterService.UpdateMasterType(id, tenantId, request);
            if (response != null)
            {
                return new ResponseResult<MasterResponseVM>(HttpStatusCode.OK, response, "master type updated successfully");
            }
            return new ResponseResult<MasterResponseVM>(HttpStatusCode.BadRequest, response, "master type not updated");
        }

        // Get all master types by MasterTypeId and TenantId
        // GET: api/master/getByMasterTypeId/{masterTypeId}/{tenantId}
        // Developed by: Vardhan
        [HttpGet("/getByMasterTypeId/{masterTypeId}/{tenantId}")]
        [HttpGet("/api/getByMasterTypeId/{masterTypeId}/{tenantId}")]
        public ResponseResult<List<Object>> GetAllMastersByMasterTypeId([FromRoute] int masterTypeId, [FromRoute] int tenantId, bool isUtilites=false)
        {
            List<object> response;
            if (isUtilites)
            {
                var utilitesList = _utilitesService.GetAll(tenantId); 
                response = utilitesList?.Cast<object>().ToList() ?? new List<object>();
            }
            else
            {

                var masterList = masterService.GetAllByMasterTypeId(masterTypeId, tenantId); 
                response = masterList?.Cast<object>().ToList() ?? new List<object>();
            }
            if (response == null)
            {
                return new ResponseResult<List<Object>>(HttpStatusCode.NotFound, response, "No data Found");
            }
            return new ResponseResult<List<Object>>(HttpStatusCode.OK, response, "Master types fetched successfully");
        }

        // GET: api/master/by-type/{masterTypeId}/tenant/{tenantId}
      

    }
}
