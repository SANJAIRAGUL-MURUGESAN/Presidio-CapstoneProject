using BloggingApp.Models;
using BloggingApp.Models.UserDTOs;

namespace BloggingApp.Interfaces
{
    public interface ITweetServices
    {
        public Task<AddTweetContentReturnDTO> AddTweetContentByUser(AddUserTweetContent addUserTweetContent);
        public Task<string> AddTweetFiles(TweetFiles tweetFiles);
        public Task<FeedsPageReturnDTO> TweetsFeeder(int userid);
        public Task<string> AddRetweetContent(AddRetweetDTO addRetweetDTO);
        public Task<RetweetDetailsReturnDTO> RetweetDetailsFeeder(RetweetDetailsDTO retweetDetailsDTO);
        public Task<TweetDetailsReturnDTO> TweetDetailsFeeder(TweetDetailsDTO tweetDetailsDTO);
        public Task<string> UpdateTweetContent(UpdateTweetContentDTO updateTweetContentDTO);
        public Task<string> UpdateRetweetContent(UpdateRetweetContentDTO updateRetweetContentDTO);
    }

}