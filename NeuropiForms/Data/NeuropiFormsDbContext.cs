using Microsoft.EntityFrameworkCore;

namespace NeuropiForms.Data
{
    public class NeuropiFormsDbContext : DbContext
    {
        public NeuropiFormsDbContext(DbContextOptions<NeuropiFormsDbContext> options) : base(options)
        {
        }
    }
}
