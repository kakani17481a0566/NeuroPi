using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Corporate;
using SchoolManagement.Data;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class CorporateServiceImpl : ICorporateService
    {
        private readonly SchoolManagementDb _db;

        public CorporateServiceImpl(SchoolManagementDb db)
        {
            _db = db;
        }

        public List<CorporateVM> GetAll(int tenantId)
        {
            return _db.Corporates
                      .Where(c => c.TenantId == tenantId && !c.IsDeleted)
                      .Select(c => new CorporateVM
                      {
                          Id = c.Id,
                          Name = c.Name ?? string.Empty,
                          Discount = c.Discount,
                          ContactId = c.ContactId ?? 0
                      })
                      .ToList();
        }
    }
}
