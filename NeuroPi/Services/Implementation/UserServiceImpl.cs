using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.User;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class UserServiceImpl : IUserService
    {
        private readonly NeuroPiDbContext _context;
        private readonly IConfiguration _configuration;

        public UserServiceImpl(NeuroPiDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        // login
        public UserLogInSucessVM LogIn(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u =>
                !u.IsDeleted && u.Username == username && u.Password == password);
            MUserRole Role = null;
            MUserDepartment department = null;

            if (user != null)
            {
                Role = _context.UserRoles.Include(r => r.Role).FirstOrDefault(r => !r.IsDeleted && r.UserId == user.UserId && r.TenantId == user.TenantId);
                department=_context.UserDepartments.FirstOrDefault(d=>d.UserId == user.UserId && !d.IsDeleted);

            }


            if (user == null) return null;

            return new UserLogInSucessVM
            {
                UserName = username,
                TenantId = user.TenantId,
                UserId = user.UserId,
                token = GenerateJwtToken(user),
                UserProfile = UserResponseVM.ToViewModel(user),
                RoleName = Role.Role.Name != null ? Role.Role.Name : null,
                departmentId = department.DepartmentId!=null ? department.DepartmentId:0,
                UserImageUrl = user.UserImageUrl ?? null

            };
        }

        private string GenerateJwtToken(MUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Username", user.Username ?? " "),
                new Claim("Email", user.Email ?? "")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: creds);

            return "bearer " + new JwtSecurityTokenHandler().WriteToken(token);
        }

        public List<UserResponseVM> GetAllUsers() =>
            UserResponseVM.ToViewModelList(_context.Users.Where(u => !u.IsDeleted).ToList());

        public List<UserResponseVM> GetAllUsersByTenantId(int tenantId) =>
            UserResponseVM.ToViewModelList(_context.Users.Where(u => u.TenantId == tenantId && !u.IsDeleted).ToList());

        public UserResponseVM GetUser(int id, int tenantId)
        {
            var user=_context.Users.FirstOrDefault(u=>u.UserId==id && u.TenantId==tenantId && !u.IsDeleted);
            if(user != null)
            {
                return UserResponseVM.ToViewModel(user);
            }
            return null;
        }


        public UserResponseVM AddUser(UserRequestVM request)
        {
            var user = UserRequestVM.ToModel(request);
            _context.Users.Add(user);
            _context.SaveChanges();
            return UserResponseVM.ToViewModel(user);
        }

        public UserResponseVM UpdateUser(int id, int tenantId, UserUpdateRequestVM userUpdate)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id && u.TenantId == tenantId && !u.IsDeleted);
            if (user == null) return null;

            user.Username = userUpdate.Username;
            user.FirstName = userUpdate.FirstName;
            user.MiddleName = userUpdate.MiddleName;
            user.LastName = userUpdate.LastName;
            user.Email = userUpdate.Email;
            user.Password = userUpdate.Password;
            user.MobileNumber = userUpdate.MobileNumber;
            user.AlternateNumber = userUpdate.AlternateNumber;
            user.DateOfBirth = userUpdate.DateOfBirth;
            user.Address = userUpdate.Address;
            user.UpdatedBy = userUpdate.UpdatedBy;
            user.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();

            return UserResponseVM.ToViewModel(user);
        }

       
        



        public UserResponseVM DeleteUser(int id, int tenantId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id && u.TenantId == tenantId && !u.IsDeleted);
            if (user == null) return null;

            user.IsDeleted = true;
            _context.SaveChanges();

            return UserResponseVM.ToViewModel(user);
        }

        public async Task<string> UpdateUserImageAsync(int id, int tenantId, UserImageUploadVM request, Cloudinary cloudinary)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id && u.TenantId == tenantId && !u.IsDeleted);
            if (user == null || request.Image == null || request.Image.Length == 0)
                return null;

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(request.Image.FileName, request.Image.OpenReadStream()),
                UseFilename = true,
                UniqueFilename = true,
                Overwrite = true,
                Folder = "user_profiles"
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            if (uploadResult.StatusCode != HttpStatusCode.OK)
                return null;

            var imageUrl = uploadResult.SecureUrl.AbsoluteUri;

            user.UserImageUrl = imageUrl;
            user.UpdatedBy = request.UpdatedBy;
            user.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return imageUrl;
        }

    }
}
