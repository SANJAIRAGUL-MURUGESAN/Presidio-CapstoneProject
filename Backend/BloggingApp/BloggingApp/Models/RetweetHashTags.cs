using System.ComponentModel.DataAnnotations;

namespace BloggingApp.Models
{
    public class RetweetHashTags
    {
        [Key]
        public int Id { get; set; }
        public string HashTagTitle { get; set; }
        public int RetweetId { get; set; }

    }
}
