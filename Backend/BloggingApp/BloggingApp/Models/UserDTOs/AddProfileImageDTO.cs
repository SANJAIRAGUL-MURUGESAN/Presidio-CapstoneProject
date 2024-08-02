namespace BloggingApp.Models.UserDTOs
{
    public class AddProfileImageDTO
    {
        public List<IFormFile> Images { get; set; }
        public int UserId { get; set; }
    }
}
