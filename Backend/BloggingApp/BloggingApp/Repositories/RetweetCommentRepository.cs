using BloggingApp.Contexts;
using BloggingApp.Exceptions.CommentExceptions;
using BloggingApp.Exceptions.RetweetCommentExceptions;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories
{
    public class RetweetCommentRepository : IRepository<int, RetweetComment>
    {
        protected readonly BloggingAppContext _context;
        public RetweetCommentRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<RetweetComment> Add(RetweetComment item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<RetweetComment> Delete(int key)
        {
            var comment = await GetbyKey(key);
            if (comment != null)
            {
                _context.Remove(comment);
                await _context.SaveChangesAsync(true);
                return comment;
            }
            throw new NoSuchRetweetCommentFoundException();
        }
        public virtual async Task<RetweetComment> GetbyKey(int key)
        {
            var comment = await _context.RetweetComments.FirstOrDefaultAsync(t => t.Id == key);
            if (comment != null)
            {
                return comment;
            }
            throw new NoSuchRetweetCommentFoundException();
        }
        public async Task<IEnumerable<RetweetComment>> Get()
        {
            var comment = await _context.RetweetComments.ToListAsync();
            if (comment != null)
            {
                return comment;
            }
            throw new NoRetweetCommentsFoundException();
        }
        public async Task<RetweetComment> Update(RetweetComment item)
        {
            var comment = await GetbyKey(item.Id);
            if (comment != null)
            {
                _context.Entry(comment).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return comment;
            throw new NoSuchRetweetCommentFoundException();
        }
    }
}
