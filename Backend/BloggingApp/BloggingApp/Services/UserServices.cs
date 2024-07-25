using BloggingApp.Interfaces;
using BloggingApp.Models;
using BloggingApp.Models.UserDTOs;
using BloggingApp.Repositories;

namespace BloggingApp.Services
{
    public class UserServices : IUserServices
    {
        private readonly IRepository<int, User> _UserRepository;

        public UserServices(IRepository<int, User> userRepository)
        {
            _UserRepository = userRepository;
        }

        public User MapRegisterUserDTOtoUser(RegisterUserDTO registerUserDTO)
        {
            User user = new User();
            user.UserName = registerUserDTO.UserName;
            user.UserId = registerUserDTO.UserId;
            user.UserEmail = registerUserDTO.UserEmail;
            user.UserGender = registerUserDTO.UserGender;
            user.UserEmail = registerUserDTO.UserEmail;
            user.UserMobile = registerUserDTO.UserMobile;
            user.Location = registerUserDTO.Location;
            user.IsPremiumHolder = registerUserDTO.IsPremiumHolder;
            user.DateOfBirth = registerUserDTO.DateOfBirth;
            user.Age = registerUserDTO.Age;
            user.BioDescription = registerUserDTO.BioDescription;
            return user;
        }

        public async Task<RegisterUserReturnDTO> RegisterUser(RegisterUserDTO registerUserDTO)
        {
            try
            {
                var user = MapRegisterUserDTOtoUser(registerUserDTO);
                var AddedUser = await _UserRepository.Add(user);
                if (AddedUser != null)
                {
                    RegisterUserReturnDTO registerUserReturnDTO = new RegisterUserReturnDTO();
                    registerUserReturnDTO.Result = "Success";
                    return registerUserReturnDTO;
                }
                throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
