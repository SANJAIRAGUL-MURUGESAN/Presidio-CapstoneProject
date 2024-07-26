using BloggingApp.Exceptions.UserExceptions;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using BloggingApp.Models.UserDTOs;
using BloggingApp.Repositories;
using static BloggingApp.Services.TokenService;

namespace BloggingApp.Services
{
    public class UserServices : IUserServices
    {

        private readonly ITokenServices _TokenService;
        private readonly IRepository<int, User> _UserRepository;

        public UserServices(IRepository<int, User> userRepository, ITokenServices tokenService)
        {
            _UserRepository = userRepository;
            _TokenService = tokenService;
        }

        // Function for User Registration - Starts
        public User MapRegisterUserDTOtoUser(RegisterUserDTO registerUserDTO)
        {
            User user = new User();
            user.UserName = registerUserDTO.UserName;
            user.UserId = registerUserDTO.UserId;
            user.UserEmail = registerUserDTO.UserEmail;
            user.UserPassword = registerUserDTO.UserPassword;
            user.UserGender = registerUserDTO.UserGender;
            user.UserEmail = registerUserDTO.UserEmail;
            user.UserMobile = registerUserDTO.UserMobile;
            user.Location = registerUserDTO.Location;
            user.IsPremiumHolder = registerUserDTO.IsPremiumHolder;
            user.DateOfBirth = registerUserDTO.DateOfBirth;
            user.Age = registerUserDTO.Age;
            user.BioDescription = registerUserDTO.BioDescription;
            user.UserProfileImgLink = registerUserDTO.UserProfileImgLink;
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

        // Function for User Registration - Ends

        // Function for User Login - Starts

        public UserLoginReturnDTO MaUserDTOtoUserLoginReturnDTO(User user)
        {
            UserLoginReturnDTO userLoginReturnDTO = new UserLoginReturnDTO();
            userLoginReturnDTO.Id = user.Id;
            userLoginReturnDTO.UserId = user.UserId;
            userLoginReturnDTO.UserProfileImgLink = user.UserProfileImgLink;
            userLoginReturnDTO.UserName = user.UserName;
            userLoginReturnDTO.Token = _TokenService.GenerateToken(user);
            return userLoginReturnDTO;
        }

        public async Task<UserLoginReturnDTO> UserLogin(UserLoginDTO userLoginDTO)
        {
            try
            {
                var users = await _UserRepository.Get();
                foreach(User user in users)
                {
                    if(user.UserEmail == userLoginDTO.Email)
                    {
                        if(user.UserPassword == userLoginDTO.Password)
                        {
                            return MaUserDTOtoUserLoginReturnDTO(user);
                        }
                        else
                        {
                            throw new InvalidCredentialsException();
                        }
                    }
                }
                throw new InvalidCredentialsException();
            }
            catch (InvalidCredentialsException)
            {
                throw new InvalidCredentialsException();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // Function for User Login - Ends
    }
}
