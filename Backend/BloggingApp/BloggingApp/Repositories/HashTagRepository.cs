using BloggingApp.Contexts;
using BloggingApp.Exceptions.HashTagsExceptions;
using BloggingApp.Exceptions.TweetHashTagsExceptions;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories
{
    public class HashTagRepository : IRepository<int, HashTags>
    {
        private readonly BloggingAppContext _context;
        public HashTagRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<HashTags> Add(HashTags item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<HashTags> Delete(int key)
        {
            var hashtags = await GetbyKey(key);
            if (hashtags != null)
            {
                _context.Remove(hashtags);
                await _context.SaveChangesAsync(true);
                return hashtags;
            }
            throw new NoSuchHashTagFoundException();
        }
        public async Task<HashTags> GetbyKey(int key)
        {
            var hashtags = await _context.HashTags.FirstOrDefaultAsync(t => t.Id == key);
            if (hashtags != null)
            {
                return hashtags;
            }
            throw new NoSuchHashTagFoundException();
        }
        public async Task<IEnumerable<HashTags>> Get()
        {
            var hashtags = await _context.HashTags.ToListAsync();
            if (hashtags != null)
            {
                return hashtags;
            }
            throw new NoHashTagsFoundException();
        }
        public async Task<HashTags> Update(HashTags item)
        {
            var hashtags = await GetbyKey(item.Id);
            if (hashtags != null)
            {
                _context.Entry(hashtags).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return hashtags;
            throw new NoSuchHashTagFoundException();
        }
    }
}
