using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Services.Implementation;
using NeuroPi.UserManagment.Services.Interface;
using SchoolManagement.Data;
using SchoolManagement.Services.Implementation;
using SchoolManagement.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// === Register Services ===
// Example registrations — add all your AddScoped registrations here
builder.Services.AddScoped<ITenantService, TenantServiceImpl>();
// ... (other AddScoped registrations)

// Cloudinary
var cloudinary = new Cloudinary(new Account(
    builder.Configuration["Cloudinary:CloudName"],
    builder.Configuration["Cloudinary:ApiKey"],
    builder.Configuration["Cloudinary:ApiSecret"]
));
cloudinary.Api.Secure = true;
builder.Services.AddSingleton(cloudinary);

// DbContexts
builder.Services.AddDbContext<NeuroPiDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<SchoolManagementDb>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger + JWT
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "NeuroPi.UserManagment", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                        Enter 'Bearer' [space] and then your token.
                        Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

// CORS: Allow all origins, methods, headers (for local/dev)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// === Build App ===
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Always place CORS before controllers!
app.UseHttpsRedirection();
app.UseCors("AllowAll");    // <--- Enable CORS globally

// app.UseAuthentication(); // Uncomment if you use authentication
app.UseAuthorization();

app.MapControllers();

app.Run();
