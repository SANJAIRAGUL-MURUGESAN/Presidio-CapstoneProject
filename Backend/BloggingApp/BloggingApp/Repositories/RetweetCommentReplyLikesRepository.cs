using BloggingApp.Contexts;
using BloggingApp.Exceptions.RetweetCommentLikesExceptions;
using BloggingApp.Exceptions.RetweetCommentreplyLikesException;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories
{
    public class RetweetCommentReplyLikesRepository : IRepository<int, RetweetCommentReplyLikes>
    {
        public readonly BloggingAppContext _context;
        public RetweetCommentReplyLikesRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<RetweetCommentReplyLikes> Add(RetweetCommentReplyLikes item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<RetweetCommentReplyLikes> Delete(int key)
        {
            var retweetcommentreply = await GetbyKey(key);
            if (retweetcommentreply != null)
            {
                _context.Remove(retweetcommentreply);
                await _context.SaveChangesAsync(true);
                return retweetcommentreply;
            }
            throw new NoSuchRetweetCommentReplyLikeFoundException();
        }
        public async Task<RetweetCommentReplyLikes> GetbyKey(int key)
        {
            var retweetcommentreply = await _context.RetweetCommentReplyLikes.FirstOrDefaultAsync(t => t.Id == key);
            if (retweetcommentreply != null)
            {
                return retweetcommentreply;
            }
            throw new NoSuchRetweetCommentReplyLikeFoundException();
        }
        public async Task<IEnumerable<RetweetCommentReplyLikes>> Get()
        {
            var retweetcommentreply = await _context.RetweetCommentReplyLikes.ToListAsync();
            if (retweetcommentreply != null)
            {
                return retweetcommentreply;
            }
            throw new NoRetweetCommentReplyLikesFoundException();
        }
        public async Task<RetweetCommentReplyLikes> Update(RetweetCommentReplyLikes item)
        {
            var retweetcommentreply = await GetbyKey(item.Id);
            if (retweetcommentreply != null)
            {
                _context.Entry(retweetcommentreply).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return retweetcommentreply;
            throw new NoSuchRetweetCommentReplyLikeFoundException();
        }
    }
}
