using BloggingApp.Models;
using BloggingApp.Models.UserDTOs;

namespace BloggingApp.Interfaces
{
    public interface ITweetServices
    {
        public Task<AddTweetContentReturnDTO> AddTweetContentByUser(AddUserTweetContent addUserTweetContent);
        public Task<string> AddTweetFiles(TweetFiles tweetFiles);
        public  Task<FeedsPageReturnDTO> TweetsFeeder(int userid);
        public Task<string> AddRetweetContent(AddRetweetDTO addRetweetDTO);
    }

}
