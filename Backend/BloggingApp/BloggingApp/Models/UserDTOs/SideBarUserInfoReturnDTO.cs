namespace BloggingApp.Models.UserDTOs
{
    public class SideBarUserInfoReturnDTO
    {
        public int UserId { get; set; }
        public int FollowingCount { get; set; }
        public int FollowersCount { get; set; }
        public string PUserId { get; set; }
        public string UserName { get; set; }
        public string UserProfileImgLink { get; set; }
    }
}
