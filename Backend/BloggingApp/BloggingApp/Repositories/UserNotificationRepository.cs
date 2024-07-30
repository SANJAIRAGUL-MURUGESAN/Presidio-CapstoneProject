using BloggingApp.Contexts;
using BloggingApp.Exceptions.CommentExceptions;
using BloggingApp.Exceptions.UserNotifications;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories
{
    public class UserNotificationRepository : IRepository<int, UserNotification>
    {
        public readonly BloggingAppContext _context;
        public UserNotificationRepository(BloggingAppContext context)
        {
            _context = context;
        }
        public async Task<UserNotification> Add(UserNotification item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<UserNotification> Delete(int key)
        {
            var userNotification = await GetbyKey(key);
            if (userNotification != null)
            {
                _context.Remove(userNotification);
                await _context.SaveChangesAsync(true);
                return userNotification;
            }
            throw new NoSuchUserNotificationFoundException();
        }
        public virtual async Task<UserNotification> GetbyKey(int key)
        {
            var userNotification = await _context.UserNotifications.FirstOrDefaultAsync(t => t.Id == key);
            if (userNotification != null)
            {
                return userNotification;
            }
            throw new NoSuchUserNotificationFoundException();
        }
        public async Task<IEnumerable<UserNotification>> Get()
        {
            var userNotification = await _context.UserNotifications.ToListAsync();
            if (userNotification != null)
            {
                return userNotification;
            }
            throw new NoUserNotificationsFoundException();
        }
        public async Task<UserNotification> Update(UserNotification item)
        {
            var userNotification = await GetbyKey(item.Id);
            if (userNotification != null)
            {
                _context.Entry(userNotification).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            return userNotification;
            throw new NoSuchUserNotificationFoundException();
        }
    }
}
