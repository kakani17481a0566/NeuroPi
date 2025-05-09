using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NeuroPi.Model;
using NeuroPi.Models;

namespace NeuroPi.Data
{
    public class NeuroPiDbContext : DbContext
    {
        public NeuroPiDbContext(DbContextOptions<NeuroPiDbContext> options) : base(options) { }

        public DbSet<MTenant> Tenants { get; set; }
        public DbSet<MUser> Users { get; set; }
        public DbSet<MOrganization> Organizations { get; set; }
        public DbSet<MDepartment> Departments { get; set; }
        public DbSet<MRole> Roles { get; set; }
        public DbSet<MPermission> Permissions { get; set; }
        public DbSet<MRolePermission> RolePermissions { get; set; }
        public DbSet<MUserRole> UserRoles { get; set; }
        public DbSet<MTeam> Teams { get; set; }
        public DbSet<MTeamUser> TeamUsers { get; set; }
        public DbSet<MGroup> Groups { get; set; }
        public DbSet<MGroupUser> GroupUsers { get; set; }
        public DbSet<MUserDepartment> UserDepartments { get; set; }
        public DbSet<MAuditLog> AuditLogs { get; set; }
        public DbSet<MConfig> Configs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User-Tenant relationship (MTenant to MUser, which is the reverse relationship)
            modelBuilder.Entity<MUser>()
                .HasOne(user => user.Tenant)
                .WithMany(tenant => tenant.Users) // Ensuring MTenant has a collection of MUser
                .HasForeignKey(user => user.TenantId)
                .OnDelete(DeleteBehavior.Restrict); // Optionally prevent cascading deletes

            // Composite indexes
            modelBuilder.Entity<MRolePermission>()
                .HasIndex(rp => new { rp.RoleId, rp.PermissionId })
                .IsUnique();

            modelBuilder.Entity<MUserRole>()
                .HasIndex(ur => new { ur.UserId, ur.RoleId })
                .IsUnique();

            modelBuilder.Entity<MTeamUser>()
                .HasIndex(tu => new { tu.TeamId, tu.UserId })
                .IsUnique();

            modelBuilder.Entity<MGroupUser>()
                .HasIndex(gu => new { gu.GroupId, gu.UserId })
                .IsUnique();

            modelBuilder.Entity<MUserDepartment>()
                .HasIndex(ud => new { ud.UserId, ud.DepartmentId })
                .IsUnique();
        }
    }
}
