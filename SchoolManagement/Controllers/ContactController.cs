using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Model;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Contact;
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


        //Get all Contacts
        //GET: api/contact
        // Developed by: Kiran

        [HttpGet]
        public ResponseResult<IEnumerable<ContactResponseVM>> GetAll()
        {
            var contacts = _contactService.GetAllContacts();
            return new ResponseResult<IEnumerable<ContactResponseVM>>(HttpStatusCode.OK, contacts, "All contacts retrieved successfully");
        }

        //Get Contacts by Tenant Id
        //GET: api/contact/GetByTenant/{tenantId}
        // Developed by: Kiran
        [HttpGet("GetByTenant/{tenantId}")]
        public ResponseResult<List<ContactResponseVM>> GetByTenant(int tenantId)
        {
            var contacts = _contactService.GetContactsByTenant(tenantId);
            return contacts == null
                ? new ResponseResult<List<ContactResponseVM>>(HttpStatusCode.NotFound, null, "No contacts found for the specified tenant")
                : new ResponseResult<List<ContactResponseVM>>(HttpStatusCode.OK, contacts, "Contacts retrieved successfully");
        }

        //Get Contacts by Id
        //GET: api/contact/{id}
        // Developed by: Kiran
        [HttpGet("{id}")]
        public ResponseResult<ContactResponseVM> GetById(int id)
        {
            var contact = _contactService.GetContactById(id);
            return contact == null
                ? new ResponseResult<ContactResponseVM>(HttpStatusCode.NotFound, null, "Contact not found")
                : new ResponseResult<ContactResponseVM>(HttpStatusCode.OK, contact, "Contact retrieved successfully");
        }

        //Get Contacts by Id and Tenant Id
        //GET: api/contact/{id}/{tenantId}
        // Developed by: Kiran

        [HttpGet("{id}/{tenantId}")]
        public ResponseResult<ContactResponseVM> GetByIdAndTenantId(int id, int tenantId)
        {
            var contact = _contactService.GetContactByIdAndTenantId(id, tenantId);
            return contact == null
                ? new ResponseResult<ContactResponseVM>(HttpStatusCode.NotFound, null, "Contact not found")
                : new ResponseResult<ContactResponseVM>(HttpStatusCode.OK, contact, "Contact retrieved successfully");
        }

        //Create a new Contact
        //POST: api/contact
        // Developed by: Kiran
        [HttpPost]
        public ResponseResult<ContactResponseVM> Create([FromBody] ContactRequestVM contact)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseResult<ContactResponseVM>(HttpStatusCode.BadRequest, null, "Invalid contact data");
            }
            var createdContact = _contactService.createContact(contact);
            return new ResponseResult<ContactResponseVM>(HttpStatusCode.Created, createdContact, "Contact created successfully");
        }

        //Update the exicting Contact by usning Id and Tenanant Id
        //PUT: api/contact/{id}/{tenantId}
        // Developed by: Kiran

        [HttpPut("{id}/{tenantId}")]
        public ResponseResult<ContactResponseVM> Update(int id, int tenantId, [FromBody] ContactUpdateVM contact)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseResult<ContactResponseVM>(HttpStatusCode.BadRequest, null, "Invalid contact data");
            }
            var updatedContact = _contactService.updateContact(id, tenantId, contact);
            return updatedContact == null
                ? new ResponseResult<ContactResponseVM>(HttpStatusCode.NotFound, null, "Contact not found")
                : new ResponseResult<ContactResponseVM>(HttpStatusCode.OK, updatedContact, "Contact updated successfully");
        }

        //Delete by Id and Tenant Id
        // DELETE: api/contact/{id}/{tenantId}
        // Developed by: Kiran
        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<bool> Delete(int id, int tenantId)
        {
            var isDeleted = _contactService.deleteContactByIdAndTenantId(id, tenantId);
            return isDeleted
                ? new ResponseResult<bool>(HttpStatusCode.OK, true, "Contact deleted successfully")
                : new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Contact not found");

        }
    }
}