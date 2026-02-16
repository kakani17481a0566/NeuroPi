using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuropiForms.Services.Interface;
using NeuropiForms.ViewModels.FormSubmission;
using System.Net;

namespace NeuropiForms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormSubmissionController : ControllerBase
    {
        public IFormSubmissionService FormSubmissionService;

        public FormSubmissionController(IFormSubmissionService formSubmissionService)
        {
            FormSubmissionService = formSubmissionService;
            
        }
        [HttpPost]
        public ResponseResult<FormSubmissionResponseVM> AddFormSubmission([FromBody] FormSubmissionRequestVM formSubmissionRequestVM)
        {
            var result = FormSubmissionService.AddFormSubmission(formSubmissionRequestVM);
            if (result != null)
            {
                return new ResponseResult<FormSubmissionResponseVM>(HttpStatusCode.OK, result, "Added successfully");
            }
            return new ResponseResult<FormSubmissionResponseVM>(HttpStatusCode.NotFound, result, "Not added");
        }
        [HttpGet("/{id}")]
        public ResponseResult<FormSubmissionResponseVM> GetFormSubmission(int id)
        {
            var result = FormSubmissionService.GetFormSubmission(id);
            if (result != null)
            {
                return new ResponseResult<FormSubmissionResponseVM>(HttpStatusCode.OK, result, "Retrieved Successfully ");
            }
            return new ResponseResult<FormSubmissionResponseVM>(HttpStatusCode.NotFound, result, "Not Found");
        }
        [HttpGet("/allFormSubmission")]
        public ResponseResult<List<FormSubmissionResponseVM>> GetAllFormSubmissions()
        {
            var result = FormSubmissionService.GetAllFormSubmissions();
            if (result != null)
            {
                return new ResponseResult<List<FormSubmissionResponseVM>>(HttpStatusCode.OK, result, "Retrieved  all form submissions Successfully ");
            }
            return new ResponseResult<List<FormSubmissionResponseVM>>(HttpStatusCode.NotFound, result, "No Data Found");
        }
        [HttpDelete("/{id}")]
        public ResponseResult<FormSubmissionResponseVM> DeleteFormSubmission(int id)
        {
            var result = FormSubmissionService.DeleteFormSubmission(id);
            if (result != null)
            {
                return new ResponseResult<FormSubmissionResponseVM>(HttpStatusCode.NoContent, result, "Deleted Successfully ");
            }
            return new ResponseResult<FormSubmissionResponseVM>(HttpStatusCode.Conflict, result, "No Data Found");
        }
        [HttpPut("/{id}")]
        public ResponseResult<FormSubmissionResponseVM> UpdateFormSubmission(int id,FormSubmissionUpdateVM formSubmissionUpdate)
        {
            var result = FormSubmissionService.UpdateFormSubmission(id,formSubmissionUpdate);
            if (result != null)
            {
                return new ResponseResult<FormSubmissionResponseVM>(HttpStatusCode.OK, result, "updated successfully ");
            }
            return new ResponseResult<FormSubmissionResponseVM>(HttpStatusCode.NotFound, result, "No Data Found");
        }
    }
}
