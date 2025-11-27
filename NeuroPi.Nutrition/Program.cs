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
builder.Services.AddScoped<IUserFeedback, UserFeedbackServiceImpl>();
builder.Services.AddScoped<ITimetable, TimetableServiceImpl>();



// --------------------------------------
// 🌐 CORS
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

// --------------------------------------
// 🚀 Swagger ALWAYS ON (Dev + Prod)
// --------------------------------------
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "NeuroPi API V1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

// --------------------------------------
// 🌐 Enable CORS
// --------------------------------------
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
