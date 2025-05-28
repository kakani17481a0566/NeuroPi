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

        [HttpGet("/getByMasterTypeId/{masterTypeId}/{tenantId}")]
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

    }
}
