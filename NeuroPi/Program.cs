using Microsoft.EntityFrameworkCore;
using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Services.Implementation;
using NeuroPi.UserManagment.Services.Interface;

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
builder.Services.AddTransient<IRolePermissionService, RolePermissionServiceImpl>();
builder.Services.AddScoped<ITeamService, TeamServiceImpl>();
builder.Services.AddScoped<ITeamService, TeamServiceImpl>();
builder.Services.AddTransient<ITeamUserService, TeamUserServiceImpl>();
builder.Services.AddTransient<IPermissionService, PermissionServiceImpl>();
builder.Services.AddTransient<IRoleService, RoleServiceImpl>();





//builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<IUserRolesService, UserRolesServiceImpl>();





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