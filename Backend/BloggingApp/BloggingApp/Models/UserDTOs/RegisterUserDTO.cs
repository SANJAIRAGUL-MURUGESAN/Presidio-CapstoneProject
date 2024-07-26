namespace BloggingApp.Models.UserDTOs
{
    public class RegisterUserDTO
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string Location { get; set; }
        public string UserMobile { get; set; }
        public string UserGender { get; set; }
        public string IsPremiumHolder { get; set; }
        public string DateOfBirth { get; set; }
        public int Age { get; set; }
        public string BioDescription { get; set; }
        public string UserProfileImgLink { get; set; }
    }
}
