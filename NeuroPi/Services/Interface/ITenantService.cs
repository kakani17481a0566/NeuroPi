using NeuroPi.ViewModel.Tenent;
using System.Collections.Generic;

namespace NeuroPi.Services.Interface
{
    public interface ITenantService
    {
        List<TenantVM> GetAllTenants();
        TenantVM GetTenantById(int id);
        TenantVM CreateTenant(TenantInputVM input);
        TenantVM UpdateTenant(int id, TenantUpdateInputVM input);
        bool DeleteTenant(int id);
    }
}