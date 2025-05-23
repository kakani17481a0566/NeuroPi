using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Model;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using System.Collections.Generic;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public ResponseResult<IEnumerable<MContact>> GetAll()
        {
            var contacts = _contactService.GetAllContacts();
            return new ResponseResult<IEnumerable<MContact>>(HttpStatusCode.OK, contacts, "All contacts retrieved successfully");
        }

        [HttpGet("GetByTenant/{tenantId}")]
        public ResponseResult<IEnumerable<MContact>> GetByTenant(int tenantId)
        {
            var contacts = _contactService.GetContactsByTenant(tenantId);
            return new ResponseResult<IEnumerable<MContact>>(HttpStatusCode.OK, contacts, $"Contacts for tenant {tenantId} retrieved successfully");
        }
    }
}