using NeuroPi.UserManagment.Model;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class ContactServiceImpl : IContactService
    {
        private readonly SchoolManagementDb _dbContext;

        public ContactServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }

        public List<MContact> GetAllContacts()
        {
            return _dbContext.Contacts.ToList();
        }

        public List<MContact> GetContactsByTenant(int tenantId)
        {
            return _dbContext.Contacts
                .Where(c => c.TenantId == tenantId)
                .ToList();
        }
    }
}