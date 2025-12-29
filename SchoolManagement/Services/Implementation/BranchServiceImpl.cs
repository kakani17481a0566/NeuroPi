using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Branch;
using SchoolManagement.Model;

namespace SchoolManagement.Services.Implementation
{
    public class BranchServiceImpl : IBranchService
    {
        private readonly SchoolManagementDb _context;

        public BranchServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public List<BranchVM> GetBranchesByTenantId(int tenantId)
        {
            return _context.Set<MBranch>()
                .Where(b => b.TenantId == tenantId && !b.IsDeleted)
                .Select(b => new BranchVM
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
                .OrderBy(b => b.Name)
                .ToList();
        }

        public BranchVM GetBranchById(int id, int tenantId)
        {
            var branch = _context.Set<MBranch>()
                .FirstOrDefault(b => b.Id == id && b.TenantId == tenantId && !b.IsDeleted);

            if (branch == null)
                return null;

            return new BranchVM
            {
                Id = branch.Id,
                Name = branch.Name,
                Contact = branch.Contact,
                Address = branch.Address,
                Pincode = branch.Pincode,
                District = branch.District,
                State = branch.State,
                TenantId = branch.TenantId
            };
        }
    }
}
