using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.MasterType;
using System.Net;
//sai vardhan
namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterTypeController : ControllerBase
    {
        private readonly IMasterTypeService masterTypeService;
        public MasterTypeController(IMasterTypeService masterTypeService)
        {
            this.masterTypeService = masterTypeService;
            
        }

        [HttpGet]
        public ResponseResult<List<MasterTypeResponseVM>> GetAllMasterTypes()
        {
            var response = masterTypeService.GetAll();
            if (response == null)
            {
                return new ResponseResult<List<MasterTypeResponseVM>>(HttpStatusCode.NotFound,response,"No data Found");
            }
            return new ResponseResult<List<MasterTypeResponseVM>>(HttpStatusCode.OK, response, "Master types fetched successfully");
        }
        [HttpGet("{id}")]
        public ResponseResult<MasterTypeResponseVM> GetById([FromRoute] int id)
        {
            var response= masterTypeService.GetById(id);
            if (response != null)
            {
                return new ResponseResult<MasterTypeResponseVM>(HttpStatusCode.OK, response, "master type is fetched successfully");   
            }
            return new ResponseResult<MasterTypeResponseVM>(HttpStatusCode.BadGateway, response, $" MasterType not found with id {id}");
        }

        [HttpGet("/tenantId-id")]
        public ResponseResult<MasterTypeResponseVM> GetByIdAndTenantId([FromQuery(Name ="id")] int id, [FromQuery(Name ="tenantId")] int tenantId)
        {
            var response = masterTypeService.GetByIdAndTenantId(id,tenantId);
            if (response != null)
            {
                return new ResponseResult<MasterTypeResponseVM>(HttpStatusCode.OK, response, "master type is fetched successfully");
            }
            return new ResponseResult<MasterTypeResponseVM>(HttpStatusCode.NotFound, response, $" master type not found with id {id} and tenantid {tenantId}");
        }

        [HttpGet("/tenant/{id}")]
        public ResponseResult<List<MasterTypeResponseVM>> GetAllMasterTypes([FromRoute]int id)
        {
            var response = masterTypeService.GetAllByTenantId(id);

            if (response == null)
            {
                return new ResponseResult<List<MasterTypeResponseVM>>(HttpStatusCode.NotFound, response, "No data Found");
            }
            return new ResponseResult<List<MasterTypeResponseVM>>(HttpStatusCode.OK, response, "Master types fetched successfully");
        }
        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<MasterTypeResponseVM> DeleteByIdAndTenantId([FromRoute]int id, [FromRoute] int tenantId)
        {
            var response = masterTypeService.DeleteById(id, tenantId);
            if (response != null)
            {
                return new ResponseResult<MasterTypeResponseVM>(HttpStatusCode.OK, response, "Deleted Successfully");
            }
            return new ResponseResult<MasterTypeResponseVM>(HttpStatusCode.NoContent, response, $"No Data found with Id {id}");

        }
        [HttpPost]
        public ResponseResult<MasterTypeResponseVM> AddMasterType([FromBody]MasterTypeRequestVM request)
        {
            var response = masterTypeService.CreateMasterType(request);
            if (response != null)
            {
                return new ResponseResult<MasterTypeResponseVM>(HttpStatusCode.OK, response, "master type created  successfully");
            }
            return new ResponseResult<MasterTypeResponseVM>(HttpStatusCode.BadRequest, response, "master type not created");
        }
        [HttpPut("/{id}/tenant/{tenantId}")]
        public ResponseResult<MasterTypeResponseVM> UpdateMasterType( int id,int tenantId,[FromBody] MasterTypeUpdateVM request)
        {
            var response = masterTypeService.UpdateMasterType(id,tenantId,request);
            if (response != null)
            {
                return new ResponseResult<MasterTypeResponseVM>(HttpStatusCode.OK, response, "master type updated successfully");
            }
            return new ResponseResult<MasterTypeResponseVM>(HttpStatusCode.BadRequest, response, "master type not updated");
        }



    }
}
