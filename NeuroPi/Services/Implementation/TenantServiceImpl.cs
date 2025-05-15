using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Tenent;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class TenantServiceImpl : ITenantService
    {
        private readonly NeuroPiDbContext _context;

        public TenantServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

    
        public List<TenantVM> GetAllTenants()
        {
            return _context.Tenants
                .Where(t => !t.IsDeleted)
                .Select(t => new TenantVM
                {
                    TenantId = t.TenantId,
                    Name = t.Name,
                    CreatedOn = t.CreatedOn,
                    UpdatedOn = t.UpdatedOn
                })
                .ToList();
        }

        
        public TenantVM GetTenantById(int id)
        {
            var tenant = _context.Tenants
                .Where(t => t.TenantId == id && !t.IsDeleted)
                .Select(t => new TenantVM
                {
                    TenantId = t.TenantId,
                    Name = t.Name,
                    CreatedOn = t.CreatedOn,
                    UpdatedOn = t.UpdatedOn
                })
                .FirstOrDefault();

            return tenant;
        }

     
        public TenantVM CreateTenant(TenantInputVM input)
        {
            var tenant = new MTenant
            {
                Name = input.Name,
                CreatedBy = input.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.Tenants.Add(tenant);
            _context.SaveChanges();

            return new TenantVM
            {
                TenantId = tenant.TenantId,
                Name = tenant.Name,
                CreatedOn = tenant.CreatedOn
            };
        }

      
        public TenantVM UpdateTenant(int id, TenantUpdateInputVM input)
        {
            var existingTenant = _context.Tenants.FirstOrDefault(t => t.TenantId == id && !t.IsDeleted);
            if (existingTenant == null)
                return null;

            existingTenant.Name = input.Name;
            existingTenant.UpdatedBy = input.UpdatedBy;
            existingTenant.UpdatedOn = DateTime.UtcNow;

            _context.Tenants.Update(existingTenant);
            _context.SaveChanges();

            return new  TenantVM
            {
                TenantId = existingTenant.TenantId,
                Name = existingTenant.Name,
                CreatedOn = existingTenant.CreatedOn,
                UpdatedOn = existingTenant.UpdatedOn
            };
        }

        
        public bool DeleteTenant(int id)
        {
            var tenant = _context.Tenants.FirstOrDefault(t => t.TenantId == id && !t.IsDeleted);
            if (tenant == null)
                return false;

            tenant.IsDeleted = true;
            _context.SaveChanges();

            return true;
        }
    }
}
