using BloggingApp.Contexts;
using BloggingApp.Exceptions.RetweetExceptions;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories
{
    public class RetweetRepository : IRepository<int, Retweet>
    {
        protected readonly BloggingAppContext _context;
        public RetweetRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<Retweet> Add(Retweet item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<Retweet> Delete(int key)
        {
            var tweet = await GetbyKey(key);
            if (tweet != null)
            {
                _context.Remove(tweet);
                await _context.SaveChangesAsync(true);
                return tweet;
            }
            throw new NoSuchRetweetFoundException();
        }
        public async virtual Task<Retweet> GetbyKey(int key)
        {
            var tweet = await _context.Retweets.FirstOrDefaultAsync(t => t.Id == key);
            if (tweet != null)
            {
                return tweet;
            }
            throw new NoSuchRetweetFoundException();
        }
        public async Task<IEnumerable<Retweet>> Get()
        {
            var tweets = await _context.Retweets.ToListAsync();
            if (tweets != null)
            {
                return tweets;
            }
            throw new NoRetweetsFoundException();
        }
        public async Task<Retweet> Update(Retweet item)
        {
            var tweet = await GetbyKey(item.Id);
            if (tweet != null)
            {
                _context.Entry(tweet).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return tweet;
            throw new NoSuchRetweetFoundException();
        }
    }
}
