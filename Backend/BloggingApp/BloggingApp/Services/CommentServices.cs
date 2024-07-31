using Azure.Storage.Blobs.Models;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using BloggingApp.Models.CommentDTOs;
using BloggingApp.Models.FollowDTOs;
using BloggingApp.Models.ReplyDTOs;
using BloggingApp.Models.UserDTOs;
using BloggingApp.Repositories;
using BloggingApp.Repositories.CommentRequest;
using BloggingApp.Repositories.RetweetCommentRequest;
using BloggingApp.Repositories.TweetRequest;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BloggingApp.Services
{
    public class CommentServices : ICommentServices
    {
        private readonly IRepository<int, Tweet> _TweetRepository;
        private readonly IRepository<int, Retweet> _RetweetRepository;
        private readonly IRepository<int, Comment> _CommentRepository;
        private readonly IRepository<int, Reply> _ReplyRepository;
        private readonly IRepository<int, User> _UserRepository;
        private readonly CommentRequestForRepliesRepository CommentRequestForRepliesRepository;
        private readonly RetweetCommentRequestforRepliesRepository _RetweetCommentRequestForRepliesRepository;
        private readonly IRepository<int, RetweetComment> _RetweetCommentRepository;
        private readonly IRepository<int, RetweetCommentReply> _RetweetCommentReplyRepository;
        private readonly IRepository<int, TweetCommentLikes> _TweetCommentLikesRepository;
        private readonly IRepository<int, TweetReplyLikes> _TweetCommentReplyLikesRepository;
        private readonly IRepository<int, RetweetCommentLikes> _RetweetCommentLikesRepository;
        private readonly IRepository<int, UserNotification> _UserNotificationRepository;
        private readonly IRepository<int, RetweetCommentReplyLikes> _RetweetCommentReplyLikesReposioryl;
        public CommentServices(IRepository<int, Comment> commentRepository, IRepository<int, Reply> replyRepository, IRepository<int, User> userRepository,IRepository<int,Tweet> tweetRepository, IRepository<int, Retweet> retweetRepository,
            CommentRequestForRepliesRepository commentRequestForRepliesRepository, IRepository<int, RetweetComment> retweetCommentRepository, IRepository<int, RetweetCommentReply> retweetCommentReplyRepository,
            RetweetCommentRequestforRepliesRepository retweetCommentRequestForRepliesRepository, IRepository<int, TweetCommentLikes> tweetCommentsLikesRepository,
            IRepository<int, TweetReplyLikes> tweetReplyLikesRepository, IRepository<int, RetweetCommentLikes> retweetCommentLikesRepository, IRepository<int, RetweetCommentReplyLikes> retweetCommentReplyLikesReposioryl, IRepository<int, UserNotification> userNotificationRepository)
        {
            _RetweetRepository = retweetRepository;
            _TweetRepository = tweetRepository;
            _CommentRepository = commentRepository;
            _UserRepository = userRepository;
            _ReplyRepository = replyRepository;
            _RetweetCommentRepository = retweetCommentRepository;
            _RetweetCommentReplyRepository = retweetCommentReplyRepository;
            CommentRequestForRepliesRepository = commentRequestForRepliesRepository;
            _RetweetCommentRequestForRepliesRepository = retweetCommentRequestForRepliesRepository;
            _TweetCommentLikesRepository = tweetCommentsLikesRepository;
            _TweetCommentReplyLikesRepository = tweetReplyLikesRepository;
            _RetweetCommentLikesRepository = retweetCommentLikesRepository;
            _RetweetCommentReplyLikesReposioryl = retweetCommentReplyLikesReposioryl;
            _UserNotificationRepository = userNotificationRepository;
        }

        // Function to add Tweet Comment - Starts
        public Comment MapAddCommentDTOToComment(AddCommentDTO addCommentDTO)
        {
            Comment comment = new Comment();
            comment.CommentContent = addCommentDTO.CommentContent;
            comment.CommentDateTime = DateTime.Now;
            comment.UserId = addCommentDTO.UserId;
            comment.TweetId = addCommentDTO.TweetId;
            return comment;
        }
        public async Task<string> AddComment(AddCommentDTO addCommentDTO)
        {
            try
            {
                var comment = MapAddCommentDTOToComment(addCommentDTO);
                var addedcomment = await _CommentRepository.Add(comment);

                var TweetDetails = await _TweetRepository.GetbyKey(addedcomment.TweetId);
                var TweetOwnerDetails = await _UserRepository.GetbyKey(TweetDetails.UserId);

                var CommentUserDetails = await _UserRepository.GetbyKey(addedcomment.UserId);
                var CommentedUsername = CommentUserDetails.UserName;
                UserNotification userNotification = new UserNotification();
                userNotification.UserId = TweetOwnerDetails.Id;
                userNotification.NotificationPost = CommentUserDetails.UserProfileImgLink;
                userNotification.IsUserSeen = "No";
                userNotification.ContentDateTime = addedcomment.CommentDateTime;
                userNotification.TweetType = "Tweet";
                userNotification.TweetId = TweetDetails.Id;
                userNotification.NotificatioContent = CommentedUsername + " Commented on your Tweet";
                var addedNotification = await _UserNotificationRepository.Add(userNotification);
                if (addCommentDTO != null)
                {
                    return "success";
                }
                throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        // Function to add Tweet Comment - Ends

        // Function to add Comment Reply- Starts

        public Reply MapAddReplyDTOToReply(AddReplyDTO addReplyDTO)
        {
            Reply reply = new Reply();
            reply.ReplyContent = addReplyDTO.ReplyContent;
            reply.ReplyDateTime = DateTime.Now;
            reply.ReplyType = "Comment";
            reply.UserId = addReplyDTO.UserId;
            reply.CommentId = addReplyDTO.Comment_ReplyId;
            return reply;
        }

        public async Task<string> AddCommentReply(AddReplyDTO addReplyDTO)
        {
            try
            {
                var reply = MapAddReplyDTOToReply(addReplyDTO);
                var addedreply = await _ReplyRepository.Add(reply);

                var CommentDetails = await _CommentRepository.GetbyKey(addedreply.CommentId);
                var CommentOwnerDetails = await _UserRepository.GetbyKey(CommentDetails.UserId);

                var ReplyUserDetails = await _UserRepository.GetbyKey(addedreply.UserId);

                UserNotification userNotification = new UserNotification();
                userNotification.UserId = CommentOwnerDetails.Id;
                userNotification.NotificationPost = ReplyUserDetails.UserProfileImgLink;
                userNotification.IsUserSeen = "No";
                userNotification.ContentDateTime = addedreply.ReplyDateTime;
                userNotification.TweetType = "Tweet";
                userNotification.TweetId = CommentDetails.TweetId;
                userNotification.NotificatioContent = ReplyUserDetails.UserName+" Replied to your Comment";
                var addedNotification = await _UserNotificationRepository.Add(userNotification);

                if (addedreply != null)
                {
                    return "success";
                }
                throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // Function to add Comment Reply - Ends

        // Function to Provide All Comment,Replies for tweets- Starts
        public async Task<List<CommentDetailsReturnDTO>> ReturnComments(TweetCommentDetails tweetCommentDetails)
        {
            try
            {
                var comments = await _CommentRepository.Get();
                List<CommentDetailsReturnDTO> commentDetailsReturnDTOs = new List<CommentDetailsReturnDTO>();
                foreach (var comment in comments)
                {
                    if (comment.TweetId == tweetCommentDetails.TweetId)
                    {
                        CommentDetailsReturnDTO commentDetailsReturnDTO = new CommentDetailsReturnDTO();
                        commentDetailsReturnDTO.CommentId = comment.Id;
                        commentDetailsReturnDTO.CommentContent = comment.CommentContent;
                        commentDetailsReturnDTO.CommentDateTime = comment.CommentDateTime;
                        var userdetails = await _UserRepository.GetbyKey(comment.UserId);
                        commentDetailsReturnDTO.UserId = comment.UserId;
                        commentDetailsReturnDTO.UserName = userdetails.UserName;
                        commentDetailsReturnDTO.PUserId = userdetails.UserId;
                        commentDetailsReturnDTO.UserProfileLink = userdetails.UserProfileImgLink;

                        var commentlikescount = ((await _TweetCommentLikesRepository.Get()).Where(l => l.CommentId == comment.Id)).ToList();
                        commentDetailsReturnDTO.LikesCount = commentlikescount.Count;

                        int flag1 = 0;
                        foreach(var like in commentlikescount)
                        {
                            if(like.LikedUserId == tweetCommentDetails.UserId)
                            {
                                commentDetailsReturnDTO.IsLikedByUser = "Yes";
                                flag1 = 1;
                                break;
                                
                            }
                        }
                        if(flag1 == 0)
                        {
                            commentDetailsReturnDTO.IsLikedByUser = "No";
                        }
                        //commentDetailsReturnDTOs.Add(commentDetailsReturnDTO);

                        List<ReplyDetailsReturnDTO> ReplyList = new List<ReplyDetailsReturnDTO>();
                        var replies = await CommentRequestForRepliesRepository.GetbyKey(comment.Id);
                        var replies2 = replies.CommentReplies.ToList();
                        if (replies2.Count > 0)
                        {
                            foreach (var reply in replies2)
                            {
                                if (reply.ReplyId == 0)
                                {
                                    ReplyDetailsReturnDTO replyDetailsReturnDTO = new ReplyDetailsReturnDTO();
                                    replyDetailsReturnDTO.Id = reply.Id;
                                    replyDetailsReturnDTO.ReplyType = reply.ReplyType;
                                    replyDetailsReturnDTO.ReplyContent = reply.ReplyContent;
                                    replyDetailsReturnDTO.UserId = reply.UserId;
                                    replyDetailsReturnDTO.ReplyDateTime = reply.ReplyDateTime;
                                    replyDetailsReturnDTO.CommentId = reply.CommentId;
                                    var user = await _UserRepository.GetbyKey(reply.UserId);
                                    replyDetailsReturnDTO.PUserId = user.UserId;
                                    replyDetailsReturnDTO.UserName = user.UserName;
                                    replyDetailsReturnDTO.UserProfileImageLink = user.UserProfileImgLink;

                                    var ReplyLikescount = ((await _TweetCommentReplyLikesRepository.Get()).Where(l => l.ReplyId == reply.Id)).ToList();
                                    replyDetailsReturnDTO.LikedCount = ReplyLikescount.Count;
                                    int flag2 = 0;

                                    foreach(var like in ReplyLikescount)
                                    {
                                        if(like.LikedUserId == tweetCommentDetails.UserId)
                                        {
                                            replyDetailsReturnDTO.IsLikedByUser = "Yes";
                                            flag2 = 1;
                                            break;
                                        }
                                    }
                                    if (flag2 == 0)
                                    {
                                        replyDetailsReturnDTO.IsLikedByUser = "No";
                                    }

                                    ReplyList.Add(replyDetailsReturnDTO);


                                    if (ReplyList.Count > 0)
                                    {
                                        int flag = 0;
                                        while (flag == 0)
                                        {
                                            var lastreply = (await _ReplyRepository.Get()).Where(l => l.ReplyId == (ReplyList[ReplyList.Count - 1].Id)).ToList();
                                            if (lastreply.Count == 0)
                                            {
                                                flag = 1;
                                                break;
                                            }
                                            else
                                            {
                                                foreach (var r in lastreply)
                                                {
                                                    ReplyDetailsReturnDTO replyDetailsReturnDTO2 = new ReplyDetailsReturnDTO();
                                                    replyDetailsReturnDTO2.Id = r.Id;
                                                    replyDetailsReturnDTO2.ReplyType = r.ReplyType;
                                                    replyDetailsReturnDTO2.ReplyContent = r.ReplyContent;
                                                    replyDetailsReturnDTO2.UserId = r.UserId;
                                                    replyDetailsReturnDTO2.ReplyDateTime = r.ReplyDateTime;
                                                    replyDetailsReturnDTO2.CommentId = r.CommentId;
                                                    var user1 = await _UserRepository.GetbyKey(r.UserId);
                                                    replyDetailsReturnDTO2.PUserId = user1.UserId;
                                                    replyDetailsReturnDTO2.UserName = user1.UserName;
                                                    replyDetailsReturnDTO2.UserProfileImageLink = user1.UserProfileImgLink;

                                                    var ReplyLikescount1 = ((await _TweetCommentReplyLikesRepository.Get()).Where(l => l.ReplyId == r.Id)).ToList();
                                                    replyDetailsReturnDTO2.LikedCount = ReplyLikescount1.Count;
                                                    int flag5 = 0;

                                                    foreach (var like in ReplyLikescount1)
                                                    {
                                                        if (like.LikedUserId == tweetCommentDetails.UserId)
                                                        {
                                                            replyDetailsReturnDTO2.IsLikedByUser = "Yes";
                                                            flag5 = 1;
                                                            break;
                                                        }
                                                    }
                                                    if (flag5 == 0)
                                                    {
                                                        replyDetailsReturnDTO2.IsLikedByUser = "No";
                                                    }

                                                    ReplyList.Add(replyDetailsReturnDTO2);
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }
                        commentDetailsReturnDTO.Replies = ReplyList;
                        commentDetailsReturnDTOs.Add(commentDetailsReturnDTO);
                    }
                }
                return commentDetailsReturnDTOs;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        // Function to Provide All Comment for tweets- Ends

        // Function to Add All Reply for a Reply - Starts

        public Reply MapAddReplyDTOToReply1(AddReplytoRelpyDTO addReplyDTO)
        {
            Reply reply = new Reply();
            reply.ReplyContent = addReplyDTO.ReplyContent;
            reply.ReplyDateTime = DateTime.Now;
            reply.ReplyType = "Reply";
            reply.UserId = addReplyDTO.UserId;
            reply.CommentId = addReplyDTO.CommentId;
            reply.ReplyId = addReplyDTO.ReplyId;
            return reply;
        }

        public async Task<string> AddReplyTOReply(AddReplytoRelpyDTO addReplyDTO)
        {
            try
            {
                var reply = MapAddReplyDTOToReply1(addReplyDTO);
                var addedreply = await _ReplyRepository.Add(reply);
                if (addedreply != null)
                {
                    return "success";
                }
                throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        // Function to Add All Reply for a Reply - Ends

        // Function to Add Comment for a Retweet - Starts

        public RetweetComment MapAddRetweetCommentDTOToComment(AddRetweetCommentDTO addRetweetCommentDTO)
        {
            RetweetComment retweetComment = new RetweetComment();
            retweetComment.CommentContent = addRetweetCommentDTO.CommentContent;
            retweetComment.CommentDateTime = DateTime.Now;
            retweetComment.UserId = addRetweetCommentDTO.UserId;
            retweetComment.RetweetId = addRetweetCommentDTO.RetweetId;
            return retweetComment;
        }
        public async Task<string> AddCommenttoRetweet(AddRetweetCommentDTO addRetweetCommentDTO)
        {
            try
            {
                RetweetComment AddedRetweetComment = MapAddRetweetCommentDTOToComment(addRetweetCommentDTO);
                var Retweetcomment = await _RetweetCommentRepository.Add(AddedRetweetComment);


                var RetweetDetails = await _RetweetRepository.GetbyKey(Retweetcomment.RetweetId);
                var RetweetOwnerDetails = await _UserRepository.GetbyKey(RetweetDetails.UserId);

                var CommentUserDetails = await _UserRepository.GetbyKey(Retweetcomment.UserId);
                var CommentedUsername = CommentUserDetails.UserName;
                UserNotification userNotification = new UserNotification();
                userNotification.UserId = RetweetOwnerDetails.Id;
                userNotification.NotificationPost = CommentUserDetails.UserProfileImgLink;
                userNotification.IsUserSeen = "No";
                userNotification.ContentDateTime = Retweetcomment.CommentDateTime;
                userNotification.TweetType = "Retweet";
                userNotification.TweetId = RetweetDetails.Id;
                userNotification.NotificatioContent = CommentedUsername + " Commented on your Retweet";
                var addedNotification = await _UserNotificationRepository.Add(userNotification);

                if (Retweetcomment != null)
                {
                    return "success";
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        // Function to Add Comment for a Retweet - Ends

        // Function to add Retweet-Comment Reply- Starts

        public RetweetCommentReply MapAddCommentReplyDTOToRetweetCommentReply(AddRetweetCommentReplyDTO addRetweetCommentReplyDTO)
        {
            RetweetCommentReply retweetCommentReply = new RetweetCommentReply();
            retweetCommentReply.ReplyContent = addRetweetCommentReplyDTO.ReplyContent;
            retweetCommentReply.ReplyDateTime = DateTime.Now;
            retweetCommentReply.ReplyType = "Comment";
            retweetCommentReply.UserId = addRetweetCommentReplyDTO.UserId;
            retweetCommentReply.RetweetCommentId = addRetweetCommentReplyDTO.Comment_ReplyId;
            return retweetCommentReply;
        }

        public async Task<string> AddRetweetCommentReply(AddRetweetCommentReplyDTO addRetweetCommentReplyDTO)
        {
            try
            {
                var reply = MapAddCommentReplyDTOToRetweetCommentReply(addRetweetCommentReplyDTO);
                var addedreply = await _RetweetCommentReplyRepository.Add(reply);

                var CommentDetails = await _RetweetCommentRepository.GetbyKey(addedreply.RetweetCommentId);
                var CommentOwnerDetails = await _UserRepository.GetbyKey(CommentDetails.UserId);

                var ReplyUserDetails = await _UserRepository.GetbyKey(addedreply.UserId);

                UserNotification userNotification = new UserNotification();
                userNotification.UserId = CommentOwnerDetails.Id;
                userNotification.NotificationPost = ReplyUserDetails.UserProfileImgLink;
                userNotification.IsUserSeen = "No";
                userNotification.ContentDateTime = addedreply.ReplyDateTime;
                userNotification.TweetType = "Retweet";
                userNotification.TweetId = CommentDetails.RetweetId;
                userNotification.NotificatioContent = ReplyUserDetails.UserName + " Replied to your Comment";
                var addedNotification = await _UserNotificationRepository.Add(userNotification);
                if (addedreply != null)
                {
                    return "success";
                }
                throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // Function to add Retweet-Comment Reply- Starts

        // Function to add Retweet-Reply to Reply- Starts
        public RetweetCommentReply MapRetweetCommentRepltotReply(AddRetweetCommentReplytoRelpy addRetweetCommentReplytoRelpy)
        {
            RetweetCommentReply retweetCommentReply1 = new RetweetCommentReply();
            retweetCommentReply1.ReplyContent = addRetweetCommentReplytoRelpy.ReplyContent;
            retweetCommentReply1.ReplyDateTime = DateTime.Now;
            retweetCommentReply1.ReplyType = "Reply";
            retweetCommentReply1.UserId = addRetweetCommentReplytoRelpy.UserId;
            retweetCommentReply1.RetweetCommentId = addRetweetCommentReplytoRelpy.RetweetCommentId;
            retweetCommentReply1.ReplyId = addRetweetCommentReplytoRelpy.ReweetCommentReplyId;
            return retweetCommentReply1;
        }

        public async Task<string> AddRetweetCommentReplyTOReply(AddRetweetCommentReplytoRelpy addRetweetCommentReplytoRelpy)
        {
            try
            {
                var reply = MapRetweetCommentRepltotReply(addRetweetCommentReplytoRelpy);
                var addedreply = await _RetweetCommentReplyRepository.Add(reply);
                if (addedreply != null)
                {
                    return "success";
                }
                throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        // Function to add Retweet-Reply to Reply- Ends

        public async Task<List<RetweetCommentDetailsReturnDTO>> ReturnRetweetComments(RetweetCommentDetails retweetCommentDetails)
        {
            try
            {
                var comments = await _RetweetCommentRepository.Get();
                List<RetweetCommentDetailsReturnDTO> rcommentDetailsReturnDTOs = new List<RetweetCommentDetailsReturnDTO>();
                foreach (var comment in comments)
                {
                    if (comment.RetweetId == retweetCommentDetails.RetweetId)
                    {
                        RetweetCommentDetailsReturnDTO commentDetailsReturnDTO1 = new RetweetCommentDetailsReturnDTO();
                        commentDetailsReturnDTO1.CommentId = comment.Id;
                        commentDetailsReturnDTO1.CommentContent = comment.CommentContent;
                        commentDetailsReturnDTO1.CommentDateTime = comment.CommentDateTime;
                        var userdetails = await _UserRepository.GetbyKey(comment.UserId);
                        commentDetailsReturnDTO1.UserId = comment.UserId;
                        commentDetailsReturnDTO1.UserName = userdetails.UserName;
                        commentDetailsReturnDTO1.PUserId = userdetails.UserId;
                        commentDetailsReturnDTO1.UserProfileLink = userdetails.UserProfileImgLink;

                        var commentlikescount1 = ((await _RetweetCommentLikesRepository.Get()).Where(l => l.RetweetCommentId == comment.Id)).ToList();
                        commentDetailsReturnDTO1.LikesCount = commentlikescount1.Count;

                        int flag1 = 0;
                        foreach (var like in commentlikescount1)
                        {
                            if (like.LikedUserId == retweetCommentDetails.UserId)
                            {
                                commentDetailsReturnDTO1.IsLikedByUser = "Yes";
                                flag1 = 1;
                                break;

                            }
                        }
                        if (flag1 == 0)
                        {
                            commentDetailsReturnDTO1.IsLikedByUser = "No";
                        }
                        //commentDetailsReturnDTOs.Add(commentDetailsReturnDTO);

                        List<RetweetReplyDetailsReturnDTO> ReplyList = new List<RetweetReplyDetailsReturnDTO>();
                        var replies = await _RetweetCommentRequestForRepliesRepository.GetbyKey(comment.Id);
                        var replies2 = replies.RetweetCommentReplies.ToList();
                        if (replies2.Count > 0)
                        {
                            foreach (var reply in replies2)
                            {
                                if (reply.ReplyId == 0)
                                {
                                    RetweetReplyDetailsReturnDTO replyDetailsReturnDTO1 = new RetweetReplyDetailsReturnDTO();
                                    replyDetailsReturnDTO1.Id = reply.Id;
                                    replyDetailsReturnDTO1.ReplyType = reply.ReplyType;
                                    replyDetailsReturnDTO1.ReplyContent = reply.ReplyContent;
                                    replyDetailsReturnDTO1.UserId = reply.UserId;
                                    replyDetailsReturnDTO1.ReplyDateTime = reply.ReplyDateTime;
                                    replyDetailsReturnDTO1.CommentId = reply.RetweetCommentId;
                                    var user = await _UserRepository.GetbyKey(reply.UserId);
                                    replyDetailsReturnDTO1.PUserId = user.UserId;
                                    replyDetailsReturnDTO1.UserName = user.UserName;
                                    replyDetailsReturnDTO1.UserProfileImageLink = user.UserProfileImgLink;

                                    var ReplyLikescount = ((await _RetweetCommentReplyLikesReposioryl.Get()).Where(l => l.ReplyCommentReplyId == reply.Id)).ToList();
                                    replyDetailsReturnDTO1.LikedCount = ReplyLikescount.Count;
                                    int flag2 = 0;

                                    foreach (var like in ReplyLikescount)
                                    {
                                        if (like.LikedUserId == retweetCommentDetails.UserId)
                                        {
                                            replyDetailsReturnDTO1.IsLikedByUser = "Yes";
                                            flag2 = 1;
                                            break;
                                        }
                                    }
                                    if (flag2 == 0)
                                    {
                                        replyDetailsReturnDTO1.IsLikedByUser = "No";
                                    }

                                    ReplyList.Add(replyDetailsReturnDTO1);

                                    if (ReplyList.Count > 0)
                                    {
                                        int flag = 0;
                                        while (flag == 0)
                                        {
                                            var lastreply = (await _RetweetCommentReplyRepository.Get()).Where(l => l.ReplyId == (ReplyList[ReplyList.Count - 1].Id)).ToList();
                                            if (lastreply.Count == 0)
                                            {
                                                flag = 1;
                                                break;
                                            }
                                            else
                                            {
                                                foreach (var r in lastreply)
                                                {
                                                    RetweetReplyDetailsReturnDTO replyDetailsReturnDTO3 = new RetweetReplyDetailsReturnDTO();
                                                    replyDetailsReturnDTO3.Id = r.Id;
                                                    replyDetailsReturnDTO3.ReplyType = r.ReplyType;
                                                    replyDetailsReturnDTO3.ReplyContent = r.ReplyContent;
                                                    replyDetailsReturnDTO3.UserId = r.UserId;
                                                    replyDetailsReturnDTO3.ReplyDateTime = r.ReplyDateTime;
                                                    replyDetailsReturnDTO3.CommentId = r.RetweetCommentId;
                                                    var user1 = await _UserRepository.GetbyKey(r.UserId);
                                                    replyDetailsReturnDTO3.PUserId = user1.UserId;
                                                    replyDetailsReturnDTO3.UserName = user1.UserName;
                                                    replyDetailsReturnDTO3.UserProfileImageLink = user1.UserProfileImgLink;

                                                    var ReplyLikescount8 = ((await _RetweetCommentReplyLikesReposioryl.Get()).Where(l => l.ReplyCommentReplyId == r.Id)).ToList();
                                                    replyDetailsReturnDTO3.LikedCount = ReplyLikescount8.Count;
                                                    int flag8 = 0;

                                                    foreach (var like in ReplyLikescount8)
                                                    {
                                                        if (like.LikedUserId == retweetCommentDetails.UserId)
                                                        {
                                                            replyDetailsReturnDTO3.IsLikedByUser = "Yes";
                                                            flag8 = 1;
                                                            break;
                                                        }
                                                    }
                                                    if (flag8 == 0)
                                                    {
                                                        replyDetailsReturnDTO3.IsLikedByUser = "No";
                                                    }
                                                    ReplyList.Add(replyDetailsReturnDTO3);
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }
                        commentDetailsReturnDTO1.Replies = ReplyList;
                        rcommentDetailsReturnDTOs.Add(commentDetailsReturnDTO1);
                    }
                }
                return rcommentDetailsReturnDTOs;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
