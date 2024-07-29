using BloggingApp.Contexts;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories.CommentRequest
{
    public class CommentRequestForRepliesRepository : CommentRepository
    {
        public CommentRequestForRepliesRepository(BloggingAppContext context) : base(context)
        {

        }
        public async override Task<Comment> GetbyKey(int key)
        {
            var Files = _context.Comments.Include(e => e.CommentReplies).SingleOrDefault(e => e.Id == key);
            return Files;
        }
    }
}

