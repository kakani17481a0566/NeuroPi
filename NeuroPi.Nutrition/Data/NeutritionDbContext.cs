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

<<<<<<< Updated upstream
        public DbSet<MNutritionalIteamType> NutritionalIteamType { get; set; }

        

=======
        public DbSet<MVitamins> Vitamins { get; set; }

        public DbSet<MMealType> MealTypes { get; set; }
>>>>>>> Stashed changes


    }
}
