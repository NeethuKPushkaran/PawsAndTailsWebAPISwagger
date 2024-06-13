using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PawsAndTailsWebAPISwagger.Data;
using PawsAndTailsWebAPISwagger.DTOs;
using PawsAndTailsWebAPISwagger.Interfaces;
using PawsAndTailsWebAPISwagger.Models;

namespace PawsAndTailsWebAPISwagger.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Invalid User ID");
            }

            var user = await _context.Users.FindAsync(id);
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

            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
            if(user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task AddUserAsync(UserDto userDto)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.UserName == userDto.UserName);
            if(existingUser != null)
            {
                throw new InvalidOperationException("Username is already taken");
            }

            var user = _mapper.Map<User>(userDto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            if(userDto.UserId <= 0)
            {
                throw new ArgumentException("Invalid User ID");
            }

            var user = _mapper.Map<User>(userDto);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Invalid User ID");
            }

            var user = await _context.Users.FindAsync(id);
            if(user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task BlockUserAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Invalid User ID");
            }

            var user = await _context.Users.FindAsync(id);
            if(user != null)
            {
                user.IsBlocked = true; //Assuming user entity has an IsBlocked Property
                await _context.SaveChangesAsync();
            }
        }

        public async Task UnblockUserAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid User ID");
            }

            var user = await _context.Users.FindAsync(id);
            if(user != null)
            {
                user.IsBlocked = false; //Assuming User entity has an IsBlocked property
                await _context.SaveChangesAsync();
            }
        }
    }
}
