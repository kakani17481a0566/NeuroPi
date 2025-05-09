using System.Net;
using Microsoft.EntityFrameworkCore;
using NeuroPi.Data;
using NeuroPi.Model;
using NeuroPi.Response;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.User;

namespace NeuroPi.Services.Implementation
{
    public class UserServiceImpl : IUserService
    {
        private readonly NeuroPiDbContext _context;

        public UserServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        // Get all non-deleted users
        public async Task<ResponseResult<List<UserResponseVM>>> GetAll()
        {
            try
            {
                var users = await _context.Users
                                          .Include(u => u.Tenant)
                                          .Where(u => u.DeletedAt == null)
                                          .ToListAsync();

                var response = UserResponseVM.ToViewModelList(users);
                return ResponseResult<List<UserResponseVM>>.SuccessResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return ResponseResult<List<UserResponseVM>>.FailResponse(HttpStatusCode.InternalServerError, $"Error fetching users: {ex.Message}");
            }
        }

        // Get single user by ID (only if not soft-deleted)
        public async Task<ResponseResult<UserResponseVM>> GetUserById(int userId)
        {
            try
            {
                var user = await _context.Users
                                         .Include(u => u.Tenant)
                                         .FirstOrDefaultAsync(u => u.UserId == userId && u.DeletedAt == null);

                if (user == null)
                    return ResponseResult<UserResponseVM>.FailResponse(HttpStatusCode.NotFound, $"User with ID {userId} not found.");

                var response = UserResponseVM.ToViewModel(user);
                return ResponseResult<UserResponseVM>.SuccessResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return ResponseResult<UserResponseVM>.FailResponse(HttpStatusCode.InternalServerError, $"Error fetching user with ID {userId}: {ex.Message}");
            }
        }

        // Create new user
        public async Task<ResponseResult<UserResponseVM>> CreateUser(UserCreateRequestVM userRequest)
        {
            try
            {
                if (userRequest == null)
                    return ResponseResult<UserResponseVM>.FailResponse(HttpStatusCode.BadRequest, "User data cannot be null");

                if (!userRequest.TenantId.HasValue)
                    return ResponseResult<UserResponseVM>.FailResponse(HttpStatusCode.BadRequest, "TenantId is required");

                if (!userRequest.CreatedBy.HasValue)
                    return ResponseResult<UserResponseVM>.FailResponse(HttpStatusCode.BadRequest, "CreatedBy is required");

                var user = new MUser
                {
                    Username = userRequest.Username,
                    FirstName = userRequest.FirstName,
                    LastName = userRequest.LastName,
                    Email = userRequest.Email,
                    Password = userRequest.Password,
                    TenantId = userRequest.TenantId.Value,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = userRequest.CreatedBy.Value
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var response = UserResponseVM.ToViewModel(user);
                return ResponseResult<UserResponseVM>.SuccessResponse(HttpStatusCode.Created, response, "User created successfully.");
            }
            catch (Exception ex)
            {
                return ResponseResult<UserResponseVM>.FailResponse(HttpStatusCode.InternalServerError, $"Error creating user: {ex.Message}");
            }
        }

        // Update existing user
        public async Task<ResponseResult<UserResponseVM>> UpdateUser(int userId, UserUpdateRequestVM userRequest)
        {
            try
            {
                if (userRequest == null)
                    return ResponseResult<UserResponseVM>.FailResponse(HttpStatusCode.BadRequest, "User request data cannot be null.");

                var user = await _context.Users
                                         .FirstOrDefaultAsync(u => u.UserId == userId && u.DeletedAt == null);

                if (user == null)
                    return ResponseResult<UserResponseVM>.FailResponse(HttpStatusCode.NotFound, "User not found.");

                // Only update if values are present
                if (!string.IsNullOrEmpty(userRequest.Username)) user.Username = userRequest.Username;
                if (!string.IsNullOrEmpty(userRequest.FirstName)) user.FirstName = userRequest.FirstName;
                if (!string.IsNullOrEmpty(userRequest.LastName)) user.LastName = userRequest.LastName;
                if (!string.IsNullOrEmpty(userRequest.Email)) user.Email = userRequest.Email;
                if (!string.IsNullOrEmpty(userRequest.Password)) user.Password = userRequest.Password;
                if (userRequest.TenantId.HasValue) user.TenantId = userRequest.TenantId.Value;

                user.UpdatedOn = DateTime.UtcNow;
                user.UpdatedBy = userRequest.UpdatedBy;

                await _context.SaveChangesAsync();

                var response = UserResponseVM.ToViewModel(user);
                return ResponseResult<UserResponseVM>.SuccessResponse(HttpStatusCode.OK, response, "User updated successfully.");
            }
            catch (Exception ex)
            {
                return ResponseResult<UserResponseVM>.FailResponse(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

        // Soft delete user
        public async Task<ResponseResult<object>> DeleteUser(int userId)
        {
            try
            {
                var user = await _context.Users
                                         .FirstOrDefaultAsync(u => u.UserId == userId && u.DeletedAt == null);

                if (user == null)
                    return ResponseResult<object>.FailResponse(HttpStatusCode.NotFound, $"User with ID {userId} not found.");

                user.DeletedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return ResponseResult<object>.SuccessResponse(HttpStatusCode.OK, null, "User soft-deleted successfully.");
            }
            catch (Exception ex)
            {
                return ResponseResult<object>.FailResponse(HttpStatusCode.InternalServerError, $"Error deleting user: {ex.Message}");
            }
        }
    }
}
