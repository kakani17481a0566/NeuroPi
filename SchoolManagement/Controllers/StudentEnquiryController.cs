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


        [HttpGet]
        public ResponseResult<List<StudentEnquiryResponseVM>> GetAllStudentEnquiries()
        {
            var studentEnquiry = _studentsEnquiry.GetAllStudentEnquiries();
            if (studentEnquiry == null)
            {
                return new ResponseResult<List<StudentEnquiryResponseVM>>(HttpStatusCode.NotFound, studentEnquiry, "Student Enquiry Not Found");
            }
            return new ResponseResult<List<StudentEnquiryResponseVM>>(HttpStatusCode.OK, studentEnquiry, "Student Enquiry List returned Successfully");
        }

        [HttpGet("{id}")]
        public ResponseResult<StudentEnquiryResponseVM> GetStudentEnquiryById([FromRoute] long id)
        {
            var studentEnquiry = _studentsEnquiry.GetStudentEnquiryById(id);
            if (studentEnquiry == null)
            {
                return new ResponseResult<StudentEnquiryResponseVM>(HttpStatusCode.NotFound, studentEnquiry, $"Student Enquiry With Id {id} Not Found");
            }
            return new ResponseResult<StudentEnquiryResponseVM>(HttpStatusCode.OK, studentEnquiry, "Student Enquiry Details Fetched Successfully");
        }
        [HttpGet("tenantId/{tenantId}")]
        public ResponseResult<List<StudentEnquiryResponseVM>> GetStudentEnquiriesByTenant([FromRoute] int tenantId)
        {
            var studentEnquiry =_studentsEnquiry.GetStudentEnquiriesByTenant(tenantId);
            if (studentEnquiry == null)
            {
                return new ResponseResult<List<StudentEnquiryResponseVM>>(HttpStatusCode.NotFound, studentEnquiry, $"Students Enquiry With Tenant Id{tenantId} Not Found");
            }
            return new ResponseResult<List<StudentEnquiryResponseVM>>(HttpStatusCode.OK, studentEnquiry, $"Students Enquiry List with Tenant Id{tenantId} returned Successfully");
        }

        [HttpGet("{id}/{tenantId}")]
        public ResponseResult<StudentEnquiryResponseVM> GetStudentEnquiryByIdAndTenant([FromRoute] long id, [FromRoute] int tenantId)
        {
            var studentEnquiry = _studentsEnquiry.GetStudentEnquiryByIdAndTenant(id, tenantId);
            if (studentEnquiry == null)
            {
                return new ResponseResult<StudentEnquiryResponseVM>(HttpStatusCode.NotFound, studentEnquiry, $"Student Enquiry With Id {id} and Tenant Id {tenantId} Not Found");
            }
            return new ResponseResult<StudentEnquiryResponseVM>(HttpStatusCode.OK, studentEnquiry, "Student Enquiry Details Fetched Successfully");
        }

        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<bool> DeleteStudentEnquiryByIdAndTenant([FromRoute] long id, [FromRoute] int tenantId)
        {
            var isDeleted = _studentsEnquiry.DeleteStudentEnquiryByIdAndTenant(id, tenantId);
            if (isDeleted)
            {
                return new ResponseResult<bool>(HttpStatusCode.OK, true, $"Student Enquiry With Id {id} and Tenant Id {tenantId} Deleted Successfully");
            }
            return new ResponseResult<bool>(HttpStatusCode.NotFound, false, $"Student Enquiry With Id {id} and Tenant Id {tenantId} Not Found");
        }

        [HttpPut("{id}/tenant/{tenantId}")]
        public ResponseResult<StudentEnquiryResponseVM> UpdateStudentEnquiry([FromRoute] long id, [FromRoute] int tenantId, [FromBody] StudentEnquiryUpdateVM vm)
        {
            if (vm == null)
            {
                return new ResponseResult<StudentEnquiryResponseVM>(HttpStatusCode.BadRequest, null, "Invalid request payload");
            }
            var updatedEnquiry = _studentsEnquiry.UpdateStudentEnquiry(id, tenantId, vm);
            if (updatedEnquiry == null)
            {
                return new ResponseResult<StudentEnquiryResponseVM>(HttpStatusCode.NotFound, null, $"Student Enquiry With Id {id} and Tenant Id {tenantId} Not Found");
            }
            return new ResponseResult<StudentEnquiryResponseVM>(HttpStatusCode.OK, updatedEnquiry, "Student Enquiry Updated Successfully");
        }
    }
}
