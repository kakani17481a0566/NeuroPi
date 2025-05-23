using Microsoft.EntityFrameworkCore;
using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Services.Implementation;
using NeuroPi.UserManagment.Services.Interface;
using SchoolManagement.Data;
using SchoolManagement.Services.Implementation;
using SchoolManagement.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.NeuroPi.UserManagment
// Register application services
//builder.Services.AddScoped<ITenantService, TenantServiceImpl>();
//builder.Services.AddScoped<IDepartmentService, DepartmentServiceImpl>();
//builder.Services.AddScoped<IGroupService, GroupServiceImpl>();
//builder.Services.AddScoped<IOrganizationService, OrganizationImpl>();
//builder.Services.AddScoped<IGroupUserService, GroupUserServiceImpl>();
//builder.Services.AddScoped<IRolePermissionService, RolePermissionServiceImpl>();
//builder.Services.AddScoped<ITeamService, TeamServiceImpl>();
//builder.Services.AddScoped<IRoleService, RoleServiceImpl>();
//builder.Services.AddScoped<ITeamUserService, TeamUserServiceImpl>();
//builder.Services.AddScoped<IPermissionService, PermissionServiceImpl>();
//builder.Services.AddScoped<IUserDepartmentService, UserDepartmentServiceImpl>();
//builder.Services.AddScoped<IConfigService, ConfigServiceImpl>();
//builder.Services.AddScoped<IUserRolesService, UserRolesServiceImpl>();
//builder.Services.AddScoped<IUserService, UserServiceImpl>();



builder.Services.AddScoped<IContactService, ContactServiceImpl>();

builder.Services.AddDbContext<NeuroPiDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<SchoolManagementDb>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IContactService, ContactServiceImpl>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();