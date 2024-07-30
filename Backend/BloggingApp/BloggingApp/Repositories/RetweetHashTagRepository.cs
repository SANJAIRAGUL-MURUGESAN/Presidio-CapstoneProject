using BloggingApp.Contexts;
using BloggingApp.Exceptions.CommentExceptions;
using BloggingApp.Exceptions.HashTagsExceptions;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories
{
    public class RetweetHashTagRepository : IRepository<int, RetweetHashTags>
    {
        public readonly BloggingAppContext _context;
        public RetweetHashTagRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<RetweetHashTags> Add(RetweetHashTags item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<RetweetHashTags> Delete(int key)
        {
            var comment = await GetbyKey(key);
            if (comment != null)
            {
                _context.Remove(comment);
                await _context.SaveChangesAsync(true);
                return comment;
            }
            throw new NoSuchHashTagFoundException();
        }
        public virtual async Task<RetweetHashTags> GetbyKey(int key)
        {
            var comment = await _context.RetweetHashTags.FirstOrDefaultAsync(t => t.Id == key);
            if (comment != null)
            {
                return comment;
            }
            throw new NoSuchHashTagFoundException();
        }
        public async Task<IEnumerable<RetweetHashTags>> Get()
        {
            var comment = await _context.RetweetHashTags.ToListAsync();
            if (comment != null)
            {
                return comment;
            }
            throw new NoHashTagsFoundException();
        }
        public async Task<RetweetHashTags> Update(RetweetHashTags item)
        {
            var comment = await GetbyKey(item.Id);
            if (comment != null)
            {
                _context.Entry(comment).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return comment;
            throw new NoSuchHashTagFoundException();
        }
    }
}
