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
builder.Services.AddScoped<IInstitutionService, InstitutionServiceImpl>();

builder.Services.AddScoped<IAccountService, AccountServiceImpl>();
builder.Services.AddScoped<ITransactionService, TransactionServiceImpl>();
builder.Services.AddScoped<IMasterService, MasterServiceImpl>();
builder.Services.AddScoped<IMasterTypeService, MasterTypeServiceImpl>();
builder.Services.AddScoped<IBooksService, BooksServiceImpl>();
builder.Services.AddScoped<IUtilitesService, UtilitiesServiceImpl>();
builder.Services.AddScoped<IItemService, ItemServiceImpl>();

builder.Services.AddScoped<ISubjectService, SubjectServiceImpl>();
builder.Services.AddScoped<ICourseService, CourseServiceImpl>();
builder.Services.AddScoped<ICourseSubjectService, CourseSubjectServiceImpl>();
builder.Services.AddScoped<ITopicService, TopicServiceImpl>();
builder.Services.AddScoped<IWeekService, WeekServiceImpl>();
builder.Services.AddScoped<IParentStudentsService, ParentStudentsServiceImpl>();
builder.Services.AddScoped<IWorkSheetService, WorkSheetServiceImpl>();
builder.Services.AddScoped<IAssessmentService, AssessmentServiceImpl>();
builder.Services.AddScoped<IGradeService, GradeServiceImpl>();
builder.Services.AddScoped<IParentService, ParentServiceImpl>();
builder.Services.AddScoped<IStudentService, StudentServiceImpl>();
builder.Services.AddScoped<IAssessmentSkillService, AssessmentSkillsServiceImpl>();



//builder.Services.AddScoped<IBooksService, BooksServiceImpl>();

builder.Services.AddScoped<IPrefixSuffixService, PrefixSuffixServiceImpl>();

builder.Services.AddDbContext<NeuroPiDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<SchoolManagementDb>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS policy to allow all origins, methods, headers
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
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS middleware with your policy globally
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();
