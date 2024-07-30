using BloggingApp.Interfaces;
using BloggingApp.Models;
using BloggingApp.Repositories.TweetRequest;
using BloggingApp.Repositories;
using BloggingApp.Models.UserDTOs;
using BloggingApp.Exceptions.TweetLikesRepository;
using BloggingApp.Models.NewFolder;
using BloggingApp.Models.CRLikesDTOs;
using BloggingApp.Exceptions.TweeCommentLikesRepository;
using BloggingApp.Exceptions.RetweetCommentLikesExceptions;

namespace BloggingApp.Services
{
    public class TweetLikesServices : ITweetLikesServices
    {
        private readonly IRepository<int, TweetLikes> _TweetLikesRepository;
        private readonly IRepository<int, RetweetLikes> _RetweetLikesRepository;

        private readonly IRepository<int, TweetCommentLikes> _TweetCommentLikesRepository;
        private readonly IRepository<int, TweetReplyLikes> _TweetReplyLikesRepository;

        private readonly IRepository<int, RetweetCommentLikes> _RetweetCommentLikesRepository;
        private readonly IRepository<int, RetweetCommentReplyLikes> _RetweetCommentReplyLikesRepository;


        public TweetLikesServices(IRepository<int, TweetLikes> tweetLikesRepository, IRepository<int, RetweetLikes> reTweetLikesRepository,
            IRepository<int, TweetCommentLikes> tweetcommentLikesRepository,IRepository<int, TweetReplyLikes> tweetcommentReplyLikesRepository,
            IRepository<int, RetweetCommentLikes> retweetcommentLikesRepository, IRepository<int, RetweetCommentReplyLikes> retweetcommentReplyLikesRepository)
        {
            _TweetLikesRepository = tweetLikesRepository;
            _RetweetLikesRepository = reTweetLikesRepository;
            _TweetCommentLikesRepository = tweetcommentLikesRepository;
            _TweetReplyLikesRepository = tweetcommentReplyLikesRepository;
            _RetweetCommentLikesRepository = retweetcommentLikesRepository;
            _RetweetCommentReplyLikesRepository = retweetcommentReplyLikesRepository;
        }

        // Function to Add Tweet Likes to database - Starts

        public TweetLikes MapTweetLikeDTOtoTweetLike(AddTweetLikesDTO addTweetLikesDTO)
        {
            TweetLikes tweetLikes = new TweetLikes();
            tweetLikes.LikedUserId = addTweetLikesDTO.LikedUserId;
            tweetLikes.TweetId = addTweetLikesDTO.TweetId;
            return tweetLikes;
        }
        public async Task<string> AddTweetLikes(AddTweetLikesDTO addTweetLikesDTO)
        {
            try
            {
                var tweetlike = MapTweetLikeDTOtoTweetLike(addTweetLikesDTO);
                var addedtweetlike = await _TweetLikesRepository.Add(tweetlike);
                if(addedtweetlike != null)
                {
                    return "success";
                }
                else
                {
                    throw new NoSuchTweetLikeFoundException();
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // Function to Add Tweet Likes to database - Ends

        // Function to Add Retweet Likes to database - Starts

        public RetweetLikes MapTweetLikeDTOtoReTweetLike(AddRetweekLikeDTO addRetweekLikeDTO)
        {
            RetweetLikes retweetLikes = new RetweetLikes();
            retweetLikes.LikedUserId = addRetweekLikeDTO.LikedUserId;
            retweetLikes.RetweetId = addRetweekLikeDTO.RetweetId;
            return retweetLikes;
        }

        public async Task<string> AddRetweetLikes(AddRetweekLikeDTO addRetweekLikeDTO)
        {
            try
            {
                var tweetlike = MapTweetLikeDTOtoReTweetLike(addRetweekLikeDTO);
                var addedtweetlike = await _RetweetLikesRepository.Add(tweetlike);
                if (addedtweetlike != null)
                {
                    return "success";
                }
                else
                {
                    throw new NoSuchTweetLikeFoundException();
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        // Function to Add Retweet Likes to database - Ends

        // Function to Add Tweet Dislikes to database - Starts

        public async Task<string> AddTweetDisLikes(AddTweetDislikeDTO addTweetDislikeDTO)
        {
            try
            {
                var dislikelike = (await _TweetLikesRepository.Get()).Where(l => l.TweetId == addTweetDislikeDTO.TweetId);
                if (dislikelike != null)
                {
                    foreach (TweetLikes tweetLikes in dislikelike)
                    {
                        if(tweetLikes.LikedUserId == addTweetDislikeDTO.LikedUserId)
                        {
                            var deltedtweetlike = await _TweetLikesRepository.Delete(tweetLikes.Id);
                            return "success";
                        }
                    }
                }
                throw new NoSuchTweetLikeFoundException();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // Function to Add Tweet Dislikes to database - Ends

        // Function to Add Retweet Dislikes to database - Starts

        public async Task<string> AddRetweetDisLikes(AddRetweetDislikeDTO addRetweetDislikeDTO)
        {
            try
            {
                var dislikelike = (await _RetweetLikesRepository.Get()).Where(l => l.RetweetId == addRetweetDislikeDTO.RetweetId);
                if (dislikelike != null)
                {
                    foreach (RetweetLikes retweetLikes in dislikelike)
                    {
                        if (retweetLikes.LikedUserId == addRetweetDislikeDTO.LikedUserId)
                        {
                            var deltedtweetlike = await _RetweetLikesRepository.Delete(retweetLikes.Id);
                            return "success";
                        }
                    }
                }
                throw new NoSuchTweetLikeFoundException();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // Function to Add Retweet Dislikes to database - Ends

        // Commenst and Replies(Likes) starts

        // Function to Add Like to a Tweet Comment - Starts

        public TweetCommentLikes MapTweetCommentLikeDTOtoTweetCommentLike(AddTweetCommentLikesDTO addTweetCommentLikesDTO)
        {
            TweetCommentLikes tweetCommentLikes = new TweetCommentLikes();
            tweetCommentLikes.LikedUserId = addTweetCommentLikesDTO.LikedUserid;
            tweetCommentLikes.CommentId = addTweetCommentLikesDTO.CommentId;
            return tweetCommentLikes;
        }
        public async Task<string> AddTweetCommentLikes(AddTweetCommentLikesDTO addTweetCommentLikesDTO)
        {
            try
            {
                var tweetcommentlike = MapTweetCommentLikeDTOtoTweetCommentLike(addTweetCommentLikesDTO);
                var addedtweetcommentlike = await _TweetCommentLikesRepository.Add(tweetcommentlike);
                if (addedtweetcommentlike != null)
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
        // Function to Add Like to a Tweet Comment - Ends

        // Function to Add  Dislike to a Tweet Comment - Starts
        public async Task<string> AddTweetCommentDislike(AddTweetCommentDislikeDTO addTweetCommentDislikeDTO)
        {
            try
            {
                var dislikelike = (await _TweetCommentLikesRepository.Get()).Where(l => l.CommentId == addTweetCommentDislikeDTO.CommentId);
                if (dislikelike != null)
                {
                    foreach (var like in dislikelike)
                    {
                        if (like.LikedUserId == addTweetCommentDislikeDTO.LikedUserid)
                        {
                            var deltedtweetlike = await _TweetCommentLikesRepository.Delete(like.Id);
                            return "success";
                        }
                    }
                }
                throw new NoSuchTweetCommentLikeFoundException();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // Function to Add  Dislike to a Tweet Comment - Ends

        // Function to Add  Like to a Tweet Comment Reply - Starts

        public TweetReplyLikes MapTweetCommentReplyLikeDTOtoTweetCommentReplyLike(AddTweetCommentReplyLikeDTO addTweetCommentReplyLikeDTO)
        {
            TweetReplyLikes tweetReplyLikes = new TweetReplyLikes();
            tweetReplyLikes.LikedUserId = addTweetCommentReplyLikeDTO.LikedUserId;
            tweetReplyLikes.ReplyId = addTweetCommentReplyLikeDTO.ReplyId;
            return tweetReplyLikes;
        }
        public async Task<string> AddTweetCommentReplyLikes(AddTweetCommentReplyLikeDTO addTweetCommentReplyLikeDTO)
        {
            try
            {
                var tweetcommentlike = MapTweetCommentReplyLikeDTOtoTweetCommentReplyLike(addTweetCommentReplyLikeDTO);
                var addedtweetcommentlike = await _TweetReplyLikesRepository.Add(tweetcommentlike);
                if (addedtweetcommentlike != null)
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
        // Function to Add  Like to a Tweet Comment Reply - Starts

        // Function to Add  Dislike to a Tweet Comment Reply- Starts
        public async Task<string> AddTweetCommentReplyDislike(AddTweetCommentReplyDislikeDTO addTweetCommentReplyDislikeDTO)
        {
            try
            {
                var dislikelike = (await _TweetReplyLikesRepository.Get()).Where(l => l.ReplyId == addTweetCommentReplyDislikeDTO.ReplyId);
                if (dislikelike != null)
                {
                    foreach (var like in dislikelike)
                    {
                        if (like.LikedUserId == addTweetCommentReplyDislikeDTO.LikedUserId)
                        {
                            var deltedtweetlike = await _TweetReplyLikesRepository.Delete(like.Id);
                            return "success";
                        }
                    }
                }
                throw new NoSuchTweetCommentLikeFoundException();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // Function to Add  Dislike to a Tweet Comment Reply - Ends

        // Retweets

        // Function to Add Like to a Retweet Comment - Starts

        public RetweetCommentLikes MapRetweetCommentLikeDTOtoTweetCommentLike(AddRetweetCommentLikeDTO addTweetCommentLikesDTO)
        {
            RetweetCommentLikes retweetCommentLikes = new RetweetCommentLikes();
            retweetCommentLikes.LikedUserId = addTweetCommentLikesDTO.LikedUserid;
            retweetCommentLikes.RetweetCommentId = addTweetCommentLikesDTO.RetweetCommentId;
            return retweetCommentLikes;
        }
        public async Task<string> AddRetweetCommentLikes(AddRetweetCommentLikeDTO addRetweetCommentLikeDTO)
        {
            try
            {
                var tweetcommentlike = MapRetweetCommentLikeDTOtoTweetCommentLike(addRetweetCommentLikeDTO);
                var addedtweetcommentlike = await _RetweetCommentLikesRepository.Add(tweetcommentlike);
                if (addedtweetcommentlike != null)
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
        // Function to Add Like to a Retweet Comment - Ends

        // Function to Add  Dislike to a Reweet Comment - Starts
        public async Task<string> AddRetweetCommentDislike(AddRetweetCommentDislikeDTO addRetweetCommentDislikeDTO)
        {
            try
            {
                var dislikelike = (await _RetweetCommentLikesRepository.Get()).Where(l => l.RetweetCommentId == addRetweetCommentDislikeDTO.RetweetCommentId);
                if (dislikelike != null)
                {
                    foreach (var like in dislikelike)
                    {
                        if (like.LikedUserId == addRetweetCommentDislikeDTO.LikedUserid)
                        {
                            var deltedtweetlike = await _RetweetCommentLikesRepository.Delete(like.Id);
                            return "success";
                        }
                    }
                }
                throw new NoSuchRetweetCommentLikeFoundException();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // Function to Add  Dislike to a Reweet Comment - Ends

        // Function to Add Like to a Retweet Comment Reply Like- Starts

        public RetweetCommentReplyLikes MapRetweetCommentReplyLikeDTOtoTweetCommentLike(AddRetweetCommentReplyLikeDTO addRetweetCommentReplyLikeDTO)
        {
            RetweetCommentReplyLikes retweetCommentReplyLikes = new RetweetCommentReplyLikes();
            retweetCommentReplyLikes.LikedUserId = addRetweetCommentReplyLikeDTO.LikedUserId;
            retweetCommentReplyLikes.ReplyCommentReplyId = addRetweetCommentReplyLikeDTO.ReplyCommentReplyId;
            return retweetCommentReplyLikes;
        }
        public async Task<string> AddRetweetCommentReplyLikes(AddRetweetCommentReplyLikeDTO addRetweetCommentReplyLikeDTO)
        {
            try
            {
                var tweetcommentlike = MapRetweetCommentReplyLikeDTOtoTweetCommentLike(addRetweetCommentReplyLikeDTO);
                var addedtweetcommentlike = await _RetweetCommentReplyLikesRepository.Add(tweetcommentlike);
                if (addedtweetcommentlike != null)
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
        // Function to Add Like to a Retweet Reply Like- Ends

        // Function to Add Dislike to a Reweet Comment Reply - Starts
        public async Task<string> AddRetweetCommentReplyDislike(AddRetweetCommentReplyDislikeDTO retweetCommentReplyDislikeDTO)
        {
            try
            {
                var dislikelike = (await _RetweetCommentReplyLikesRepository.Get()).Where(l => l.ReplyCommentReplyId == retweetCommentReplyDislikeDTO.ReplyCommentReplyId);
                if (dislikelike != null)
                {
                    foreach (var like in dislikelike)
                    {
                        if (like.LikedUserId == retweetCommentReplyDislikeDTO.LikedUserId)
                        {
                            var deltedtweetlike = await _RetweetCommentReplyLikesRepository.Delete(like.Id);
                            return "success";
                        }
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // Function to Add  Dislike to a Reweet Comment Reply - Ends
    }
}
