using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace BloggingApp.Models
{
    public class Tweet
    {
        [Key]
        public int Id { get; set; }
        public string TweetContent { get; set; }
        public DateTime TweetDateTime { get; set; }
        public string IsCommentEnable { get; set; }

        // ForeignKey - User ID
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<TweetFiles> TweetFiles { get; set; }//No effect on the table
        public ICollection<TweetMentions> TweetMentions { get; set; }//No effect on the table
        public ICollection<TweetHashTags> TweetHashTags { get; set; }//No effect on the table
    }
}
