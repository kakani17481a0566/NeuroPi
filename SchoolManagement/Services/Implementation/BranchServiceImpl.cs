using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Branch;
using SchoolManagement.Data;
using SchoolManagement.Model;
using System.Collections.Generic;
using System.Linq;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class BranchServiceImpl : IBranchService
    {
        private readonly SchoolManagementDb _dbContext;

        public BranchServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BranchResponseVM> GetAllBranches(int tenantId)
        {
            return _dbContext.Set<MBranch>()
                .Where(b => b.TenantId == tenantId)
                .Select(b => new BranchResponseVM
                {
                    Id = b.Id,
                    Name = b.Name,
                    Contact = b.Contact,
                    Address = b.Address,
                    Pincode = b.Pincode,
                    District = b.District,
                    State = b.State,
                    TenantId = b.TenantId
                })
                .ToList();
        }
    }
}
