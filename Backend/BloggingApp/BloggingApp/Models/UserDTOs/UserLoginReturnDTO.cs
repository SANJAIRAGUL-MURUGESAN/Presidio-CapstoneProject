namespace BloggingApp.Models.UserDTOs
{
    public class UserLoginReturnDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public string UserProfileImgLink { get; set; }
    }
}
