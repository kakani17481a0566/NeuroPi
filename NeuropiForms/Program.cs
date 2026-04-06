using Microsoft.EntityFrameworkCore;
using NeuropiForms.Data;
using NeuropiForms.Services.Implementation;
using NeuropiForms.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<NeuropiForms.Data.NeuropiFormsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<NeuropiForms.Services.Interface.IFormService, NeuropiForms.Services.Impl.FormServiceImpl>();
builder.Services.AddScoped<IFormSubmissionService, FormSubmissionServiceImpl>();
builder.Services.AddScoped<ISubmissionFieldValueService, SubmissionFieldValueServiceImpl>();
builder.Services.AddScoped<ISubmissionSectionValueService, SubmissionSectionValueServiceImpl>();



builder.Services.AddControllers();
builder.Services.AddScoped<ISectionService, SectionServiceImpl>();
builder.Services.AddScoped<IFieldService, FieldServiceImpl>();
builder.Services.AddScoped<ISectionFieldService, SectionFieldServiceImpl>();
builder.Services.AddScoped<ISectionGroupService, SectionGroupServiceImpl>();


// Explicitly bind to Azure's PORT environment variable (defaults to 8080 on Linux App Service)
var port = int.Parse(Environment.GetEnvironmentVariable("PORT") ?? "8080");
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();


// Always enable Swagger — required on Azure since ASPNETCORE_ENVIRONMENT=Development is set via app settings
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "NeuroPiForms API v1");
    c.RoutePrefix = "swagger";
});

// UseHttpsRedirection disabled — causes redirect loops on Azure App Service Linux free tier
// app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
