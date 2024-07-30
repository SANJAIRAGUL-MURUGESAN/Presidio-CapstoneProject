using BloggingApp.Contexts;
using BloggingApp.Exceptions.CommentExceptions;
using BloggingApp.Exceptions.FollowExceptions;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories
{
    public class FollowRepository : IRepository<int, Follow>
    {
        public readonly BloggingAppContext _context;
        public FollowRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<Follow> Add(Follow item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<Follow> Delete(int key)
        {
            var follow = await GetbyKey(key);
            if (follow != null)
            {
                _context.Remove(follow);
                await _context.SaveChangesAsync(true);
                return follow;
            }
            throw new NoSuchFollowerFoundException();
        }
        public virtual async Task<Follow> GetbyKey(int key)
        {
            var follow = await _context.Follows.FirstOrDefaultAsync(t => t.Id == key);
            if (follow != null)
            {
                return follow;
            }
            throw new NoSuchFollowerFoundException();
        }
        public async Task<IEnumerable<Follow>> Get()
        {
            var follow = await _context.Follows.ToListAsync();
            if (follow != null)
            {
                return follow;
            }
            throw new NoFollowersFoundException();
        }
        public async Task<Follow> Update(Follow item)
        {
            var follow = await GetbyKey(item.Id);
            if (follow != null)
            {
                _context.Entry(follow).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return follow;
            throw new NoSuchFollowerFoundException();
        }
    }
}
