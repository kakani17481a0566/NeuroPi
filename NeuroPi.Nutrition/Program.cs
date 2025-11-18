using Microsoft.EntityFrameworkCore;
using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Interface;
using NeuroPi.Nutrition.Services.Implementation;
using NeuroPi.Nutrition.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB Context
builder.Services.AddDbContext<NeutritionDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services
builder.Services.AddScoped<IGenGenesService, GenGenesServiceImpl>();
builder.Services.AddScoped<IVitamins, VitaminServiceImpl>();
builder.Services.AddScoped<IMealType, MealTypeServiceImpl>();
builder.Services.AddScoped<INutritionMasterType, NutritionMasterTypeServiceImpl>();
builder.Services.AddScoped<INutritionMaster, NutritionMasterServiceImpl>();
builder.Services.AddScoped<IUserFavourites, UserFavouritesServiceImpl>();
builder.Services.AddScoped<INutritionalIteamType, NutritionalIteamTypeServicesImpl>();
builder.Services.AddScoped<INutritionalItem, NutritionalItemServiceImpl>();
builder.Services.AddScoped<IGeneNutritionalFocus, GeneNutritionalFocusServiceImpl>();
builder.Services.AddScoped<INutritionalFocus, NutritionalFocusServiceImpl>();
builder.Services.AddScoped<IMealPlan, MealPlanServiceImpl>();
builder.Services.AddScoped<INutritionalItemVitamins, NutritionalItemVitamins>();
builder.Services.AddScoped<IUnplannedMeal, UnplannedMealServiceImpl>();
builder.Services.AddScoped<INutritionalItemMealType, NutritionalItemMealTypeServiceImpl>();
builder.Services.AddScoped<IUserMealType, UserMealTypeServiceImpl>();
builder.Services.AddScoped<IRecipesInstructions, RecipesInstructionServiceImpl>();
builder.Services.AddScoped<IMealPlanMonitoring, MealPlanMonitoringServiceImpl>();
builder.Services.AddScoped<IUserGene, UserGeneServiceImpl>();

// --------------------------------------
// ✅ CORS POLICY
// --------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// --------------------------------------
// 🚀 IMPORTANT: Enable CORS here
// --------------------------------------
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
