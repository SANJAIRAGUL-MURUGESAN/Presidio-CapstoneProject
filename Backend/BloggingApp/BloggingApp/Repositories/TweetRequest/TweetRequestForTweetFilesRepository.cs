using BloggingApp.Contexts;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories.TweetRequest
{
    public class TweetRequestForTweetFilesRepository : TweetRepository
    {
        public TweetRequestForTweetFilesRepository(BloggingAppContext context) : base(context)
        {
        }
        public async override Task<Tweet> GetbyKey(int key)
        {
            var Files = _context.Tweets.Include(e => e.TweetFiles).SingleOrDefault(e => e.Id == key);
            return Files;
        }
    }
}


