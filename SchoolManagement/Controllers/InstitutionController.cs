using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.institutions;
using System.Collections.Generic;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitutionService _service;

        public InstitutionController(IInstitutionService service)
        {
            _service = service;
        }

        // get: api/institution
        // get all institutions
        // Developer: Mohith
        [HttpGet]
        public ResponseResult<List<InstitutionResponseVM>> GetAll()
        {
            var institutions = _service.GetAll();
            return new ResponseResult<List<InstitutionResponseVM>>(HttpStatusCode.OK, institutions);
        }

        // get: api/institution/{id}
        // get institution by id
        // Developer: Mohith
        [HttpGet("{id}")]
        public ResponseResult<InstitutionResponseVM> GetById(int id)
        {
            var institution = _service.GetById(id);
            if (institution == null)
                return new ResponseResult<InstitutionResponseVM>(HttpStatusCode.NotFound, null, $"Institution with id {id} not found");

            return new ResponseResult<InstitutionResponseVM>(HttpStatusCode.OK, institution);
        }

        // post: api/institution
        // create a new institution
        // Developer: Mohith
        [HttpPost]
        public ResponseResult<InstitutionResponseVM> Create([FromBody] InstitutionCreateRequestVM request)
        {
            var createdInstitution = _service.Create(request);
            return new ResponseResult<InstitutionResponseVM>(HttpStatusCode.Created, createdInstitution, "Institution created successfully");
        }

        // put: api/institution/{id}/tenant/{tenantId}
        // update institution by id and tenant id
        // Developer: Mohith
        [HttpPut("{id}/tenant/{tenantId}")]
        public ResponseResult<InstitutionResponseVM> UpdateByIdAndTenantId(int id, int tenantId, [FromBody] InstitutionUpdateRequestVM request)
        {
            var updatedInstitution = _service.UpdateByIdAndTenantId(id, tenantId, request);
            if (updatedInstitution == null)
                return new ResponseResult<InstitutionResponseVM>(HttpStatusCode.NotFound, null, $"Institution with id {id} and tenant {tenantId} not found");

            return new ResponseResult<InstitutionResponseVM>(HttpStatusCode.OK, updatedInstitution, "Institution updated successfully");
        }

        // delete: api/institution/{id}/tenant/{tenantId}
        // delete institution by id and tenant id
        // Developer: Mohith
        [HttpDelete("{id}/tenant/{tenantId}")]
        public ResponseResult<bool> DeleteByIdAndTenantId(int id, int tenantId, [FromQuery] bool deleteContact = false)
        {
            var success = _service.DeleteByIdAndTenantId(id, tenantId, deleteContact);
            if (!success)
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, $"Institution with id {id} and tenant {tenantId} not found");

            var message = deleteContact
                ? "Institution and related contact deleted successfully"
                : "Institution deleted successfully";

            return new ResponseResult<bool>(HttpStatusCode.OK, true, message);
        }

        // get: api/institution/{id}/tenant/{tenantId}
        // get institution and contact details by id and tenant id
        // Developer: Mohith
        [HttpGet("{id}/tenant/{tenantId}")]
        public ResponseResult<InstitutionWithContactResponseVM> GetByIdAndTenantId(int id, int tenantId)
        {
            var result = _service.GetByIdAndTenantId(id, tenantId);
            if (result == null)
                return new ResponseResult<InstitutionWithContactResponseVM>(HttpStatusCode.NotFound, null, $"Institution with id {id} and tenant {tenantId} not found.");

            return new ResponseResult<InstitutionWithContactResponseVM>(HttpStatusCode.OK, result);
        }


        // post: api/institution/with-contact
        // Create a new institution with contact details
        // Developer: Mohith
        [HttpPost("with-contact")]
        public ResponseResult<InstitutionWithContactResponseVM> CreateWithContact([FromBody] InstitutionWithContactRequestVM request)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseResult<InstitutionWithContactResponseVM>(HttpStatusCode.BadRequest, null, "Invalid request data");
            }

            try
            {
                var response = _service.CreateWithContact(request);
                return new ResponseResult<InstitutionWithContactResponseVM>(HttpStatusCode.Created, response, "Institution with contact created successfully");
            }
            catch (System.Exception ex)
            {
                return new ResponseResult<InstitutionWithContactResponseVM>(HttpStatusCode.InternalServerError, null, $"Internal server error: {ex.Message}");
            }
        }
    }
}
