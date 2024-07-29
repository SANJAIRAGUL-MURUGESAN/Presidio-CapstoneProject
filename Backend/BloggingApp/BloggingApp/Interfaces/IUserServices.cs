
using BloggingApp.Models.UserDTOs;

namespace BloggingApp.Interfaces
{
    public interface IUserServices
    {
        public Task<RegisterUserReturnDTO> RegisterUser(RegisterUserDTO registerUserDTO);
        public Task<UserLoginReturnDTO> UserLogin(UserLoginDTO userLoginDTO);
    }
}
