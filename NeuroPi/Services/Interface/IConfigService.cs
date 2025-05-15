using NeuroPi.UserManagment.ViewModel.Config;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IConfigService
    {
        List<ConfigVM> GetAllConfigs();
        ConfigVM GetConfigById(int id);
        ConfigVM GetConfigByIdAndTenant(int id, int tenantId);
        List<ConfigVM> GetConfigsByTenantId(int tenantId);  
        ConfigVM CreateConfig(ConfigCreateVM config);
        ConfigVM UpdateConfig(int id, ConfigUpdateVM config);
        bool DeleteConfig(int id, int tenantId);
    }
}
