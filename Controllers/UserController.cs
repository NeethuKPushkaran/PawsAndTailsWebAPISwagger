using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsAndTailsWebAPISwagger.DTOs;
using PawsAndTailsWebAPISwagger.Interfaces;

namespace PawsAndTailsWebAPISwagger.Controllers
{
    //Restrict all actions to admin users
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _userService.AddUserAsync(userDto);
                return Ok("User added successfully");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid User ID");
            }
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound("User Not Found");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                var user = await _userService.FindByEmailAsync(email);
                if (user == null)
                {
                    return NotFound("User not found.");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            try
            {
                var user = await _userService.GetUserByNameAsync(username);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the user: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult>GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving users: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser (int id, [FromBody] UserDto userDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id <=0 || id != userDto.UserId)
            {
                return BadRequest("Invalid User ID or User ID mismatch");
            }

            try
            {
                await _userService.UpdateUserAsync(userDto);
                return Ok("User Updated");
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the user: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Invalid User ID");
            }

            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok("User Deleted Successfully");
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the user: {ex.Message}");
            }
        }

        [HttpPost("block/{id}")]
        public async Task<IActionResult> BlockUser(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Invalid User ID");
            }

            try
            {
                await _userService.BlockUserAsync(id);
                return Ok("User Blocked");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while blocking the user: {ex.Message}");
            }
        }

        [HttpPost("unblock/{id}")]
        public async Task<IActionResult> UnblockUser(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Invalid User ID");
            }

            try
            {
                await _userService.UnblockUserAsync(id);
                return Ok("User Unblocked");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while unblocking the user: {ex.Message}");
            }
        }
    }
}
