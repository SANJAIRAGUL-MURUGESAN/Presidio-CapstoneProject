using BloggingApp.Models.CommentDTOs;
using BloggingApp.Models.ReplyDTOs;
using BloggingApp.Models.UserDTOs;

namespace BloggingApp.Interfaces
{
    public interface ICommentServices
    {
        public Task<string> AddComment(AddCommentDTO addCommentDTO);
        public Task<List<CommentDetailsReturnDTO>> ReturnComments(TweetCommentDetails tweetCommentDetails);
        public Task<string> AddCommentReply(AddReplyDTO addReplyDTO);
        public Task<string> AddReplyTOReply(AddReplytoRelpyDTO addReplyDTO);
        public Task<string> AddCommenttoRetweet(AddRetweetCommentDTO addRetweetCommentDTO);
        public Task<string> AddRetweetCommentReply(AddRetweetCommentReplyDTO addRetweetCommentReplyDTO);
        public Task<string> AddRetweetCommentReplyTOReply(AddRetweetCommentReplytoRelpy addRetweetCommentReplytoRelpy);
        public Task<List<RetweetCommentDetailsReturnDTO>> ReturnRetweetComments(RetweetCommentDetails retweetCommentDetails);
    }
}
