using Microsoft.AspNetCore.Mvc;
using NeuroPi.Response;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.User;
using System.Threading.Tasks;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: api/users
    [HttpGet]
    public async Task<ResponseResult<List<UserResponseVM>>> GetAllUsers()
    {
        // Returns a list of users
        return await _userService.GetAll(); // Ensure GetAll returns ResponseResult<List<UserResponseVM>>
    }

    // GET: api/users/{id}
    [HttpGet("{id}")]
    public async Task<ResponseResult<UserResponseVM>> GetUserById(int id)
    {
        // Returns a user by id
        return await _userService.GetUserById(id); // Ensure GetUserById returns ResponseResult<UserResponseVM>
    }

    // POST: api/users
    [HttpPost]
    public async Task<ResponseResult<UserResponseVM>> CreateUser([FromBody] UserCreateRequestVM userRequest) // Use UserCreateRequestVM for creation
    {
        // Create a new user
        return await _userService.CreateUser(userRequest); // Ensure CreateUser returns ResponseResult<UserResponseVM>
    }

    // PUT: api/users/{id}
    [HttpPut("{id}")]
    public async Task<ResponseResult<UserResponseVM>> UpdateUser(int id, [FromBody] UserUpdateRequestVM userRequest) // Use UserUpdateRequestVM for updating
    {
        // Update an existing user
        return await _userService.UpdateUser(id, userRequest); // Ensure UpdateUser returns ResponseResult<UserResponseVM>
    }

    // DELETE: api/users/{id}
    [HttpDelete("{id}")]
    public async Task<ResponseResult<object>> DeleteUser(int id)
    {
        // Delete a user by id
        return await _userService.DeleteUser(id); // Ensure DeleteUser returns ResponseResult<object>
    }
}
