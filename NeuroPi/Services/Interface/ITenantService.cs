using NeuroPi.ViewModel.Tenent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuroPi.Services.Interface
{
    public interface ITenantService
    {
        Task<List<TenantViewModel>> GetAllTenantsAsync();
        Task<TenantViewModel> GetTenantByIdAsync(int id);
        Task<TenantViewModel> CreateTenantAsync(TenantInputModel input);
        Task<TenantViewModel> UpdateTenantAsync(int id, TenantUpdateInputModel input);
        Task<bool> DeleteTenantAsync(int id);
    }
}
