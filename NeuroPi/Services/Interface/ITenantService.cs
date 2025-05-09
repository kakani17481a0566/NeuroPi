using NeuroPi.ViewModel.Tenent;
using System.Collections.Generic;

namespace NeuroPi.Services.Interface
{
    public interface ITenantService
    {
        List<TenantViewModel> GetAllTenants();
        TenantViewModel GetTenantById(int id);
        TenantViewModel CreateTenant(TenantInputModel input);
        TenantViewModel UpdateTenant(int id, TenantUpdateInputModel input);
        bool DeleteTenant(int id);
    }
}