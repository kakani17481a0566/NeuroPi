using Microsoft.EntityFrameworkCore;
using NeuroPi.Data;
using NeuroPi.Services.Interface;
using NeuroPi.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register application services
builder.Services.AddScoped<ITenantService, TenantServiceImpl>();

builder.Services.AddTransient<IDepartmentService, DepartmentServiceImpl>();
builder.Services.AddScoped<IGroupService, GroupServiceImpl>();
builder.Services.AddScoped<IOrganizationService, OrganizationImpl>();
builder.Services.AddTransient<IGroupUserService, GroupUserServiceImpl>();

builder.Services.AddTransient<IRolePermisisionService, RolePermissionServiceImpl>();
builder.Services.AddScoped<ITeamService, TeamService>();



builder.Services.AddScoped<IUserService, UserServiceImpl>();




builder.Services.AddDbContext<NeuroPiDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();