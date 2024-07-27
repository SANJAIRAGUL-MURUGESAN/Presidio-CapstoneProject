using BloggingApp.Contexts;
using BloggingApp.Exceptions.HashTagsExceptions;
using BloggingApp.Exceptions.TweetLikesRepository;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories
{
    public class TweetLikesRepository : IRepository<int, TweetLikes>
    {
        private readonly BloggingAppContext _context;
        public TweetLikesRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<TweetLikes> Add(TweetLikes item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<TweetLikes> Delete(int key)
        {
            var tweetLikes = await GetbyKey(key);
            if (tweetLikes != null)
            {
                _context.Remove(tweetLikes);
                await _context.SaveChangesAsync(true);
                return tweetLikes;
            }
            throw new NoSuchTweetLikeFoundException();
        }
        public async Task<TweetLikes> GetbyKey(int key)
        {
            var tweetLikes = await _context.TweetLikes.FirstOrDefaultAsync(t => t.Id == key);
            if (tweetLikes != null)
            {
                return tweetLikes;
            }
            throw new NoSuchTweetLikeFoundException();
        }
        public async Task<IEnumerable<TweetLikes>> Get()
        {
            var tweetLikes = await _context.TweetLikes.ToListAsync();
            if (tweetLikes != null)
            {
                return tweetLikes;
            }
            throw new NoTweetLikesFoundException();
        }
        public async Task<TweetLikes> Update(TweetLikes item)
        {
            var tweetLikes = await GetbyKey(item.Id);
            if (tweetLikes != null)
            {
                _context.Entry(tweetLikes).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return tweetLikes;
            throw new NoSuchTweetLikeFoundException();
        }
    }
}
