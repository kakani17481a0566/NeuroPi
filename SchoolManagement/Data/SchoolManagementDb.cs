using Microsoft.EntityFrameworkCore;


namespace NeuroPi.UserManagment.Data
{
    public class SchoolManagementDb : DbContext
    {
        public SchoolManagementDb(DbContextOptions<SchoolManagementDb> options) : base(options) { }

      
    }
}
