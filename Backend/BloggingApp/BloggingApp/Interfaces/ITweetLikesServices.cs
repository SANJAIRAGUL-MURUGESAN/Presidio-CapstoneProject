using BloggingApp.Models.UserDTOs;

namespace BloggingApp.Interfaces
{
    public interface ITweetLikesServices
    {
        public Task<string> AddTweetLikes(AddTweetLikesDTO addTweetLikesDTO);
        public Task<string> AddRetweetLikes(AddRetweekLikeDTO addRetweekLikeDTO);
        public Task<string> AddTweetDisLikes(AddTweetDislikeDTO addTweetDislikeDTO);
        public Task<string> AddRetweetDisLikes(AddRetweetDislikeDTO addRetweetDislikeDTO);
    }
}