using Microsoft.EntityFrameworkCore;
using SchoolManagement.Model;

namespace NeuroPi.UserManagment.Data
{
    public class SchoolManagementDb : DbContext
    {
        public SchoolManagementDb(DbContextOptions<SchoolManagementDb> options) : base(options) { }

        public DbSet<MContact> Contacts { get; set; }
        public DbSet<MInstitution> Institutions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MContact>(entity =>
            {
                entity.ToTable("contact");
                entity.HasIndex(e => e.PriNumber).IsUnique();
                entity.HasCheckConstraint("CK_Contact_PriSecNumberDiff",
                    @"""pri_number"" <> ""sec_number"" OR ""sec_number"" IS NULL");
            });

            modelBuilder.Entity<MInstitution>(entity =>
            {
                entity.ToTable("institution");
                entity.HasIndex(e => e.Name).IsUnique();
            });
        }
    }
}