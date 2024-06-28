using PawsAndTailsWebAPISwagger.DTOs;
using PawsAndTailsWebAPISwagger.Models;

namespace PawsAndTailsWebAPISwagger.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(UserDto userDto);
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> FindByEmailAsync(string email);
        Task<UserDto> GetUserByNameAsync(string username);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task UpdateUserAsync(UserDto userDto);
        Task DeleteUserAsync(int id);
        Task BlockUserAsync(int id);
        Task UnblockUserAsync(int id);
    }
}
