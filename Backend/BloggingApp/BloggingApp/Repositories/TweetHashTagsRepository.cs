using BloggingApp.Contexts;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Exceptions.TweetHashTagsExceptions;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories
{
    public class TweetHashTagsRepository : IRepository<int, TweetHashTags>
    {
        private readonly BloggingAppContext _context;
        public TweetHashTagsRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<TweetHashTags> Add(TweetHashTags item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<TweetHashTags> Delete(int key)
        {
            var tweethashtags = await GetbyKey(key);
            if (tweethashtags != null)
            {
                _context.Remove(tweethashtags);
                await _context.SaveChangesAsync(true);
                return tweethashtags;
            }
            throw new NoSuchTweetHashTagsFoundException();
        }
        public async Task<TweetHashTags> GetbyKey(int key)
        {
            var tweethashtags = await _context.TweetHashTags.FirstOrDefaultAsync(t => t.Id == key);
            if (tweethashtags != null)
            {
                return tweethashtags;
            }
            throw new NoSuchTweetHashTagsFoundException();
        }
        public async Task<IEnumerable<TweetHashTags>> Get()
        {
            var tweethashtags = await _context.TweetHashTags.ToListAsync();
            if (tweethashtags != null)
            {
                return tweethashtags;
            }
            throw new NoTweetHashTagsFoundException();
        }
        public async Task<TweetHashTags> Update(TweetHashTags item)
        {
            var tweethashtags = await GetbyKey(item.Id);
            if (tweethashtags != null)
            {
                _context.Entry(tweethashtags).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return tweethashtags;
            throw new NoSuchTweetHashTagsFoundException();
        }
    }
}
