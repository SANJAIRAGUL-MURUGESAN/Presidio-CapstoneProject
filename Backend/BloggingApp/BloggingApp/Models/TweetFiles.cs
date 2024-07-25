using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingApp.Models
{
    public class TweetFiles
    {
        [Key]
        public int Id { get; set; }
        public string File1 { get; set; }
        public string File2 { get; set; }
        public string File3 { get; set; }

        // ForeignKey - Tweet ID
        public int TweetId { get; set; }
        public Tweet Tweet { get; set; }
    }
}
