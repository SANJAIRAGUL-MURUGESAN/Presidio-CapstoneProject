using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingApp.Models
{
    public class TweetMentions
    {
        [Key]
        public int Id { get; set; }
        public int MentionerId { get; set; }
        public int MentionedByUserId { get; set; }
        public DateTime MentionedDateTime { get; set; }

        // ForeignKey - Tweet ID
        public int TweetId { get; set; }
        public Tweet Tweet { get; set; }
    }
}
