using Microsoft.EntityFrameworkCore;
using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Implementation;
using NeuroPi.Nutrition.Services.Interface;


var builder = WebApplication.CreateBuilder(args);

// ✅ Add controllers, swagger, etc.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Register BOTH DbContexts with the SAME PostgreSQL connection string
builder.Services.AddDbContext<NeutritionDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));



// ✅ Register all scoped services
builder.Services.AddScoped<IGenGenesService, GenGenesServiceImpl>();
builder.Services.AddScoped<IVitamins, VitaminServiceImpl>();
builder.Services.AddScoped<IMealType, MealTypeServiceImpl>();
builder.Services.AddScoped<INutritionMasterType, NutritionMasterTypeServiceImpl>();
builder.Services.AddScoped<INutritionMaster, NutritionMasterServiceImpl>();


var app = builder.Build();

// ✅ Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
