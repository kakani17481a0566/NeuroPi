using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.IdentityModel.Tokens;
using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.User;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class UserServiceImpl : IUserService
    {
        private readonly NeuroPiDbContext _context;

        private readonly IConfiguration configuration;
        public UserServiceImpl(NeuroPiDbContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }

        public UserLogInSucessVM LogIn(string username, string password)
        {
            var user = _context.Users.Where(u => !u.IsDeleted).FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                string token = GenerateJwtToken(user);
                return new UserLogInSucessVM()
                {
                    UserName = username,
                    token = token,
                };

            }
            return null;
        }

        public string GenerateJwtToken(MUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Username", user.Username??" "),
                new Claim("Email",user.Email?? "")

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn
                );
            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);


            return "bearer " + tokenValue;
        }

        public List<UserResponseVM> GetAllUsers()
        {
            var result=_context.Users.Where(r=>!r.IsDeleted).ToList();
            if (result != null)
            {
                return UserResponseVM.ToViewModelList(result);
            }
            return null;
        }

        public UserResponseVM GetUser(int id)
        {
            var result = _context.Users.FirstOrDefault(r=>r.UserId==id && !r.IsDeleted);
            if (result != null)
            {
                return UserResponseVM.ToViewModel(result);
            }
            return null;

        }

        public UserResponseVM GetUserByIdAndTenantId(int id, int tenantId)
        {
            var user=_context.Users.FirstOrDefault(u=>u.UserId==id && u.TenantId==tenantId && !u.IsDeleted);
            if (user != null)
            {
                return UserResponseVM.ToViewModel(user);
            }
            return null;
        }

        public List<UserResponseVM> GetAllUsersByTenantId(int tenantId)
        {
            var users=_context.Users.Where(u=>u.TenantId==tenantId && !u.IsDeleted).ToList();
            if(users != null && users.Count > 0)
            {
                return UserResponseVM.ToViewModelList(users);
            }
            return null;
        }

        public UserResponseVM AddUser(UserRequestVM request)
        {
            MUser user =UserRequestVM.ToModel(request);
            _context.Users.Add(user);
            _context.SaveChanges();
            return UserResponseVM.ToViewModel(user) ;

        }

        public UserResponseVM UpdateUser(int id, int tenantId, UserUpdateRequestVM userUpdate)
        {
            var user=_context.Users.FirstOrDefault(u=>u.UserId==id && u.TenantId==tenantId && !u.IsDeleted);
            if (user != null)
            {
                user.Username = userUpdate.Username;
                user.TenantId = tenantId;
                user.AlternateNumber = userUpdate.AlternateNumber;
                user.Address = userUpdate.Address;
                user.Email = userUpdate.Email;
                user.DateOfBirth = userUpdate.DateOfBirth;
                user.MiddleName = userUpdate.MiddleName;
                user.FirstName = userUpdate.FirstName;
                user.LastName = userUpdate.LastName;
                user.Password = userUpdate.Password;
                user.UpdatedBy = userUpdate.UpdatedBy;
                user.UpdatedOn = DateTime.UtcNow;
                user.MobileNumber = userUpdate.MobileNumber;
                user.UserId = id;
                _context.SaveChanges();
                return UserResponseVM.ToViewModel(user);
            }
            return null;
        }

        public UserResponseVM DeleteUser(int id,int tenantId)
        {
            var user=_context.Users.FirstOrDefault(u=>u.UserId == id && u.TenantId==tenantId && !u.IsDeleted);
            if (user != null)
            {
                user.IsDeleted = true;
                _context.SaveChanges();
                return UserResponseVM.ToViewModel(user);
            }
            return null;
        }
    }
}