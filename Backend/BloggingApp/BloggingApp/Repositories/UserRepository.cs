using BloggingApp.Contexts;
using BloggingApp.Exceptions.UserExceptions;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories
{
    public class UserRepository : IRepository<int, User>
    {
        private readonly BloggingAppContext _context;
        public UserRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<User> Add(User item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<User> Delete(int key)
        {
            var user = await GetbyKey(key);
            if (user != null)
            {
                _context.Remove(user);
                await _context.SaveChangesAsync(true);
                return user;
            }
            throw new NoSuchUserFoundException();
        }
        public async Task<User> GetbyKey(int key)
        {
            var user = await _context.Users.FirstOrDefaultAsync(t => t.Id == key);
            if (user != null)
            {
                return user;
            }
            throw new NoSuchUserFoundException();
        }
        public async Task<IEnumerable<User>> Get()
        {
            var users = await _context.Users.ToListAsync();
            if (users != null)
            {
                return users;
            }
            throw new NoUsersFoundException();
        }
        public async Task<User> Update(User item)
        {
            var user = await GetbyKey(item.Id);
            if (user != null)
            {
                _context.Entry(user).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return user;
            throw new NoSuchUserFoundException();
        }
    }
}
