using BloggingApp.Models.UserDTOs;

namespace BloggingApp.Interfaces
{
    public interface ITweetServices
    {
        public Task<AddTweetContentReturnDTO> AddTweetContentByUser(AddUserTweetContent addUserTweetContent);
    }

}
