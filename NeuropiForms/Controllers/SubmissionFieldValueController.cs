using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuropiForms.Services.Interface;
using NeuropiForms.ViewModels.SubmissionFieldValue;
using System.Net;

namespace NeuropiForms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionFieldValueController : ControllerBase
    {
        private readonly ISubmissionFieldValueService submissionFieldValueService;

        public SubmissionFieldValueController(ISubmissionFieldValueService submissionFieldValue)
        {
            submissionFieldValueService = submissionFieldValue;
        }

        [HttpGet("/submissionFieldValue/{id}")]
        public ResponseResult<SubmissionFieldValueResponseVM> GetById(int id)
        {
            var result=submissionFieldValueService.GetById(id);
            if (result != null)
            {
                return new ResponseResult<SubmissionFieldValueResponseVM>(HttpStatusCode.OK, result,"retrieved successfully");
            }
            return new ResponseResult<SubmissionFieldValueResponseVM>(HttpStatusCode.NotFound, result, "Submission Field value  is not found");
        }
        [HttpGet("/allSubmissionFieldValues")]
        public ResponseResult<List<SubmissionFieldValueResponseVM>> GetAll() 
        {
            var result = submissionFieldValueService.GetAllSubmissionFieldValues();
            if(result != null)
            {
                return new ResponseResult<List<SubmissionFieldValueResponseVM>>(HttpStatusCode.OK, result, "retrieved all the submission field values successfully");
            }
            return new ResponseResult<List<SubmissionFieldValueResponseVM>>(HttpStatusCode.NotFound, result, "Submission field values not found ");

        }
        [HttpPost]
        public ResponseResult<SubmissionFieldValueResponseVM> AddSubmissionFieldValue(SubmissionFieldValueRequestVM request)
        {
            var result=submissionFieldValueService.AddSubmissionFieldValue(request);
            if (result != null)
            {
                return new ResponseResult<SubmissionFieldValueResponseVM>(HttpStatusCode.Created, result, "Created submission field value successfully");
            }
            return new ResponseResult<SubmissionFieldValueResponseVM>(HttpStatusCode.BadRequest, result, "Not created Submission Field Value");

        }
        [HttpDelete("/submissionFieldValue/{id}")]
        public ResponseResult<SubmissionFieldValueResponseVM> DeleteSubmissionFieldValue(int id)
        {
            var result=submissionFieldValueService.DeleteSubmissionValue(id);
            if(result != null)
            {
                return new ResponseResult<SubmissionFieldValueResponseVM>(HttpStatusCode.OK, result, "Deleted Successfully");
            }
            return new ResponseResult<SubmissionFieldValueResponseVM>(HttpStatusCode.BadRequest, result, "Not Found any data");
        }
        [HttpPut]
        public ResponseResult<SubmissionFieldValueResponseVM> UpdateSubmissionFieldValue(int id ,SubmissionFieldValueUpdateVM updateVM)
        {
            var result=submissionFieldValueService.UpdateSubmissionFieldValue(id, updateVM);
            if(result != null)
            {
                return new ResponseResult<SubmissionFieldValueResponseVM>(HttpStatusCode.Accepted, result, "Updated SuccessFully Submission Field Value");
            }
            return new ResponseResult<SubmissionFieldValueResponseVM>(HttpStatusCode.BadRequest, result, "Not Updated Submission Field value");
        }
    }
}
