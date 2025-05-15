namespace NeuroPi.UserManagment.ViewModel.Config
{
    public class ConfigVM
    {
        public int ConfigId { get; set; }
        public int TenantId { get; set; } 
        public string DbType { get; set; }
        public string? ConnectionString { get; set; }
        public string? DbHost { get; set; }
        public int? DbPort { get; set; }
        public string? DbName { get; set; }
        public string? DbUsername { get; set; }
        public string? DbPassword { get; set; }
    }
}
