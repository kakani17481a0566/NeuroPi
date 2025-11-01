using Microsoft.EntityFrameworkCore;
using NeuroPi.Nutrition.Model;


namespace NeuroPi.Nutrition.Data
{
    public class NeutritionDbContext : DbContext
    {
        public NeutritionDbContext(DbContextOptions<NeutritionDbContext> options) : base(options) { }

        public DbSet<MGenGenes> GenGenes { get; set;}

}
}
