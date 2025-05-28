using NeuroPi.UserManagment.Model;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Contact;
using System.Collections.Generic;
using System.Linq;

//Code written by Kiran updated on 2023-05-27

namespace SchoolManagement.Services.Implementation
{
    public class ContactServiceImpl : IContactService
    {
        private readonly SchoolManagementDb _dbContext;

        public ContactServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }

        // Creates a new contact and saves it to the database
        // Developed by: Kiran
        public ContactResponseVM createContact(ContactRequestVM contact)
        {
            var newContact = ContactRequestVM.ToModel(contact);
            newContact.CreatedOn = DateTime.UtcNow;
            _dbContext.Contacts.Add(newContact);
            _dbContext.SaveChanges();
            return ContactResponseVM.ToViewModel(newContact);
        }

        // Deletes a contact by ID and marks it as deleted
        // Developed by: Kiran
        public bool deleteContactByIdAndTenantId(int id, int tenantId)
        {
            var contact = _dbContext.Contacts
                .FirstOrDefault(c => c.Id == id && c.TenantId == tenantId && !c.IsDeleted);
            if (contact == null)
            {
                return false;
            }
            contact.IsDeleted = true;
            contact.UpdatedOn = DateTime.UtcNow;
            _dbContext.SaveChanges();
            return true;


        }

        // Retrieves all contacts that are not deleted
        // Developed by: Kiran
        public List<ContactResponseVM> GetAllContacts()
        {
            return ContactResponseVM.ToViewModelList(_dbContext.Contacts
                .Where(c => !c.IsDeleted)
                .ToList());


        }

        // Retrieves a contact by its ID
        // Developed by: Kiran
        public ContactResponseVM GetContactById(int id)
        {
            var contact = _dbContext.Contacts.FirstOrDefault(c => c.Id == id && !c.IsDeleted);
            if (contact == null)
            {
                return null;
            }
            return ContactResponseVM.ToViewModel(contact);
        }

        // Retrieves a contact by its ID and Tenant ID
        // Developed by: Kiran
        public ContactResponseVM GetContactByIdAndTenantId(int id, int tenantId)
        {
            var contact = _dbContext.Contacts.FirstOrDefault(c => c.Id == id && c.TenantId == tenantId &&!c.IsDeleted);
            if (contact == null)
            {
                return null;
            }
            return ContactResponseVM.ToViewModel(contact);
        }

        // Retrieves all contacts for a specific tenant that are not deleted
        // Developed by: Kiran
        public List<ContactResponseVM> GetContactsByTenant(int tenantId)
        {
            return ContactResponseVM.ToViewModelList(_dbContext.Contacts
                .Where(c => c.TenantId == tenantId && !c.IsDeleted)
                .ToList());
        }

        // Updates an existing contact by its ID and Tenant ID
        // Developed by: Kiran
        public ContactResponseVM updateContact(int id, int tenantId, ContactUpdateVM contact)
        {
            var existingContact = _dbContext.Contacts.FirstOrDefault(c => c.Id == id && c.TenantId == tenantId);
            if (contact == null)
            {
                return null;
            }
            existingContact.Name = contact.Name;
            existingContact.PriNumber = contact.PriNumber;
            existingContact.SecNumber = contact.SecNumber;
            existingContact.Email = contact.Email;
            existingContact.Address1 = contact.Address1;
            existingContact.Address2 = contact.Address2;
            existingContact.State = contact.State;
            existingContact.City = contact.City;
            existingContact.Pincode = contact.Pincode;
            existingContact.UpdatedOn = DateTime.UtcNow;
            existingContact.UpdatedBy = contact.UpdatedBy;

            _dbContext.SaveChanges();
            return ContactResponseVM.ToViewModel(existingContact);

        }
    }
}