using SchoolManagement.Model;
using SchoolManagement.ViewModel.Contact;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IContactService
    {
        // Retrieves all contacts that are not deleted
        List<ContactResponseVM> GetAllContacts();
        // Retrieves all contacts for a specific tenant
        List<ContactResponseVM> GetContactsByTenant(int tenantId);
        // Retrieves a contact by its ID   
        ContactResponseVM GetContactById(int id);
        // Retrieves a contact by its ID and Tenant ID
        ContactResponseVM GetContactByIdAndTenantId(int id, int tenantId);
        // Creates a new contact and saves it to the database
        ContactResponseVM createContact(ContactRequestVM contact);
        // Updates an existing contact by ID and Tenant ID
        ContactResponseVM updateContact(int id, int tenantId, ContactUpdateVM contact);
        // Deletes a contact by ID and Tenant ID, marking it as deleted
        bool deleteContactByIdAndTenantId(int id, int tenantId);


    }
} 