using BloggingApp.Contexts;
using BloggingApp.Exceptions.CommentExceptions;
using BloggingApp.Exceptions.RetweetCommentReplyRepository;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories
{
    public class RetweetCommentReplyRepository : IRepository<int, RetweetCommentReply>
    {
        protected readonly BloggingAppContext _context;
        public RetweetCommentReplyRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<RetweetCommentReply> Add(RetweetCommentReply item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<RetweetCommentReply> Delete(int key)
        {
            var retweetcommentreply = await GetbyKey(key);
            if (retweetcommentreply != null)
            {
                _context.Remove(retweetcommentreply);
                await _context.SaveChangesAsync(true);
                return retweetcommentreply;
            }
            throw new NoSuchRetweetCommentReplyFoundException();
        }
        public virtual async Task<RetweetCommentReply> GetbyKey(int key)
        {
            var retweetcommentreply = await _context.RetweetCommentReplies.FirstOrDefaultAsync(t => t.Id == key);
            if (retweetcommentreply != null)
            {
                return retweetcommentreply;
            }
            throw new NoSuchRetweetCommentReplyFoundException();
        }
        public async Task<IEnumerable<RetweetCommentReply>> Get()
        {
            var retweetcommentreply = await _context.RetweetCommentReplies.ToListAsync();
            if (retweetcommentreply != null)
            {
                return retweetcommentreply;
            }
            throw new NoRetweetCommentRepliesFoundException();
        }
        public async Task<RetweetCommentReply> Update(RetweetCommentReply item)
        {
            var retweetcommentreply = await GetbyKey(item.Id);
            if (retweetcommentreply != null)
            {
                _context.Entry(retweetcommentreply).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return retweetcommentreply;
            throw new NoSuchRetweetCommentReplyFoundException();
        }
    }
}
