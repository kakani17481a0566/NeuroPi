using CloudinaryDotNet;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Services.Implementation;
using NeuroPi.UserManagment.Services.Interface;
using SchoolManagement.Data;
using SchoolManagement.Services.Implementation;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Audio;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
Env.Load();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// User Management Services
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
builder.Services.AddScoped<IAudioTranscriptionService, AudioTranscriptionServiceImpl>();
builder.Services.AddScoped<ApiKeyService>();

// Shared / Core Services
builder.Services.AddScoped<IContactService, ContactServiceImpl>();
builder.Services.AddScoped<IInstitutionService, InstitutionServiceImpl>();
builder.Services.AddScoped<IAccountService, AccountServiceImpl>();
builder.Services.AddScoped<ITransactionService, TransactionServiceImpl>();
builder.Services.AddScoped<IMasterService, MasterServiceImpl>();
builder.Services.AddScoped<IMasterTypeService, MasterTypeServiceImpl>();
builder.Services.AddScoped<IBooksService, BooksServiceImpl>();
builder.Services.AddScoped<IUtilitesService, UtilitiesServiceImpl>();
builder.Services.AddScoped<IItemService, ItemServiceImpl>();
builder.Services.AddScoped<IPrefixSuffixService, PrefixSuffixServiceImpl>();

// School Domain Services
builder.Services.AddScoped<IStudentService, StudentServiceImpl>();
builder.Services.AddScoped<ISubjectService, SubjectServiceImpl>();
builder.Services.AddScoped<ICourseService, CourseServiceImpl>();
builder.Services.AddScoped<ICourseSubjectService, CourseSubjectServiceImpl>();
builder.Services.AddScoped<ITopicService, TopicServiceImpl>();
builder.Services.AddScoped<IWeekService, WeekServiceImpl>();
builder.Services.AddScoped<IParentStudentsService, ParentStudentsServiceImpl>();
builder.Services.AddScoped<ITimeTableTopicsService, TimeTableTopicsServiceImpl>();
builder.Services.AddScoped<ITimeTableWorksheetService, TimeTableWorksheetServiceImpl>();
builder.Services.AddScoped<IDailyAssessmentService, DailyAssessmentServiceImpl>();
builder.Services.AddScoped<IPeriodService, PeriodServiceImpl>();
builder.Services.AddScoped<ITermService, TermServiceImpl>();
builder.Services.AddScoped<ITimeTableDetailService, TimeTableDetailServiceImpl>();
builder.Services.AddScoped<ITimeTableServices, TimeTableServiceImpl>();
builder.Services.AddScoped<IBranchService, BranchServiceImpl>();
builder.Services.AddScoped<ITimeTableAssessmentService, TimeTableAssessmentServiceImpl>();
builder.Services.AddScoped<IEmployeeService, EmployeeServiceImpl>();
builder.Services.AddScoped<IGradeService, GradeServiceImpl>();
builder.Services.AddScoped<IStudentAttendanceService, StudentAttendanceServiceImpl>();
builder.Services.AddScoped<IWorkSheetService, WorkSheetServiceImpl>();
builder.Services.AddScoped<IPublicHolidayService, PublicHolidayServiceImpl>();
builder.Services.AddScoped<IAssessmentMatrixService, AssessmentMatrixService>();

// ViewModel-based Services
builder.Services.AddScoped<IVwComprehensiveTimeTablesService, VwComprehensiveTimeTablesService>();
builder.Services.AddScoped<IVwTermPlanDetailsService, VwTermPlanDetailsViewServiceImpl>();
builder.Services.AddScoped<IMVTermTableService, VTermTableServiceImpl>();

// EF DbContexts
builder.Services.AddDbContext<NeuroPiDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<SchoolManagementDb>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Cloudinary
var cloudinary = new Cloudinary(new Account(
    builder.Configuration["Cloudinary:CloudName"],
    builder.Configuration["Cloudinary:ApiKey"],
    builder.Configuration["Cloudinary:ApiSecret"]
));
cloudinary.Api.Secure = true;
builder.Services.AddSingleton(cloudinary);

// JWT Authentication
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
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();

// Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "SchoolManagement API", Version = "v1" });
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

// CORS
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
