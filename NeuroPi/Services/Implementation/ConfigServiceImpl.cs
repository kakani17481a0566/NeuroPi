using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Config;
using Microsoft.EntityFrameworkCore;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class ConfigServiceImpl : IConfigService
    {
        private readonly NeuroPiDbContext _context;

        public ConfigServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        // Get all non-deleted configurations
        public List<ConfigVM> GetAllConfigs()
        {
            var configs = _context.Configs
                .Where(x => !x.IsDeleted)
                .Include(x => x.Tenant)
                .Select(c => new ConfigVM
                {
                    ConfigId = c.ConfigId,
                    TenantId = c.TenantId,
                    DbType = c.DbType,
                    ConnectionString = c.ConnectionString,
                    DbHost = c.DbHost,
                    DbPort = c.DbPort,
                    DbName = c.DbName,
                    DbUsername = c.DbUsername,
                    DbPassword = c.DbPassword
                }).ToList();

            return configs;
        }

        // Get configuration by ID
        public ConfigVM GetConfigById(int id)
        {
            var config = _context.Configs
                .Where(x => x.ConfigId == id && !x.IsDeleted)
                .Include(x => x.Tenant)
                .FirstOrDefault();

            if (config == null) return null;

            return new ConfigVM
            {
                ConfigId = config.ConfigId,
                TenantId = config.TenantId,
                DbType = config.DbType,
                ConnectionString = config.ConnectionString,
                DbHost = config.DbHost,
                DbPort = config.DbPort,
                DbName = config.DbName,
                DbUsername = config.DbUsername,
                DbPassword = config.DbPassword
            };
        }

        // Get configuration by ID and TenantId
        public ConfigVM GetConfigByIdAndTenant(int id, int tenantId)
        {
            var config = _context.Configs
                .Where(x => x.ConfigId == id && x.TenantId == tenantId && !x.IsDeleted)
                .FirstOrDefault();

            if (config == null) return null;

            return new ConfigVM
            {
                ConfigId = config.ConfigId,
                TenantId = config.TenantId,
                DbType = config.DbType,
                ConnectionString = config.ConnectionString,
                DbHost = config.DbHost,
                DbPort = config.DbPort,
                DbName = config.DbName,
                DbUsername = config.DbUsername,
                DbPassword = config.DbPassword
            };
        }

        // Get all configurations for a specific tenantId
        public List<ConfigVM> GetConfigsByTenantId(int tenantId)
        {
            var configs = _context.Configs
                .Where(x => x.TenantId == tenantId && !x.IsDeleted)
                .Include(x => x.Tenant)
                .Select(c => new ConfigVM
                {
                    ConfigId = c.ConfigId,
                    TenantId = c.TenantId,
                    DbType = c.DbType,
                    ConnectionString = c.ConnectionString,
                    DbHost = c.DbHost,
                    DbPort = c.DbPort,
                    DbName = c.DbName,
                    DbUsername = c.DbUsername,
                    DbPassword = c.DbPassword
                }).ToList();

            return configs;
        }

        // Create new configuration
        public ConfigVM CreateConfig(ConfigCreateVM config)
        {
            var entity = new MConfig
            {
                TenantId = config.TenantId,
                DbType = config.DbType,
                ConnectionString = config.ConnectionString,
                DbHost = config.DbHost,
                DbPort = config.DbPort,
                DbName = config.DbName,
                DbUsername = config.DbUsername,
                DbPassword = config.DbPassword,
                CreatedBy = config.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.Configs.Add(entity);
            _context.SaveChanges();

            return new ConfigVM
            {
                ConfigId = entity.ConfigId,
                TenantId = entity.TenantId,
                DbType = entity.DbType,
                ConnectionString = entity.ConnectionString,
                DbHost = entity.DbHost,
                DbPort = entity.DbPort,
                DbName = entity.DbName,
                DbUsername = entity.DbUsername,
                DbPassword = entity.DbPassword
            };
        }

        // Update existing configuration
        public ConfigVM UpdateConfig(int id, ConfigUpdateVM config)
        {
            var existingConfig = _context.Configs
                .Where(x => x.ConfigId == id && !x.IsDeleted)
                .FirstOrDefault();

            if (existingConfig == null)
                return null;

            existingConfig.DbType = config.DbType;
            existingConfig.ConnectionString = config.ConnectionString;
            existingConfig.DbHost = config.DbHost;
            existingConfig.DbPort = config.DbPort;
            existingConfig.DbName = config.DbName;
            existingConfig.DbUsername = config.DbUsername;
            existingConfig.DbPassword = config.DbPassword;
            existingConfig.UpdatedBy = config.UpdatedBy;
            existingConfig.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();

            return new ConfigVM
            {
                ConfigId = existingConfig.ConfigId,
                TenantId = existingConfig.TenantId,
                DbType = existingConfig.DbType,
                ConnectionString = existingConfig.ConnectionString,
                DbHost = existingConfig.DbHost,
                DbPort = existingConfig.DbPort,
                DbName = existingConfig.DbName,
                DbUsername = existingConfig.DbUsername,
                DbPassword = existingConfig.DbPassword
            };
        }

        // Soft delete a configuration
        public bool DeleteConfig(int id, int tenantId)
        {
            var existingConfig = _context.Configs
                .Where(x => x.ConfigId == id && x.TenantId == tenantId && !x.IsDeleted)
                .FirstOrDefault();

            if (existingConfig == null)
                return false;

            existingConfig.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }
    }
}
