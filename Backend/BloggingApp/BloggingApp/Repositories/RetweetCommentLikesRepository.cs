using BloggingApp.Contexts;
using BloggingApp.Exceptions.RetweetCommentLikesExceptions;
using BloggingApp.Exceptions.RetweetCommentReplyRepository;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories
{
    public class RetweetCommentLikesRepository : IRepository<int, RetweetCommentLikes>
    {
        public readonly BloggingAppContext _context;
        public RetweetCommentLikesRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<RetweetCommentLikes> Add(RetweetCommentLikes item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<RetweetCommentLikes> Delete(int key)
        {
            var retweetcommentreply = await GetbyKey(key);
            if (retweetcommentreply != null)
            {
                _context.Remove(retweetcommentreply);
                await _context.SaveChangesAsync(true);
                return retweetcommentreply;
            }
            throw new NoSuchRetweetCommentLikeFoundException();
        }
        public async Task<RetweetCommentLikes> GetbyKey(int key)
        {
            var retweetcommentreply = await _context.RetweetCommentLikes.FirstOrDefaultAsync(t => t.Id == key);
            if (retweetcommentreply != null)
            {
                return retweetcommentreply;
            }
            throw new NoSuchRetweetCommentLikeFoundException();
        }
        public async Task<IEnumerable<RetweetCommentLikes>> Get()
        {
            var retweetcommentreply = await _context.RetweetCommentLikes.ToListAsync();
            if (retweetcommentreply != null)
            {
                return retweetcommentreply;
            }
            throw new NoRetweetCommentLikesFoundException();
        }
        public async Task<RetweetCommentLikes> Update(RetweetCommentLikes item)
        {
            var retweetcommentreply = await GetbyKey(item.Id);
            if (retweetcommentreply != null)
            {
                _context.Entry(retweetcommentreply).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return retweetcommentreply;
            throw new NoSuchRetweetCommentLikeFoundException();
        }
    }
}
