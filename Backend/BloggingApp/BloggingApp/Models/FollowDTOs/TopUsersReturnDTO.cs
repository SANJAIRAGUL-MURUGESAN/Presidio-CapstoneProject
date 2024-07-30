namespace BloggingApp.Models.FollowDTOs
{
    public class TopUsersReturnDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PUserId { get; set; }
        public string UserProfileLink { get; set; }
        public string IsFollowedByUser { get; set; }
    }
}
