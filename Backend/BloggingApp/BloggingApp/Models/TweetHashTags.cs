using System.ComponentModel.DataAnnotations;

namespace BloggingApp.Models
{
    public class TweetHashTags
    {
        [Key]
        public int Id { get; set; }
        public string HashTagTitle { get; set; }

        // ForeignKey - Tweet ID
        public int TweetId { get; set; }
        public Tweet Tweet { get; set; }
    }
}
