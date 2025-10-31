using Microsoft.EntityFrameworkCore;
using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Implementation;
using NeuroPi.Nutrition.Services.Interface;
using SchoolManagement.Data;
using SchoolManagement.Services.Implementation;
using SchoolManagement.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add controllers, swagger, etc.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Register BOTH DbContexts with the SAME PostgreSQL connection string
builder.Services.AddDbContext<NeutritionDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<SchoolManagementDb>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Register all scoped services
builder.Services.AddScoped<IGenGenesService, GenGenesServiceImpl>();
builder.Services.AddScoped<ITestResultService, TestResultServiceImpl>();

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
