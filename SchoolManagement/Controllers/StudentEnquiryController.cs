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

        // ✅ Create Enquiry
        [HttpPost("create")]
        public ResponseResult<int> CreateStudentEnquiry([FromBody] StudentEnquiryRequestDataVM vm)
        {
            if (vm == null)
            {
                return new ResponseResult<int>(
                    HttpStatusCode.BadRequest,
                    0,
                    "Invalid request payload"
                );
            }

            try
            {
                var enquiryId = _studentsEnquiry.CreateStudentEnquiry(vm);

                return new ResponseResult<int>(
                    HttpStatusCode.Created,
                    enquiryId,
                    "Student enquiry created successfully"
                );
            }
            catch (FormatException fex)
            {
                // Catch invalid base64 for signature
                return new ResponseResult<int>(
                    HttpStatusCode.BadRequest,
                    0,
                    $"Invalid signature format: {fex.Message}"
                );
            }
            catch (Exception ex)
            {
                return new ResponseResult<int>(
                    HttpStatusCode.InternalServerError,
                    0,
                    $"Failed to create enquiry: {ex.Message}"
                );
            }
        }

        // ✅ Get All
        [HttpGet]
        public ResponseResult<List<StudentEnquiryResponseVM>> GetAllStudentEnquiries()
        {
            var studentEnquiry = _studentsEnquiry.GetAllStudentEnquiries();
            if (studentEnquiry == null || studentEnquiry.Count == 0)
            {
                return new ResponseResult<List<StudentEnquiryResponseVM>>(
                    HttpStatusCode.NotFound,
                    null,
                    "Student enquiries not found"
                );
            }
            return new ResponseResult<List<StudentEnquiryResponseVM>>(
                HttpStatusCode.OK,
                studentEnquiry,
                "Student enquiry list returned successfully"
            );
        }

        // ✅ Get by Id
        [HttpGet("{id}")]
        public ResponseResult<StudentEnquiryResponseVM> GetStudentEnquiryById([FromRoute] int id)
        {
            var studentEnquiry = _studentsEnquiry.GetStudentEnquiryById(id);
            if (studentEnquiry == null)
            {
                return new ResponseResult<StudentEnquiryResponseVM>(
                    HttpStatusCode.NotFound,
                    null,
                    $"Student enquiry with Id {id} not found"
                );
            }
            return new ResponseResult<StudentEnquiryResponseVM>(
                HttpStatusCode.OK,
                studentEnquiry,
                "Student enquiry details fetched successfully"
            );
        }

        // ✅ Get by Tenant
        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<StudentEnquiryResponseVM>> GetStudentEnquiriesByTenant([FromRoute] int tenantId)
        {
            var studentEnquiry = _studentsEnquiry.GetStudentEnquiriesByTenant(tenantId);
            if (studentEnquiry == null || studentEnquiry.Count == 0)
            {
                return new ResponseResult<List<StudentEnquiryResponseVM>>(
                    HttpStatusCode.NotFound,
                    null,
                    $"Student enquiries with Tenant Id {tenantId} not found"
                );
            }
            return new ResponseResult<List<StudentEnquiryResponseVM>>(
                HttpStatusCode.OK,
                studentEnquiry,
                $"Student enquiry list with Tenant Id {tenantId} returned successfully"
            );
        }

        // ✅ Get by Id & Tenant
        [HttpGet("{id}/{tenantId}")]
        public ResponseResult<StudentEnquiryResponseVM> GetStudentEnquiryByIdAndTenant([FromRoute] int id, [FromRoute] int tenantId)
        {
            var studentEnquiry = _studentsEnquiry.GetStudentEnquiryByIdAndTenant(id, tenantId);
            if (studentEnquiry == null)
            {
                return new ResponseResult<StudentEnquiryResponseVM>(
                    HttpStatusCode.NotFound,
                    null,
                    $"Student enquiry with Id {id} and Tenant Id {tenantId} not found"
                );
            }
            return new ResponseResult<StudentEnquiryResponseVM>(
                HttpStatusCode.OK,
                studentEnquiry,
                "Student enquiry details fetched successfully"
            );
        }

        // ✅ Delete
        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<bool> DeleteStudentEnquiryByIdAndTenant([FromRoute] int id, [FromRoute] int tenantId)
        {
            var isDeleted = _studentsEnquiry.DeleteStudentEnquiryByIdAndTenant(id, tenantId);
            if (isDeleted)
            {
                return new ResponseResult<bool>(
                    HttpStatusCode.OK,
                    true,
                    $"Student enquiry with Id {id} and Tenant Id {tenantId} deleted successfully"
                );
            }
            return new ResponseResult<bool>(
                HttpStatusCode.NotFound,
                false,
                $"Student enquiry with Id {id} and Tenant Id {tenantId} not found"
            );
        }

        // ✅ Update
        [HttpPut("{id}/tenant/{tenantId}")]
        public ResponseResult<StudentEnquiryResponseVM> UpdateStudentEnquiry(
            [FromRoute] int id,
            [FromRoute] int tenantId,
            [FromBody] StudentEnquiryUpdateVM vm)
        {
            if (vm == null)
            {
                return new ResponseResult<StudentEnquiryResponseVM>(
                    HttpStatusCode.BadRequest,
                    null,
                    "Invalid request payload"
                );
            }

            var updatedEnquiry = _studentsEnquiry.UpdateStudentEnquiry(id, tenantId, vm);
            if (updatedEnquiry == null)
            {
                return new ResponseResult<StudentEnquiryResponseVM>(
                    HttpStatusCode.NotFound,
                    null,
                    $"Student enquiry with Id {id} and Tenant Id {tenantId} not found"
                );
            }

            return new ResponseResult<StudentEnquiryResponseVM>(
                HttpStatusCode.OK,
                updatedEnquiry,
                "Student enquiry updated successfully"
            );
        }


        [HttpGet("student-enquiry-new/tenant/{tenantId}/branch/{branchId}/display")]

        public ResponseResult<List<StudentEnquiryDisplay>> GetStudentEnquiryDisplayByTenantAndBranch(
           [FromRoute] int tenantId,
           [FromRoute] int branchId)
        {
            var enquiries = _studentsEnquiry.GetStudentEnquiryDisplayByTenantAndBranch(tenantId, branchId);

            if (enquiries == null || enquiries.Count == 0)
            {
                return new ResponseResult<List<StudentEnquiryDisplay>>(
                    HttpStatusCode.NotFound,
                    null,
                    $"No student enquiries found for Tenant Id {tenantId} and Branch Id {branchId}"
                );
            }

            return new ResponseResult<List<StudentEnquiryDisplay>>(
                HttpStatusCode.OK,
                enquiries,
                $"Student enquiries for Tenant Id {tenantId} and Branch Id {branchId} returned successfully"
            );
        }
    }
}
