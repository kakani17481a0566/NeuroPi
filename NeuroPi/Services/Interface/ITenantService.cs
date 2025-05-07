using NeuroPi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuroPi.Services.Interface
{
    public interface ITenantService
    {
        Task<List<MTenant>> GetAllTenantsAsync();
    }
}
