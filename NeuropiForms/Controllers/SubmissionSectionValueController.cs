
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuropiForms.Services.Interface;
using NeuropiForms.ViewModels.SubmissionSectionValue;
using System.Net;

namespace NeuropiForms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionSectionValueController : ControllerBase
    {
        private readonly ISubmissionSectionValueService submissionSectionValueService;

        public SubmissionSectionValueController(ISubmissionSectionValueService submissionFieldValue)
        {
            submissionSectionValueService = submissionFieldValue;
        }

        [HttpGet("/submissionSectionValue/{id}")]
        public ResponseResult<SubmissionSectionValueResponseVM> GetById(int id)
        {
            var result =submissionSectionValueService.GetSubmissionSectionById(id);
            if (result != null)
            {
                return new ResponseResult<SubmissionSectionValueResponseVM>(HttpStatusCode.OK, result, "retrieved successfully");
            }
            return new ResponseResult<SubmissionSectionValueResponseVM>(HttpStatusCode.NotFound, result, "Submission Section value  is not found");
        }
        [HttpGet("/allSubmissionSectionValues")]
        public ResponseResult<List<SubmissionSectionValueResponseVM>> GetAll()
        {
            var result = submissionSectionValueService.GetSubmissionSections();
            if (result != null)
            {
                return new ResponseResult<List<SubmissionSectionValueResponseVM>>(HttpStatusCode.OK, result, "retrieved all the submission Section values successfully");
            }
            return new ResponseResult<List<SubmissionSectionValueResponseVM>>(HttpStatusCode.NotFound, result, "Submission Section values not found ");

        }
        [HttpPost]
        public ResponseResult<SubmissionSectionValueResponseVM> AddSubmissionFieldValue(SubmissionSectionValueRequestVM request)
        {
            var result = submissionSectionValueService.AddSubmissionSection(request);
            if (result != null)
            {
                return new ResponseResult<SubmissionSectionValueResponseVM>(HttpStatusCode.Created, result, "Created submission Section value successfully");
            }
            return new ResponseResult<SubmissionSectionValueResponseVM>(HttpStatusCode.BadRequest, result, "Not created Submission Section Value");

        }
        [HttpDelete("/submissionSectionValue/{id}")]
        public ResponseResult<SubmissionSectionValueResponseVM> DeleteSubmissionFieldValue(int id)
        {
            var result = submissionSectionValueService.DeleteSubmissionSectionValue(id);
            if (result != null)
            {
                return new ResponseResult<SubmissionSectionValueResponseVM>(HttpStatusCode.OK, result, "Deleted Successfully");
            }
            return new ResponseResult<SubmissionSectionValueResponseVM>(HttpStatusCode.BadRequest, result, "Not Found any data");
        }
        [HttpPut]
        public ResponseResult<SubmissionSectionValueResponseVM> UpdateSubmissionFieldValue(int id, SubmissionSectionValueUpdateVM updateVM)
        {
            var result = submissionSectionValueService.UpdateSubmissionSectionValue(id, updateVM);
            if (result != null)
            {
                return new ResponseResult<SubmissionSectionValueResponseVM>(HttpStatusCode.Accepted, result, "Updated SuccessFully Submission Section Value");
            }
            return new ResponseResult<SubmissionSectionValueResponseVM>(HttpStatusCode.BadRequest, result, "Not Updated Submission Section value");
        }
    }
}
