using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Services.Implementation;
using NeuroPi.UserManagment.Services.Interface;
using CloudinaryDotNet;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------
// Load Configuration Files
// -------------------------------------------
builder.Configuration
    .AddJsonFile("neuro_appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"neuro_appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// -------------------------------------------
// Cloudinary Configuration
// -------------------------------------------
var cloudinary = new Cloudinary(new Account(
    builder.Configuration["Cloudinary:CloudName"],
    builder.Configuration["Cloudinary:ApiKey"],
    builder.Configuration["Cloudinary:ApiSecret"]
));
cloudinary.Api.Secure = true;
builder.Services.AddSingleton(cloudinary);

// -------------------------------------------
// Add Services to Container
// -------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// -------------------------------------------
// Swagger Configuration with JWT
// -------------------------------------------
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NeuroPi.UserManagment",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
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
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// -------------------------------------------
// CORS Policy (Dev Only)
// -------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// -------------------------------------------
// JWT Authentication
// -------------------------------------------
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// -------------------------------------------
// Register EF DbContext
// -------------------------------------------
builder.Services.AddDbContext<NeuroPiDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// -------------------------------------------
// Register Application Services
// -------------------------------------------
builder.Services.AddScoped<ITenantService, TenantServiceImpl>();
builder.Services.AddScoped<IDepartmentService, DepartmentServiceImpl>();
builder.Services.AddScoped<IGroupService, GroupServiceImpl>();
builder.Services.AddScoped<IOrganizationService, OrganizationImpl>();
builder.Services.AddScoped<IGroupUserService, GroupUserServiceImpl>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionServiceImpl>();
builder.Services.AddScoped<ITeamService, TeamServiceImpl>();
builder.Services.AddScoped<IRoleService, RoleServiceImpl>();
builder.Services.AddScoped<ITeamUserService, TeamUserServiceImpl>();
builder.Services.AddScoped<IPermissionService, PermissionServiceImpl>();
builder.Services.AddScoped<IUserDepartmentService, UserDepartmentServiceImpl>();
builder.Services.AddScoped<IConfigService, ConfigServiceImpl>();
builder.Services.AddScoped<IUserRolesService, UserRolesServiceImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();

// -------------------------------------------
// Build App
// -------------------------------------------
var app = builder.Build();

// -------------------------------------------
// Middleware Pipeline
// -------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
