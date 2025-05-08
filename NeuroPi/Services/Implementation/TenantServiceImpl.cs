using NeuroPi.Data;
using NeuroPi.Models;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.Tenent;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuroPi.Services.Implementation
{
    public class TenantServiceImpl : ITenantService
    {
        private readonly NeuroPiDbContext _context;

        public TenantServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        public List<TenantViewModel> GetAllTenants()
        {
            return _context.Tenants
                .Select(t => new TenantViewModel
                {
                    TenantId = t.TenantId,
                    Name = t.Name,
                    CreatedOn = t.CreatedOn,
                    UpdatedOn = t.UpdatedOn
                })
                .ToList();
        }

        public TenantViewModel GetTenantById(int id)
        {
            var tenant = _context.Tenants
                .Where(t => t.TenantId == id)
                .Select(t => new TenantViewModel
                {
                    TenantId = t.TenantId,
                    Name = t.Name,
                    CreatedOn = t.CreatedOn,
                    UpdatedOn = t.UpdatedOn
                })
                .FirstOrDefault();

            return tenant;
        }

        public TenantViewModel CreateTenant(TenantInputModel input)
        {
            var tenant = new MTenant
            {
                Name = input.Name,
                CreatedBy = input.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };

            _context.Tenants.Add(tenant);
            _context.SaveChanges();

            return new TenantViewModel
            {
                TenantId = tenant.TenantId,
                Name = tenant.Name,
                CreatedOn = tenant.CreatedOn
            };
        }

        public TenantViewModel UpdateTenant(int id, TenantUpdateInputModel input)
        {
            var existingTenant = _context.Tenants.Find(id);
            if (existingTenant == null)
                return null;

            existingTenant.Name = input.Name;
            existingTenant.UpdatedBy = input.UpdatedBy;
            existingTenant.UpdatedOn = DateTime.UtcNow;

            _context.Tenants.Update(existingTenant);
            _context.SaveChanges();

            return new TenantViewModel
            {
                TenantId = existingTenant.TenantId,
                Name = existingTenant.Name,
                CreatedOn = existingTenant.CreatedOn,
                UpdatedOn = existingTenant.UpdatedOn
            };
        }

        public bool DeleteTenant(int id)
        {
            var tenant = _context.Tenants.Find(id);
            if (tenant == null)
                return false;

            _context.Tenants.Remove(tenant);
            _context.SaveChanges();

            return true;
        }
    }
}