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

     
    }
}