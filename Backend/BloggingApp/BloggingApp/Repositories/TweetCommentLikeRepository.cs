using BloggingApp.Contexts;
using BloggingApp.Exceptions.RetweetLikesExceptions;
using BloggingApp.Exceptions.TweeCommentLikesRepository;
using BloggingApp.Exceptions.TweetLikesRepository;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories
{
    public class TweetCommentLikeRepository : IRepository<int, TweetCommentLikes>
    {
        private readonly BloggingAppContext _context;
        public TweetCommentLikeRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<TweetCommentLikes> Add(TweetCommentLikes item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<TweetCommentLikes> Delete(int key)
        {
            var RetweetLikes = await GetbyKey(key);
            if (RetweetLikes != null)
            {
                _context.Remove(RetweetLikes);
                await _context.SaveChangesAsync(true);
                return RetweetLikes;
            }
            throw new NoSuchTweetCommentLikeFoundException();
        }
        public async Task<TweetCommentLikes> GetbyKey(int key)
        {
            var RetweetLikes = await _context.TweetCommentLikes.FirstOrDefaultAsync(t => t.Id == key);
            if (RetweetLikes != null)
            {
                return RetweetLikes;
            }
            throw new NoSuchTweetCommentLikeFoundException();
        }
        public async Task<IEnumerable<TweetCommentLikes>> Get()
        {
            var tweetLikes = await _context.TweetCommentLikes.ToListAsync();
            if (tweetLikes != null)
            {
                return tweetLikes;
            }
            throw new NoTweetCommentLikesFoundException();
        }
        public async Task<TweetCommentLikes> Update(TweetCommentLikes item)
        {
            var RetweetLikes = await GetbyKey(item.Id);
            if (RetweetLikes != null)
            {
                _context.Entry(RetweetLikes).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return RetweetLikes;
            throw new NoSuchTweetCommentLikeFoundException();
        }
    }
}
