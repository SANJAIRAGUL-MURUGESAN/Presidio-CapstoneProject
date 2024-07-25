using BloggingApp.Models.UserDTOs;

namespace BloggingApp.Interfaces
{
    public interface IUserServices
    {
        public Task<RegisterUserReturnDTO> RegisterUser(RegisterUserDTO registerUserDTO);
    }
}
