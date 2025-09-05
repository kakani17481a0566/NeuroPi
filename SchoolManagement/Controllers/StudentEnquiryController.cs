using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.StudentsEnquiry;
using System;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentEnquiryController : ControllerBase
    {
        private readonly IStudentsEnquiry _studentsEnquiry;

        public StudentEnquiryController(IStudentsEnquiry studentsEnquiry)
        {
            _studentsEnquiry = studentsEnquiry;
        }

        // POST: api/StudentEnquiry/create
        [HttpPost("create")]
        public ResponseResult<long> CreateStudentEnquiry([FromBody] StudentEnquiryRequestDataVM vm)
        {
            if (vm == null)
            {
                return new ResponseResult<long>(
                    HttpStatusCode.BadRequest,
                    0,
                    "Invalid request payload"
                );
            }

            try
            {
                var enquiryId = _studentsEnquiry.CreateStudentEnquiry(vm);

                return new ResponseResult<long>(
                    HttpStatusCode.Created,
                    enquiryId,
                    "Student enquiry created successfully"
                );
            }
            catch (Exception ex)
            {
                return new ResponseResult<long>(
                    HttpStatusCode.InternalServerError,
                    0,
                    $"Failed to create enquiry: {ex.Message}"
                );
            }
        }
    }
}
