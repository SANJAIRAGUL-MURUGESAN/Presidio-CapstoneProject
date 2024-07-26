namespace BloggingApp.Models.UserDTOs
{
    public class FeedsPageReturnDTO
    {
        public List<TweetsFeederDTO> tweets { get; set; }
        public List<RetweetsFeederResponseDTO> retweets { get; set; }
    }
}
