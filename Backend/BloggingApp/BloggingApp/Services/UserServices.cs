using BloggingApp.Contexts;
using BloggingApp.Exceptions.UserExceptions;
using BloggingApp.Exceptions.UserNotifications;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using BloggingApp.Models.FollowDTOs;
using BloggingApp.Models.UserDTOs;
using BloggingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using static BloggingApp.Services.TokenService;

namespace BloggingApp.Services
{
    public class UserServices : IUserServices
    {

        protected readonly BloggingAppContext _context;
   
        private readonly ITokenServices _TokenService;
        private readonly IRepository<int, User> _UserRepository;
        private readonly IRepository<int, Follow> _FollowRepository;
        private readonly IRepository<int, UserNotification> _UserNotificationRepository;

        public UserServices(BloggingAppContext context,IRepository<int, User> userRepository, ITokenServices tokenService, IRepository<int, Follow> followRepository,
            IRepository<int, UserNotification> userNotificationRepository)
        {
            _context = context;
            _UserRepository = userRepository;
            _TokenService = tokenService;
            _FollowRepository = followRepository;
            _UserNotificationRepository = userNotificationRepository;
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
            user.JoinedDate = DateTime.Now;
            user.BioDescription = registerUserDTO.BioDescription;
            user.UserProfileImgLink = registerUserDTO.UserProfileImgLink;
            return user;
        }

        public async Task<RegisterUserReturnDTO> RegisterUser(RegisterUserDTO registerUserDTO)
        {
            try
            {
                var user = MapRegisterUserDTOtoUser(registerUserDTO);
                var AvailableUsers = await _UserRepository.Get();
                int Flag = 0;
                foreach(var user1 in AvailableUsers)
                {
                    if(user1.UserEmail == registerUserDTO.UserEmail)
                    {
                        Flag = 1;
                        break;
                    }
                    if(user1.UserId == registerUserDTO.UserId)
                    {
                        Flag = 1;
                        break;
                    }
                }
                if (Flag == 0)
                {
                    var AddedUser = await _UserRepository.Add(user);
                    if (AddedUser != null)
                    {
                        RegisterUserReturnDTO registerUserReturnDTO = new RegisterUserReturnDTO();
                        registerUserReturnDTO.Result = "Success";
                        return registerUserReturnDTO;
                    }
                }
                else
                {
                    throw new UserEmailAlreadyExistsException();
                }
                throw new Exception();
            }
            catch (UserEmailAlreadyExistsException)
            {
                throw new UserEmailAlreadyExistsException();
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

        // Function for return Top 5 Users - Starts

        public async Task<List<TopUsersReturnDTO>> ShowFollowers(int userid)
        {
            try
            {
                var topUsers = await _context.Users
                                       .OrderByDescending(u => u.Followers.Count)
                                       .Take(2)
                                       .ToListAsync();

                //var topUsers = await _UserRepository.Get();
                List<TopUsersReturnDTO> UsersList = new List<TopUsersReturnDTO>(); 
                foreach(var user in topUsers)
                {
                    TopUsersReturnDTO user1 = new TopUsersReturnDTO();
                    var isfollowed = (await _FollowRepository.Get()).Where(f => f.UserId == userid);
                    int flag = 0;
                    foreach (var u in isfollowed)
                    {
                        if (u.FollowerId == user.Id)
                        {
                            user1.IsFollowedByUser = "Yes";
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 0)
                    {
                        user1.IsFollowedByUser = "No";
                    }
                    user1.UserId = user.Id;
                    user1.PUserId = user.UserId;
                    user1.UserName = user.UserName;
                    user1.UserProfileLink = user.UserProfileImgLink;
                    UsersList.Add(user1);
                }
                return UsersList;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // Function for return Top 5 Users - Ends

        // Function for Add User Follow - Starts
        public Follow MapAddFollowDTOtoFollow(AddFollowerDTO addFollowerDTO)
        {
            Follow follow = new Follow();
            follow.FollowerId = addFollowerDTO.FollowerId;
            follow.UserId = addFollowerDTO.UserId;
            return follow;
        }

        public async Task<string> AddFollower(AddFollowerDTO addFollowerDTO)
        {
            try
            {
                Follow MappedFollow = MapAddFollowDTOtoFollow(addFollowerDTO);
                var AddedResult = await _FollowRepository.Add(MappedFollow);

                var originoluser = await _UserRepository.GetbyKey(addFollowerDTO.UserId);
                var originoluserName = originoluser.UserName;

                UserNotification userNotification = new UserNotification();
                userNotification.UserId = addFollowerDTO.FollowerId;
                userNotification.NotificationPost = originoluser.UserProfileImgLink;
                userNotification.IsUserSeen = "No";
                userNotification.ContentDateTime = DateTime.Now;
                userNotification.TweetType = "Follow";
                userNotification.TweetId = originoluser.Id;
                userNotification.NotificatioContent = originoluserName + " Started Following you";
                var addedNotification = await _UserNotificationRepository.Add(userNotification);

                if (AddedResult != null)
                {
                    return "success";
                }
                throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // Function for Add User Follow - Ends

        // Function for Add User Unfollow(Remove) - Starts

        public async Task<string> RemoveFollower(RemoveFollowerDTO removeFollowerDTO)
        {
            try
            {
                var AvailableFollowers = (await _FollowRepository.Get()).Where(f => f.UserId==removeFollowerDTO.UserId);
                foreach(var followers in AvailableFollowers)
                {
                    if(followers.FollowerId == removeFollowerDTO.FollowerId)
                    {
                        var RemovedFollower = await _FollowRepository.Delete(followers.Id);
                        if(RemovedFollower != null)
                        {
                            return "success";
                        }
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // Function for Add User Unfollow(Remove) - Ends

        // Function for Add Return User Sidebar info - Starts
        public async Task<SideBarUserInfoReturnDTO> ReturnSideBarUserInfo(int userid)
        {
            try
            {
                SideBarUserInfoReturnDTO sideBarUserInfoReturnDTO = new SideBarUserInfoReturnDTO();
                var user = await _UserRepository.GetbyKey(userid);
                var UserFollowing = (await _FollowRepository.Get()).Where(f => f.UserId == userid);
                var Followers = (await _FollowRepository.Get()).Where(f => f.FollowerId == userid);
                sideBarUserInfoReturnDTO.UserId = user.Id;
                sideBarUserInfoReturnDTO.PUserId = user.UserId;
                sideBarUserInfoReturnDTO.UserName = user.UserName;
                sideBarUserInfoReturnDTO.UserProfileImgLink = user.UserProfileImgLink;
                sideBarUserInfoReturnDTO.FollowingCount = UserFollowing.Count();
                sideBarUserInfoReturnDTO.FollowersCount = Followers.Count();
                return sideBarUserInfoReturnDTO;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        // Function for Add Return User Sidebar info - Ends

        // Function to send all notification to users - starts

        public async Task<List<UserNotification>> NotificationSender(NotificationUserDetailsDTO notificationUserDetailsDTO)
        {
            try
            {
                var notificationdetails = (await _UserNotificationRepository.Get()).Where(u => u.UserId == notificationUserDetailsDTO.UserId).ToList();
                return notificationdetails;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        // Function to send all notification to users - Ends

        // Function to send update notification - starts

        public async Task<string> UpdateNotification(int UserId)
        {
            try
            {
                var notificationdetails1 = (await _UserNotificationRepository.Get()).Where(u => u.UserId == UserId).ToList();
                foreach(var user in notificationdetails1)
                {
                    user.IsUserSeen = "Yes";
                    await _UserNotificationRepository.Update(user);
                }
                return "success";
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        // Function to send update notification - ends

        // Function to Provide User Profile Details - starts
        public async Task<User> UserProfile(int UserId)
        {
            try
            {
                var user = await _UserRepository.GetbyKey(UserId);
                if (user != null)
                {
                    return user;
                }
                throw new Exception();
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }
        // Function to Provide User Profile Details - ends

        // Function to Update User Profile Image - starts
        public async Task<User> UserProfileImageUpdate(UpdateUserProfileImageDTO updateUserProfileImageDTO)
        {
            try
            {
                var user = await _UserRepository.GetbyKey(updateUserProfileImageDTO.UserId);
                user.UserProfileImgLink = updateUserProfileImageDTO.ProfileImageUrl;
                var uploadedimage = await _UserRepository.Update(user);
                if (uploadedimage != null)
                {
                    return user;
                }
                throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        // Function to Update User Profile Image - ends

        // Function to search username - starts
        public async Task<List<User>> UserProfileSearch(string username)
        {
            try
            {
                Console.WriteLine("Here");
                Console.WriteLine("Username",username);
                var user = ((await _UserRepository.Get()).Where(u => u.UserName == username)).ToList();
                Console.WriteLine(user.Count);
                if(user.Count == 0)
                {
                    Console.WriteLine(username.Length);
                    var prefix = username.Substring(0, Math.Min(3, username.Length));
                    Console.Write("prefix",prefix);
                    user = await _context.Users
                        .Where(u => u.UserName.StartsWith(prefix))
                        .ToListAsync();
                }
                return user;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // Function to search username - Ends

    }
}
