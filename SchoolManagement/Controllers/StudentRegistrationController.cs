using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.StudentRegistration;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentRegistrationController : ControllerBase
    {
        private readonly IStudentRegistration _service;

        public StudentRegistrationController(IStudentRegistration service)
        {
            _service = service;
        }


        [HttpPost]
        public ResponseResult<StudentRegistrationResponseVM> Create([FromBody] StudentRegistrationRequestVM request)
        {
            if (request == null)
            {
                return new ResponseResult<StudentRegistrationResponseVM>(
                    HttpStatusCode.BadRequest,
                    null,
                    "Request body cannot be empty"
                );
            }

            try
            {
                var result = _service.Create(request);

                return new ResponseResult<StudentRegistrationResponseVM>(
                    HttpStatusCode.Created,
                    result,
                    "Student registered successfully"
                );
            }
            catch (ArgumentException ex)
            {
                return new ResponseResult<StudentRegistrationResponseVM>(
                    HttpStatusCode.BadRequest,
                    null,
                    ex.Message
                );
            }
            catch (Exception)
            {
                return new ResponseResult<StudentRegistrationResponseVM>(
                    HttpStatusCode.InternalServerError,
                    null,
                    "An unexpected error occurred"
                );
            }
        }
    }
}
