using BloggingApp.Contexts;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Repositories.RetweetCommentRequest
{
    public class RetweetCommentRequestforRepliesRepository : RetweetCommentRepository
    {
        public RetweetCommentRequestforRepliesRepository(BloggingAppContext context) : base(context)
        {

        }
        public async override Task<RetweetComment> GetbyKey(int key)
        {
            var Files = _context.RetweetComments.Include(e => e.RetweetCommentReplies).SingleOrDefault(e => e.Id == key);
            return Files;
        }
    }
}
