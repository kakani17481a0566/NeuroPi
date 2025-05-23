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

        // Remove DbSet<MTenant> since it's managed by NeuroPi

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Configure Contact
        //    modelBuilder.Entity<MContact>(entity =>
        //    {
        //        entity.ToTable("contact");
        //        entity.HasIndex(e => e.PriNumber).IsUnique();
        //        entity.HasCheckConstraint("CK_Contact_PriSecNumberDiff",
        //            "pri_number <> sec_number OR sec_number IS NULL");

        //        // Configure tenant relationship without navigation property
        //        entity.HasOne(typeof(MTenant))
        //            .WithMany()
        //            .HasForeignKey("TenantId")
        //            .OnDelete(DeleteBehavior.Restrict);
        //    });

            // Configure Institution
            //modelBuilder.Entity<MInstitution>(entity =>
            //{
            //    entity.ToTable("institution");
            //    entity.HasIndex(e => e.Name).IsUnique();

            //    // Configure tenant relationship without navigation property
            //    entity.HasOne(typeof(MTenant))
            //        .WithMany()
            //        .HasForeignKey("TenantId")
            //        .OnDelete(DeleteBehavior.Restrict);

            //    entity.HasOne(i => i.Contact)
            //        .WithMany(c => c.Institutions)
            //        .HasForeignKey(i => i.ContactId)
            //        .OnDelete(DeleteBehavior.SetNull);
            //});
        //}
    }
}