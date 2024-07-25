using BloggingApp.Contexts;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Exceptions.TweetFilesExceptions;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories
{
    public class TweetFilesRepository : IRepository<int, TweetFiles>
    {
        private readonly BloggingAppContext _context;
        public TweetFilesRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<TweetFiles> Add(TweetFiles item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<TweetFiles> Delete(int key)
        {
            var tweetfile = await GetbyKey(key);
            if (tweetfile != null)
            {
                _context.Remove(tweetfile);
                await _context.SaveChangesAsync(true);
                return tweetfile;
            }
            throw new NoSuchTweetFileFoundException();
        }
        public async Task<TweetFiles> GetbyKey(int key)
        {
            var tweetfile = await _context.TweetFiles.FirstOrDefaultAsync(t => t.Id == key);
            if (tweetfile != null)
            {
                return tweetfile;
            }
            throw new NoSuchTweetFileFoundException();
        }
        public async Task<IEnumerable<TweetFiles>> Get()
        {
            var tweetfile = await _context.TweetFiles.ToListAsync();
            if (tweetfile != null)
            {
                return tweetfile;
            }
            throw new NoTweetFilesException();
        }
        public async Task<TweetFiles> Update(TweetFiles item)
        {
            var tweetfile = await GetbyKey(item.Id);
            if (tweetfile != null)
            {
                _context.Entry(tweetfile).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return tweetfile;
            throw new NoSuchTweetFileFoundException();
        }

    }
}
