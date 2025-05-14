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
    public class UserServiceImpl:IUserService
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
            var user=_context.Users.Where(u=>!u.IsDeleted).FirstOrDefault(u=>u.Username==username && u.Password==password);
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


            return tokenValue;
        }
    }
}
