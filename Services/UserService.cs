using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PawsAndTailsWebAPISwagger.Data;
using PawsAndTailsWebAPISwagger.DTOs;
using PawsAndTailsWebAPISwagger.Interfaces;
using PawsAndTailsWebAPISwagger.Models;

namespace PawsAndTailsWebAPISwagger.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper, ApplicationDbContext context)
        {
            
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        public async Task CreateUserAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, user.Role);
            }
            else
            {
                throw new InvalidOperationException("User Creation failed");
            }
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Invalid User ID");
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            if(user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByNameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be null or whitespace");
            }

            //var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == username);
            var user = await _userManager.FindByNameAsync(username);
            if(user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task AddUserAsync(UserDto userDto)
        {
            var existingUser = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == userDto.UserName);
            if(existingUser != null)
            {
                throw new InvalidOperationException("Username is already taken");
            }

            var user = _mapper.Map<ApplicationUser>(userDto);
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, userDto.Role);
            }
            else
            {
                throw new InvalidOperationException("User Creation Failed");
            }
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            if(userDto.UserId <= 0)
            {
                throw new ArgumentException("Invalid User ID");
            }

            var user = await _userManager.FindByIdAsync(userDto.UserId.ToString());
            if(user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            _mapper.Map(userDto, user);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("User update failed");
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Invalid User ID");
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            if(user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("User deletion failed");
            }
        }

        public async Task BlockUserAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Invalid User ID");
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            if(user != null)
            {
                user.IsBlocked = true;
                await _userManager.UpdateAsync(user);
            }
            else
            {
                throw new KeyNotFoundException("User not found.");
            }
        }

        public async Task UnblockUserAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid User ID");
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            if(user != null)
            {
                user.IsBlocked = false;
                await _userManager.UpdateAsync(user);
            }
            else
            {
                throw new KeyNotFoundException("User not found.");
            }
        }
    }
}
