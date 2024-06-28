using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PawsAndTailsWebAPISwagger.Data;
using PawsAndTailsWebAPISwagger.DTOs;
using PawsAndTailsWebAPISwagger.Interfaces;
using PawsAndTailsWebAPISwagger.Models;
using System.Linq.Expressions;

namespace PawsAndTailsWebAPISwagger.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        //public async Task CreateUserAsync(UserDto userDto)
        //{
        //    try
        //    {
        //        if (userDto == null) throw new ArgumentNullException(nameof(userDto));

        //        var user = _mapper.Map<ApplicationUser>(userDto);
        //        var result = await _userManager.CreateAsync(user, userDto.Password);

        //        if (result.Succeeded)
        //        {
        //            await _userManager.AddToRoleAsync(user, userDto.Role);
        //        }
        //        else
        //        {
        //            throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new Exception($"An error occurred while creating the user: {ex.Message}", ex);
        //    }
        //}

        public async Task AddUserAsync(UserDto userDto)
        {
            try
            {
                if (userDto == null) throw new ArgumentNullException(nameof(userDto));

                var existingUser = await _userRepository.FindByUsernameAsync(userDto.UserName);

                if(existingUser != null)
                {
                    throw new InvalidOperationException("Username is already taken");
                }

                var user = _mapper.Map<User>(userDto);
                await _userRepository.AddAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding the user: {ex.Message}");
            }
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("Invalid User ID");
                }

                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    throw new KeyNotFoundException("User not found");
                }

                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the user by ID: {ex.Message}");
            }
        }

        public async Task<UserDto> FindByEmailAsync(string email)
        {
            try
            {
                var user = await _userRepository.FindByEmailAsync(email);

                if (user == null)
                {
                    throw new KeyNotFoundException("User not found.");
                }

                return _mapper.Map<UserDto>(user);
            }

            catch(Exception ex)
            {
                throw new Exception($"An Error occurred while retrieving the user by email: {ex.Message}", ex);
            }
        }

        public async Task<UserDto> GetUserByNameAsync(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    throw new ArgumentNullException(nameof(username));
                }

                var user = await _userRepository.FindByUsernameAsync(username);
                if (user == null)
                {
                    throw new KeyNotFoundException("User not found.");
                }

                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the user by username: {ex.Message}", ex);
            }
        }
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                return _mapper.Map<IEnumerable<UserDto>>(users);
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occurred while retrieving all users: {ex.Message}", ex);
            }
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userDto.UserId);
                if (user == null) throw new KeyNotFoundException("User not found.");

                _mapper.Map(userDto, user);
                await _userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the user: {ex.Message}");
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("Invalid user ID.");

                var user = await _userRepository.GetByIdAsync(id);
                if (user == null) throw new KeyNotFoundException("User not found.");

                await _userRepository.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the user: {ex.Message}");
            }
        }

        public async Task BlockUserAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null) throw new KeyNotFoundException("User not found.");

                user.IsBlocked = true;
                await _userRepository.UpdateAsync(user);
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occurred while blocking the user: {ex.Message}");
            }
        }

        public async Task UnblockUserAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null) throw new KeyNotFoundException("User not found.");

                user.IsBlocked = false;
                await _userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while unblocking the user: {ex.Message}");
            }
        }
    }
}
