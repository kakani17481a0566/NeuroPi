using Microsoft.EntityFrameworkCore;
using NeuroPi.Data;
using NeuroPi.Models;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.Tenent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeuroPi.Services.Implementation
{
    public class TenantServiceImpl : ITenantService
    {
        private readonly NeuroPiDbContext _context;

        public TenantServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        // Get all tenants and map them to TenantViewModel
        public async Task<List<TenantViewModel>> GetAllTenantsAsync()
        {
            try
            {
                return await _context.Tenants
                    .Select(t => new TenantViewModel
                    {
                        TenantId = t.TenantId,
                        Name = t.Name,
                        CreatedOn = t.CreatedOn,
                        UpdatedOn = t.UpdatedOn
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the tenants.", ex);
            }
        }

        // Get a tenant by its ID and map it to TenantViewModel
        public async Task<TenantViewModel> GetTenantByIdAsync(int id)
        {
            try
            {
                var tenant = await _context.Tenants
                    .Where(t => t.TenantId == id)
                    .Select(t => new TenantViewModel
                    {
                        TenantId = t.TenantId,
                        Name = t.Name,
                        CreatedOn = t.CreatedOn,
                        UpdatedOn = t.UpdatedOn
                    })
                    .FirstOrDefaultAsync();

                if (tenant == null)
                {
                    throw new KeyNotFoundException($"Tenant with ID {id} not found.");
                }

                return tenant;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the tenant.", ex);
            }
        }

        // Create a new tenant and map it to TenantViewModel
        public async Task<TenantViewModel> CreateTenantAsync(TenantInputModel input)
        {
            try
            {
                // Validate that the 'CreatedBy' user exists
                bool userExists = await _context.Users.AnyAsync(u => u.UserId == input.CreatedBy);
                if (!userExists)
                {
                    throw new InvalidOperationException("User does not exist.");
                }

                var tenant = new MTenant
                {
                    Name = input.Name,
                    CreatedBy = input.CreatedBy,
                    CreatedOn = DateTime.UtcNow
                };

                _context.Tenants.Add(tenant);
                await _context.SaveChangesAsync();

                return new TenantViewModel
                {
                    TenantId = tenant.TenantId,
                    Name = tenant.Name,
                    CreatedOn = tenant.CreatedOn
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the tenant.", ex);
            }
        }

        // Update an existing tenant and map it to TenantViewModel
        public async Task<TenantViewModel> UpdateTenantAsync(int id, TenantUpdateInputModel input)
        {
            try
            {
                // Validate if the 'UpdatedBy' user exists
                bool userExists = await _context.Users.AnyAsync(u => u.UserId == input.UpdatedBy);
                if (!userExists)
                {
                    throw new InvalidOperationException($"User with ID {input.UpdatedBy} does not exist.");
                }

                var existingTenant = await _context.Tenants.FindAsync(id);
                if (existingTenant == null)
                {
                    throw new KeyNotFoundException($"Tenant with ID {id} not found.");
                }

                // Update the tenant's properties
                existingTenant.Name = input.Name;
                existingTenant.UpdatedBy = input.UpdatedBy;
                existingTenant.UpdatedOn = DateTime.UtcNow;

                // Apply the changes and save to the database
                _context.Tenants.Update(existingTenant);
                await _context.SaveChangesAsync();

                return new TenantViewModel
                {
                    TenantId = existingTenant.TenantId,
                    Name = existingTenant.Name,
                    CreatedOn = existingTenant.CreatedOn,
                    UpdatedOn = existingTenant.UpdatedOn
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating tenant: {ex.Message}", ex);
            }
        }

        // Delete a tenant and return true if successful
        public async Task<bool> DeleteTenantAsync(int id)
        {
            try
            {
                var tenant = await _context.Tenants.FindAsync(id);
                if (tenant == null)
                {
                    throw new KeyNotFoundException($"Tenant with ID {id} not found.");
                }

                _context.Tenants.Remove(tenant);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the tenant.", ex);
            }
        }
    }
}
