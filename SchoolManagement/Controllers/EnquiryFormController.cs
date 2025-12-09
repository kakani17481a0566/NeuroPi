using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.EnquiryForm;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryFormController : ControllerBase
    {
        private readonly IEnquiryFormService _service;

        public EnquiryFormController(IEnquiryFormService service)
        {
            _service = service;
        }

        // ---------------------------------------------------------
        // GET ALL BY TENANT
        // GET: api/enquiryform/tenant/{tenantId}
        // ---------------------------------------------------------
        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<EnquiryFormResponseVM>> GetAll(int tenantId)
        {
            var list = _service.GetAll(tenantId);

            return new ResponseResult<List<EnquiryFormResponseVM>>(
                HttpStatusCode.OK,
                list,
                "All enquiries retrieved successfully"
            );
        }

        // ---------------------------------------------------------
        // GET BY UUID + TENANT
        // GET: api/enquiryform/{uuid}/{tenantId}
        // ---------------------------------------------------------
        [HttpGet("{uuid}/{tenantId}")]
        public ResponseResult<EnquiryFormResponseVM> Get(Guid uuid, int tenantId)
        {
            var data = _service.GetByUuid(uuid, tenantId);

            if (data == null)
            {
                return new ResponseResult<EnquiryFormResponseVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "Enquiry not found"
                );
            }

            return new ResponseResult<EnquiryFormResponseVM>(
                HttpStatusCode.OK,
                data,
                "Enquiry retrieved successfully"
            );
        }

        // ---------------------------------------------------------
        // CREATE
        // POST: api/enquiryform
        // ---------------------------------------------------------
        [HttpPost]
        public async Task<ResponseResult<EnquiryFormResponseVM>> Create([FromBody] EnquiryFormRequestVM request)
        {
            if (request == null)
            {
                return new ResponseResult<EnquiryFormResponseVM>(
                    HttpStatusCode.BadRequest,
                    null,
                    "Invalid enquiry data"
                );
            }

            var created = await _service.Create(request, request.CreatedBy);

            return new ResponseResult<EnquiryFormResponseVM>(
                HttpStatusCode.Created,
                created,
                "Enquiry created successfully"
            );
        }

        // ---------------------------------------------------------
        // UPDATE
        // PUT: api/enquiryform/{uuid}/{tenantId}
        // ---------------------------------------------------------
        [HttpPut("{uuid}/{tenantId}")]
        public async Task<ResponseResult<EnquiryFormResponseVM>> Update(
            Guid uuid,
            int tenantId,
            [FromBody] EnquiryFormUpdateVM request
        )
        {
            if (request == null)
            {
                return new ResponseResult<EnquiryFormResponseVM>(
                    HttpStatusCode.BadRequest,
                    null,
                    "Invalid update data"
                );
            }

            var updated = await _service.Update(uuid, request, tenantId, request.UpdatedBy);

            if (updated == null)
            {
                return new ResponseResult<EnquiryFormResponseVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "Enquiry not found"
                );
            }

            return new ResponseResult<EnquiryFormResponseVM>(
                HttpStatusCode.OK,
                updated,
                "Enquiry updated successfully"
            );
        }

        // ---------------------------------------------------------
        // DELETE (Soft Delete)
        // DELETE: api/enquiryform/{uuid}/{tenantId}/{deletedBy}
        // ---------------------------------------------------------
        [HttpDelete("{uuid}/{tenantId}/{deletedBy}")]
        public ResponseResult<bool> Delete(Guid uuid, int tenantId, int deletedBy)
        {
            var isDeleted = _service.Delete(uuid, tenantId, deletedBy);

            if (!isDeleted)
            {
                return new ResponseResult<bool>(
                    HttpStatusCode.NotFound,
                    false,
                    "Enquiry not found"
                );
            }

            return new ResponseResult<bool>(
                HttpStatusCode.OK,
                true,
                "Enquiry deleted successfully"
            );
        }
    }
}
