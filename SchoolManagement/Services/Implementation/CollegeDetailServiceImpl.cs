using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.CollegeDetail;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class CollegeDetailServiceImpl : ICollegeDetailService
    {
        private readonly SchoolManagementDb _context;

        public CollegeDetailServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public List<CollegeDetailResponseVM> GetAllByTenantId(int tenantId)
        {
            var result = _context.CollegeDetails
                .Where(c => c.TenantId == tenantId)
                .Select(c => new CollegeDetailResponseVM
                {
                    Id = c.Id,
                    CollegeName = c.CollegeName,
                    AddressLine1 = c.AddressLine1,
                    AddressLine2 = c.AddressLine2,
                    City = c.City,
                    State = c.State,
                    Pincode = c.Pincode,
                    ContactNumber = c.ContactNumber,
                    Email = c.Email,
                    Status = c.Status,
                    TenantId = c.TenantId
                })
                .ToList();

            return result;
        }
    }
}
