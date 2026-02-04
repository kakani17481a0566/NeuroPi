using Microsoft.EntityFrameworkCore;
using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.ViewModel.User;

namespace NeuroPi.UserManagment.Data
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
        public DbSet<MMainMenu> MainMenus { get; set; }
        public DbSet<MMenu> Menu { get; set; }








        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User ↔ Tenant
            modelBuilder.Entity<MUser>()
                .HasOne(user => user.Tenant)
                .WithMany(tenant => tenant.Users)
                .HasForeignKey(user => user.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            // Composite indexes
            modelBuilder.Entity<MRolePermission>()
                .HasIndex(rp => new { rp.RoleId, rp.MenuId })
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

            // ✅ User ↔ UserRole
            modelBuilder.Entity<MUser>()
                .HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ Role ↔ UserRole
            modelBuilder.Entity<MRole>()
                .HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<MMenu>()
                 .HasOne(m => m.MainMenu)
                 .WithMany(m => m.Menus)
                 .HasForeignKey(m => m.MainMenuId);


            modelBuilder.Entity<UsersProfileSummaryVM>().HasNoKey();

        }



    }
}
