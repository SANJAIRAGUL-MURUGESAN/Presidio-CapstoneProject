using BloggingApp.Contexts;
using BloggingApp.Exceptions.TweeCommentLikesRepository;
using BloggingApp.Exceptions.TweetCommentReplyLikesRepository;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories
{
    public class TweetCommentReplyLikeRepository : IRepository<int, TweetCommentReplyLikeRepository>
    {
        private readonly BloggingAppContext _context;
        public TweetCommentReplyLikeRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<TweetReplyLikes> Add(TweetReplyLikes item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<TweetReplyLikes> Delete(int key)
        {
            var RetweetLikes = await GetbyKey(key);
            if (RetweetLikes != null)
            {
                _context.Remove(RetweetLikes);
                await _context.SaveChangesAsync(true);
                return RetweetLikes;
            }
            throw new NoSuchTweetCommentReplyLikeFoundException();
        }
        public async Task<TweetReplyLikes> GetbyKey(int key)
        {
            var RetweetLikes = await _context.TweetReplyLikes.FirstOrDefaultAsync(t => t.Id == key);
            if (RetweetLikes != null)
            {
                return RetweetLikes;
            }
            throw new NoSuchTweetCommentReplyLikeFoundException();
        }
        public async Task<IEnumerable<TweetReplyLikes>> Get()
        {
            var tweetLikes = await _context.TweetReplyLikes.ToListAsync();
            if (tweetLikes != null)
            {
                return tweetLikes;
            }
            throw new NoTweetCommentReplyLikesFoundException();
        }
        public async Task<TweetReplyLikes> Update(TweetReplyLikes item)
        {
            var RetweetLikes = await GetbyKey(item.Id);
            if (RetweetLikes != null)
            {
                _context.Entry(RetweetLikes).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return RetweetLikes;
            throw new NoSuchTweetCommentReplyLikeFoundException();
        }
    }
}
