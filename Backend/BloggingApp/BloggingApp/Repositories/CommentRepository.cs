using BloggingApp.Contexts;
using BloggingApp.Exceptions.CommentExceptions;
using BloggingApp.Exceptions.HashTagsExceptions;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories
{
    public class CommentRepository : IRepository<int, Comment>
    {
        protected readonly BloggingAppContext _context;
        public CommentRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<Comment> Add(Comment item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<Comment> Delete(int key)
        {
            var comment = await GetbyKey(key);
            if (comment != null)
            {
                _context.Remove(comment);
                await _context.SaveChangesAsync(true);
                return comment;
            }
            throw new NoSuchCommentFoundException();
        }
        public virtual async Task<Comment> GetbyKey(int key)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(t => t.Id == key);
            if (comment != null)
            {
                return comment;
            }
            throw new NoSuchCommentFoundException();
        }
        public async Task<IEnumerable<Comment>> Get()
        {
            var comment = await _context.Comments.ToListAsync();
            if (comment != null)
            {
                return comment;
            }
            throw new NoCommentsFoundException();
        }
        public async Task<Comment> Update(Comment item)
        {
            var comment = await GetbyKey(item.Id);
            if (comment != null)
            {
                _context.Entry(comment).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return comment;
            throw new NoSuchCommentFoundException();
        }
    }
}
