using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Contexts
{
    public class BloggingAppContext : DbContext
    {
        public BloggingAppContext(DbContextOptions options) : base(options)
        {

        }
    }
}
