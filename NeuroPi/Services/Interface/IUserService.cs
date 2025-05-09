using NeuroPi.Response;
using NeuroPi.ViewModel.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuroPi.Services.Interface
{
    public interface IUserService
    {
        // Get all users
        Task<ResponseResult<List<UserResponseVM>>> GetAll();

        // Get a specific user by ID
        Task<ResponseResult<UserResponseVM>> GetUserById(int userId);

        // Create a new user (accepts UserCreateRequestVM)
        Task<ResponseResult<UserResponseVM>> CreateUser(UserCreateRequestVM userRequest);

        // Update an existing user (accepts UserUpdateRequestVM)
        Task<ResponseResult<UserResponseVM>> UpdateUser(int userId, UserUpdateRequestVM userRequest);

        // Delete a user by ID
        Task<ResponseResult<object>> DeleteUser(int userId);
    }
}
