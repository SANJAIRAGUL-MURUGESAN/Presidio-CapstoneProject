using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingApp.Models
{
    public class HashTags
    {
        [Key]
        public int Id { get; set; }
        public string HashTagTitle { get; set; }
        public int CountInPosts { get; set; }
        public int CountInComments { get; set; }
        public int TweetLikes { get; set; }
        public DateTime HashTagCreatedDateTime { get; set; }
        public int UserId { get; set; }

        // ForeignKey - User ID

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
