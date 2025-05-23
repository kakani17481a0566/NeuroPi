using SchoolManagement.Model;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IContactService
    {
        List<MContact> GetAllContacts();
        List<MContact> GetContactsByTenant(int tenantId);
    }
}