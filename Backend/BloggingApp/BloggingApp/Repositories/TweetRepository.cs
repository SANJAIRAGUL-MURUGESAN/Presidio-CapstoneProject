using BloggingApp.Contexts;
using BloggingApp.Exceptions.RetweetExceptions;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Exceptions.UserExceptions;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace BloggingApp.Repositories
{
    public class TweetRepository : IRepository<int, Tweet>
    {
        protected readonly BloggingAppContext _context;
        public TweetRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<Tweet> Add(Tweet item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<Tweet> Delete(int key)
        {
            var tweet = await GetbyKey(key);
            if (tweet != null)
            {
                _context.Remove(tweet);
                await _context.SaveChangesAsync(true);
                return tweet;
            }
            throw new NoSuchTweetFoundException();
        }
        public async virtual Task<Tweet> GetbyKey(int key)
        {
            var tweet = await _context.Tweets.FirstOrDefaultAsync(t => t.Id == key);
            if (tweet != null)
            {
                return tweet;
            }
            throw new NoSuchTweetFoundException();
        }
        public async Task<IEnumerable<Tweet>> Get()
        {
            var tweets = await _context.Tweets.ToListAsync();
            if (tweets != null)
            {
                return tweets;
            }
            throw new NoTweetsFoundException();
        }
        public async Task<Tweet> Update(Tweet item)
        {
            var tweet = await GetbyKey(item.Id);
            if (tweet != null)
            {
                _context.Entry(tweet).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return tweet;
            throw new NoSuchTweetFoundException();
        }
    }
}
