using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.GeneticRegistration;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneticController : ControllerBase
    {
        private readonly IGeneticRegistrationService _geneticRegistrationService;
        public GeneticController(IGeneticRegistrationService geneticRegistrationService)
        {
            _geneticRegistrationService = geneticRegistrationService;
        }
        [HttpPost]
        public ResponseResult<string> AddGeneticRegistration([FromBody] GeneticRegistrationRequestVM requestVM)
        {
            var result = _geneticRegistrationService.AddGeneticRegistration(requestVM);
            if (result != null) {
                return new ResponseResult<string>(HttpStatusCode.OK, result, "Registered Successfully");
            }
            return new ResponseResult<string>(HttpStatusCode.Forbidden, result, "Not Registered Please try after some time");

        }

        [HttpGet("geneticId-registrationNumber")]
        public ResponseResult<GeneticRegistrationResponseVM> GetGeneticRegistrationByGeneticIdOrRegistrationNumber([FromQuery]string geneticId, [FromQuery] string registrationNumber)
        {
            var geneticRegistration = _geneticRegistrationService.GetGeneticRegistrationByGeneticIdOrRegistrationNumber(geneticId, registrationNumber);
            if (geneticRegistration != null)
            {
                return new ResponseResult<GeneticRegistrationResponseVM>(HttpStatusCode.OK, geneticRegistration, "Genetic Registration retrieved successfully");
            }
            return new ResponseResult<GeneticRegistrationResponseVM>(HttpStatusCode.NoContent, geneticRegistration, $"No data found for the provided Genetic ID{geneticId} or Registration Number{registrationNumber}");
        }

        [HttpGet("user/{userId}")]
        public ResponseResult<GeneticRegistrationResponseVM> GetGeneticRegistrationByUserId(int userId)
        {
            var result = _geneticRegistrationService.GetGeneticRegistrationByUserId(userId);
            if (result != null)
            {
                return new ResponseResult<GeneticRegistrationResponseVM>(HttpStatusCode.OK, result, "User registration retrieved successfully");
            }
            return new ResponseResult<GeneticRegistrationResponseVM>(HttpStatusCode.NoContent, null, "No registration found for this user");
        }

        [HttpGet("all")]
        public ResponseResult<List<GeneticRegistrationResponseVM>> GetAllSubmissions([FromQuery] bool includeDeleted = true)
        {
            var result = _geneticRegistrationService.GetAllSubmissions(includeDeleted);
            if (result != null && result.Any())
            {
                return new ResponseResult<List<GeneticRegistrationResponseVM>>(HttpStatusCode.OK, result, $"Retrieved {result.Count} submission(s) successfully");
            }
            return new ResponseResult<List<GeneticRegistrationResponseVM>>(HttpStatusCode.OK, new List<GeneticRegistrationResponseVM>(), "No submissions found");
        }

        [HttpGet("user/{userId}/all")]
        public ResponseResult<List<GeneticRegistrationResponseVM>> GetAllUserSubmissions(int userId, [FromQuery] bool includeDeleted = false)
        {
            var result = _geneticRegistrationService.GetAllUserSubmissions(userId, includeDeleted);
            if (result != null && result.Any())
            {
                return new ResponseResult<List<GeneticRegistrationResponseVM>>(HttpStatusCode.OK, result, $"Retrieved {result.Count} submission(s) successfully");
            }
            // Return 200 OK with empty list instead of 204 NoContent
            // 204 cannot have a response body, which causes serialization errors
            return new ResponseResult<List<GeneticRegistrationResponseVM>>(HttpStatusCode.OK, new List<GeneticRegistrationResponseVM>(), "No submissions found for this user");
        }

        [HttpDelete("{registrationNumber}")]
        public ResponseResult<string> DeleteSubmission(string registrationNumber, [FromQuery] int userId)
        {
            var result = _geneticRegistrationService.DeleteSubmission(registrationNumber, userId);
            if (result)
            {
                return new ResponseResult<string>(HttpStatusCode.OK, registrationNumber, "Submission deleted successfully");
            }
            return new ResponseResult<string>(HttpStatusCode.BadRequest, null, "Failed to delete submission. Either submission not found or you don't have permission.");
        }
    }
}
