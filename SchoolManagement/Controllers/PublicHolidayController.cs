using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Master;
using SchoolManagement.ViewModel.PublicHoliday;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicHolidayController : ControllerBase
    {
        private readonly IPublicHolidayService publicHolidayService;
        public PublicHolidayController(IPublicHolidayService publicHolidayService)
        {
            this.publicHolidayService = publicHolidayService;
        }

        [HttpGet]
        public ResponseResult<List<PublicHolidayResponseVM>> GetAllMasterTypes()
        {
            var response = publicHolidayService.GetAll();
            if (response == null)
            {
                return new ResponseResult<List<PublicHolidayResponseVM>>(HttpStatusCode.NotFound, response, "No data Found");
            }
            return new ResponseResult<List<PublicHolidayResponseVM>>(HttpStatusCode.OK, response, "Master types fetched successfully");
        }

        [HttpGet("/holiday/{tenantId}")]
        public ResponseResult<List<PublicHolidayResponseVM>> GetAllMasterTypesByTenantId([FromRoute] int tenantId)
        {
            var response = publicHolidayService.GetAllByTenantId(tenantId);

            if (response == null)
            {
                return new ResponseResult<List<PublicHolidayResponseVM>>(HttpStatusCode.NotFound, response, "No data Found");
            }
            return new ResponseResult<List<PublicHolidayResponseVM>>(HttpStatusCode.OK, response, "Master types fetched successfully");
        }


        [HttpPost]
        public ResponseResult<PublicHolidayResponseVM> AddMaster([FromBody] PublicHolidayRequestVM request)
        {
            var response = publicHolidayService.CreatePublicHoliday(request);
            if (response != null)
            {
                return new ResponseResult<PublicHolidayResponseVM>(HttpStatusCode.OK, response, "master type created  successfully");
            }
            return new ResponseResult<PublicHolidayResponseVM>(HttpStatusCode.BadRequest, response, "master type not created");
        }

        [HttpPut("/holiday/{id}/tenant/{tenantId}")]
        public ResponseResult<PublicHolidayResponseVM> UpdateMaster(int id, int tenantId, [FromBody] PublicHolidayRequestVM request)
        {
            var response = publicHolidayService.UpdatePublicHoliday(id, tenantId, request);
            if (response != null)
            {
                return new ResponseResult<PublicHolidayResponseVM>(HttpStatusCode.OK, response, "master type updated successfully");
            }
            return new ResponseResult<PublicHolidayResponseVM>(HttpStatusCode.BadRequest, response, "master type not updated");
        }

        [HttpDelete("/holiday/{id}/{tenantId}")]
        public ResponseResult<PublicHolidayResponseVM> DeleteByIdAndTenantId([FromRoute] int id, [FromRoute] int tenantId)
        {
            var response = publicHolidayService.DeleteById(id, tenantId);
            if (response != null)
            {
                return new ResponseResult<PublicHolidayResponseVM>(HttpStatusCode.OK, response, "Deleted Successfully");
            }
            return new ResponseResult<PublicHolidayResponseVM>(HttpStatusCode.NoContent, response, $"No Data found with Id {id}");

        }
    }
}
