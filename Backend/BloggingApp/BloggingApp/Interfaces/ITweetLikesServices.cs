using BloggingApp.Models.CRLikesDTOs;
using BloggingApp.Models.NewFolder;
using BloggingApp.Models.UserDTOs;

namespace BloggingApp.Interfaces
{
    public interface ITweetLikesServices
    {
        public Task<string> AddTweetLikes(AddTweetLikesDTO addTweetLikesDTO);
        public Task<string> AddRetweetLikes(AddRetweekLikeDTO addRetweekLikeDTO);
        public Task<string> AddTweetDisLikes(AddTweetDislikeDTO addTweetDislikeDTO);
        public Task<string> AddRetweetDisLikes(AddRetweetDislikeDTO addRetweetDislikeDTO);
        public Task<string> AddTweetCommentLikes(AddTweetCommentLikesDTO addTweetCommentLikesDTO);
        public Task<string> AddTweetCommentDislike(AddTweetCommentDislikeDTO addTweetCommentDislikeDTO);
        public Task<string> AddTweetCommentReplyLikes(AddTweetCommentReplyLikeDTO addTweetCommentReplyLikeDTO);
        public Task<string> AddTweetCommentReplyDislike(AddTweetCommentReplyDislikeDTO addTweetCommentReplyDislikeDTO);

        //Retweets

        public Task<string> AddRetweetCommentLikes(AddRetweetCommentLikeDTO addRetweetCommentLikeDTO);
        public Task<string> AddRetweetCommentDislike(AddRetweetCommentDislikeDTO addRetweetCommentDislikeDTO);
        public Task<string> AddRetweetCommentReplyLikes(AddRetweetCommentReplyLikeDTO addRetweetCommentReplyLikeDTO);
        public Task<string> AddRetweetCommentReplyDislike(AddRetweetCommentReplyDislikeDTO retweetCommentReplyDislikeDTO);
    }
}