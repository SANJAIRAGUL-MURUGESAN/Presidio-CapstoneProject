using BloggingApp.Models;

namespace BloggingApp.Interfaces
{
    public interface ITokenServices
    {
        public string GenerateToken(User user);
    }
}