using Microsoft.EntityFrameworkCore;
using NeuroPi.Nutrition.Model;


namespace NeuroPi.Nutrition.Data
{
    public class NeutritionDbContext : DbContext
    {
        public NeutritionDbContext(DbContextOptions<NeutritionDbContext> options) : base(options) { }

        public DbSet<MGenGenes> GenGenes { get; set;}
        public DbSet<MNutritionalFocus>NutritionalFocuses { get; set; }
        public DbSet<MGeneNutritionalFocus> GeneNutritionalFocus { get; set; }

        public DbSet<MNutritionalIteamType> NutritionalIteamType { get; set; }

        



    }
}
