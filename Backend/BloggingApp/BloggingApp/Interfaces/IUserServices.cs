
using BloggingApp.Models;
using BloggingApp.Models.FollowDTOs;
using BloggingApp.Models.UserDTOs;

namespace BloggingApp.Interfaces
{
    public interface IUserServices
    {
        public Task<RegisterUserReturnDTO> RegisterUser(RegisterUserDTO registerUserDTO);
        public Task<UserLoginReturnDTO> UserLogin(UserLoginDTO userLoginDTO);
        public Task<List<TopUsersReturnDTO>> ShowFollowers(int userid);
        public Task<string> AddFollower(AddFollowerDTO addFollowerDTO);
        public Task<string> RemoveFollower(RemoveFollowerDTO removeFollowerDTO);
        public Task<SideBarUserInfoReturnDTO> ReturnSideBarUserInfo(int userid);
        public Task<List<UserNotification>> NotificationSender(NotificationUserDetailsDTO notificationUserDetailsDTO);
        public Task<string> UpdateNotification(int UserId);
    }
}
