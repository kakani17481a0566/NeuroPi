using Microsoft.EntityFrameworkCore;
using NeuroPi.Data;
using NeuroPi.Models;
using NeuroPi.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuroPi.Services.Implementation
{
    public class TenantService : ITenantService
    {
        private readonly NeuroPiDbContext _context;

        public TenantService(NeuroPiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MTenant>> GetAllTenantsAsync()
        {
            return await _context.Tenants.ToListAsync();
        }
    }
}
