using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingApp.Models
{
    public class Retweet
    {
        [Key]
        public int Id { get; set; }
        public string RetweetContent { get; set; }
        public DateTime RetweetDateTime { get; set; }
        public string IsCommentEnable { get; set; }

        public int ActualTweetId { get; set; }
        
        [ForeignKey("ActualTweetId")]
        public Tweet Tweet { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<RetweetLikes> RetweetLikes { get; set; }//No effect on the table
        public ICollection<RetweetComment> RetweetComments { get; set; }//No effect on the table

    }
}
