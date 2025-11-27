using Microsoft.EntityFrameworkCore;
using NeuroPi.CommonLib.Model.Nutrition;
using NeuroPi.Nutrition.Model;
using System.Reflection.Emit;


namespace NeuroPi.Nutrition.Data
{
    public class NeutritionDbContext : DbContext
    {
        public NeutritionDbContext(DbContextOptions<NeutritionDbContext> options) : base(options) { }

        public DbSet<MGenGenes> GenGenes { get; set; }
        public DbSet<MNutritionalFocus> NutritionalFocuses { get; set; }
        public DbSet<MGeneNutritionalFocus> GeneNutritionalFocus { get; set; }

        public DbSet<MNutritionalItemType> NutritionalIteamType { get; set; }

        public DbSet<MVitamins> Vitamins { get; set; }

        public DbSet<MMealType> MealTypes { get; set; }

        public DbSet<MNutritionMasterType> NutritionMasterTypes { get; set; }

        public DbSet<MNutritionMaster> NutritionMasters { get; set; }

        public DbSet<MUserFavourites> UserFavourites { get; set; }

        public DbSet<MNutritionalItem> NutritionalItems { get; set; }

        public DbSet<MNutritionalItemVitamins> NutritionalItemVitamins { get; set; }


        public DbSet<MMealPlan> MealPlan { get; set; }

        public DbSet<MUnplannedMeal> UnplannedMeals { get; set; }
        public DbSet<MNutritionalItemMealType> NutritionalItemMealType { get; set; }

        public DbSet<MUserMealType> UserMealTypes { get; set; }

        public DbSet<MRecipesInstructions> RecipesInstructions { get; set; }

        public DbSet<MMealPlanMonitoring> MealPlanMonitoring { get; set; }
        public DbSet<MNutritionalItemRecipe> NutritionalItemRecipe { get; set; }

        public DbSet<MUserGene> UserGene { get; set; }

        public DbSet<MNutritionalFocusItem> NutritionalFocusItem { get; set; }



        public DbSet<MUserFeedback> UserFeedback { get; set; }


        public DbSet<MResourceType> ResourceTypes { get; set; }
        public DbSet<MResourceMaster> ResourceMasters { get; set; }
        public DbSet<MResourceInstruction> ResourceInstructions { get; set; }
        public DbSet<MTimetable> Timetables { get; set; }
        public DbSet<MUserGamesStatus> UserGamesStatuses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MGeneNutritionalFocus>()
                .HasKey(gf => gf.Id);
            modelBuilder.Entity<MGeneNutritionalFocus>()
                .HasOne(gf => gf.Genes)
                .WithMany(g => g.NutritionalFocus)
                .HasForeignKey(gf => gf.GenesId);
            modelBuilder.Entity<MGeneNutritionalFocus>()
                .HasOne(gf => gf.NutritionalFocus)
                .WithMany(nf => nf.NutritionalFocus)
                .HasForeignKey(gf => gf.NutritionalFocusId);

            modelBuilder.Entity<MNutritionalItem>()
                .HasOne(x => x.DietType)
                .WithMany()
                .HasForeignKey(x => x.DietTypeId);

        }

        





    }
}
