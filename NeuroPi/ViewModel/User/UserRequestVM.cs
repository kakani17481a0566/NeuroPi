using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.User
{
    // UserRequestVM.cs
    public class UserRequestVM
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string LastName { get; set; }


        public string Email { get; set; }

        public string Password { get; set; }

        public string? MobileNumber { get; set; }
        public string? AlternateNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Address { get; set; }
        public int TenantId { get; set; }  // Make TenantId nullable


        public int? CreatedBy { get; set; }

        public static MUser ToModel(UserRequestVM request)
        {
            return new MUser
            {
                Username = request.Username,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                MobileNumber = request.MobileNumber,
                AlternateNumber = request.AlternateNumber,
                DateOfBirth = request.DateOfBirth,
                Address = request.Address,
                TenantId = request.TenantId,
                CreatedBy = request.CreatedBy,
                CreatedOn = DateTime.UtcNow

            };
        }



    }
}