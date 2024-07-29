using BloggingApp.Contexts;
using BloggingApp.Exceptions.CommentExceptions;
using BloggingApp.Exceptions.ReplyExceptions;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories
{
    public class ReplyRepository : IRepository<int, Reply>
    {
        private readonly BloggingAppContext _context;
        public ReplyRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<Reply> Add(Reply item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<Reply> Delete(int key)
        {
            var reply = await GetbyKey(key);
            if (reply != null)
            {
                _context.Remove(reply);
                await _context.SaveChangesAsync(true);
                return reply;
            }
            throw new NoSuchReplyFoundException();
        }
        public async Task<Reply> GetbyKey(int key)
        {
            var reply = await _context.Replies.FirstOrDefaultAsync(t => t.Id == key);
            if (reply != null)
            {
                return reply;
            }
            throw new NoSuchReplyFoundException();
        }
        public async Task<IEnumerable<Reply>> Get()
        {
            var reply = await _context.Replies.ToListAsync();
            if (reply != null)
            {
                return reply;
            }
            throw new NoReplyFoundException();
        }
        public async Task<Reply> Update(Reply item)
        {
            var reply = await GetbyKey(item.Id);
            if (reply != null)
            {
                _context.Entry(reply).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return reply;
            throw new NoSuchReplyFoundException();
        }
    }
}
