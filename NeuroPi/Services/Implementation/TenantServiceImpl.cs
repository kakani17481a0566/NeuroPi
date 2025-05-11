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

        public List<TenantVM> GetAllTenants()
        {
            return _context.Tenants
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
                .Where(t => t.TenantId == id)
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
                CreatedOn = DateTime.UtcNow
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
            var existingTenant = _context.Tenants.Find(id);
            if (existingTenant == null)
                return null;

            existingTenant.Name = input.Name;
            existingTenant.UpdatedBy = input.UpdatedBy;
            existingTenant.UpdatedOn = DateTime.UtcNow;

            _context.Tenants.Update(existingTenant);
            _context.SaveChanges();

            return new TenantVM
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