using BloggingApp.Contexts;
using BloggingApp.Exceptions.HashTagsExceptions;
using BloggingApp.Exceptions.TweetMentionsExceptions;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories
{
    public class RetweetMentionRepository : IRepository<int, RetweetMentions>
    {
        public readonly BloggingAppContext _context;
        public RetweetMentionRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<RetweetMentions> Add(RetweetMentions item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<RetweetMentions> Delete(int key)
        {
            var comment = await GetbyKey(key);
            if (comment != null)
            {
                _context.Remove(comment);
                await _context.SaveChangesAsync(true);
                return comment;
            }
            throw new NoSuchTweetMentionFoundException();
        }
        public virtual async Task<RetweetMentions> GetbyKey(int key)
        {
            var comment = await _context.RetweetMentions.FirstOrDefaultAsync(t => t.Id == key);
            if (comment != null)
            {
                return comment;
            }
            throw new NoSuchTweetMentionFoundException();
        }
        public async Task<IEnumerable<RetweetMentions>> Get()
        {
            var comment = await _context.RetweetMentions.ToListAsync();
            if (comment != null)
            {
                return comment;
            }
            throw new NoTweetMentionsFoundException();
        }
        public async Task<RetweetMentions> Update(RetweetMentions item)
        {
            var comment = await GetbyKey(item.Id);
            if (comment != null)
            {
                _context.Entry(comment).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return comment;
            throw new NoSuchTweetMentionFoundException();
        }
    }
}
