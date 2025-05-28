using Microsoft.EntityFrameworkCore;
using SchoolManagement.Model;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Data
{
    public class SchoolManagementDb : DbContext
    {
        public SchoolManagementDb(DbContextOptions<SchoolManagementDb> options) : base(options) { }

        public DbSet<MContact> Contacts { get; set; }
        public DbSet<MInstitution> Institutions { get; set; }
        public DbSet<MMasterType> MasterTypes { get; set; }
        public DbSet<MMaster> Masters { get; set; }
        public DbSet<MItemHeader> ItemHeaders { get; set; }
        public DbSet<MItem> Items { get; set; }
        public DbSet<MLibBookRecord> LibBookRecords { get; set; }
        public DbSet<MBook> Books { get; set; }
        public DbSet<MAccount> Accounts { get; set; }
        public DbSet<MTransaction> Transactions { get; set; }
        public DbSet<MPrefixSuffix> PrefixSuffix { get; set; }
        public DbSet<MUtilitiesList> UtilitiesList { get; set; }
    }
}
