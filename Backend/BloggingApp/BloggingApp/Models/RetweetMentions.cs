using System.ComponentModel.DataAnnotations;

namespace BloggingApp.Models
{
    public class RetweetMentions
    {
        [Key]
        public int Id { get; set; }
        public int MentionerId { get; set; }
        public int MentionedByUserId { get; set; }
        public DateTime MentionedDateTime { get; set; }
        public int RetweetId { get; set; }
    }
}
