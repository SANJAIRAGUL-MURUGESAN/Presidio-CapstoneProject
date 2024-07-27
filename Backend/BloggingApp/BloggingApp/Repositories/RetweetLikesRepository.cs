using BloggingApp.Contexts;
using BloggingApp.Exceptions.RetweetLikesExceptions;
using BloggingApp.Exceptions.TweetLikesRepository;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories
{
    public class RetweetLikesRepository : IRepository<int, RetweetLikes>
    {
        private readonly BloggingAppContext _context;
        public RetweetLikesRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<RetweetLikes> Add(RetweetLikes item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<RetweetLikes> Delete(int key)
        {
            var RetweetLikes = await GetbyKey(key);
            if (RetweetLikes != null)
            {
                _context.Remove(RetweetLikes);
                await _context.SaveChangesAsync(true);
                return RetweetLikes;
            }
            throw new NoSuchRetweetLikesFoundException();
        }
        public async Task<RetweetLikes> GetbyKey(int key)
        {
            var RetweetLikes = await _context.RetweetLikes.FirstOrDefaultAsync(t => t.Id == key);
            if (RetweetLikes != null)
            {
                return RetweetLikes;
            }
            throw new NoSuchRetweetLikesFoundException();
        }
        public async Task<IEnumerable<RetweetLikes>> Get()
        {
            var RetweetLikes = await _context.RetweetLikes.ToListAsync();
            if (RetweetLikes != null)
            {
                return RetweetLikes;
            }
            throw new NoRetweetLikesFoundException();
        }
        public async Task<RetweetLikes> Update(RetweetLikes item)
        {
            var RetweetLikes = await GetbyKey(item.Id);
            if (RetweetLikes != null)
            {
                _context.Entry(RetweetLikes).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return RetweetLikes;
            throw new NoSuchRetweetLikesFoundException();
        }
    }
}
