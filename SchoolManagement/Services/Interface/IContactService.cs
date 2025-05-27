using SchoolManagement.Model;
using SchoolManagement.ViewModel.Contact;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IContactService
    {
        List<ContactResponseVM> GetAllContacts();
        List<ContactResponseVM> GetContactsByTenant(int tenantId);

        ContactResponseVM GetContactById(int id);

        ContactResponseVM GetContactByIdAndTenantId(int id, int tenantId);

        ContactResponseVM createContact(ContactRequestVM contact);

        ContactResponseVM updateContact(int id, int tenantId, ContactUpdateVM contact);


        bool deleteContactByIdAndTenantId(int id, int tenantId);


    }
}