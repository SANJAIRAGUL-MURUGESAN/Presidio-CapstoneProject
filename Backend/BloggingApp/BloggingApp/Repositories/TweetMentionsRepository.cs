using BloggingApp.Contexts;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Exceptions.TweetMentionsExceptions;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories
{
    public class TweetMentionsRepository : IRepository<int, TweetMentions>
    {
        private readonly BloggingAppContext _context;
        public TweetMentionsRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<TweetMentions> Add(TweetMentions item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<TweetMentions> Delete(int key)
        {
            var tweetmentions = await GetbyKey(key);
            if (tweetmentions != null)
            {
                _context.Remove(tweetmentions);
                await _context.SaveChangesAsync(true);
                return tweetmentions;
            }
            throw new NoSuchTweetMentionFoundException();
        }
        public async Task<TweetMentions> GetbyKey(int key)
        {
            var tweetmentions = await _context.TweetMentions.FirstOrDefaultAsync(t => t.Id == key);
            if (tweetmentions != null)
            {
                return tweetmentions;
            }
            throw new NoSuchTweetMentionFoundException();
        }
        public async Task<IEnumerable<TweetMentions>> Get()
        {
            var tweetmentions = await _context.TweetMentions.ToListAsync();
            if (tweetmentions != null)
            {
                return tweetmentions;
            }
            throw new NoTweetMentionsFoundException();
        }
        public async Task<TweetMentions> Update(TweetMentions item)
        {
            var tweetmentions = await GetbyKey(item.Id);
            if (tweetmentions != null)
            {
                _context.Entry(tweetmentions).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return tweetmentions;
            throw new NoSuchTweetMentionFoundException();
        }
    }
}
