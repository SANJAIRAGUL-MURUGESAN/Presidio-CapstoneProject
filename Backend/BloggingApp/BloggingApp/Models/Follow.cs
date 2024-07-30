using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace BloggingApp.Models
{
    public class Follow
    {
        [Key]
        public int Id { get; set; }
 
        //ForeignKey Key
        public int UserId { get; set; }
        public User User { get; set; }
        public int FollowerId { get; set; }
    }
}
