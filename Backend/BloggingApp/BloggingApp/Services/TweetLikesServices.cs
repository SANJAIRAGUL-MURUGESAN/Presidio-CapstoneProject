using BloggingApp.Interfaces;
using BloggingApp.Models;
using BloggingApp.Repositories.TweetRequest;
using BloggingApp.Repositories;
using BloggingApp.Models.UserDTOs;
using BloggingApp.Exceptions.TweetLikesRepository;

namespace BloggingApp.Services
{
    public class TweetLikesServices : ITweetLikesServices
    {
        private readonly IRepository<int, TweetLikes> _TweetLikesRepository;
        private readonly IRepository<int, RetweetLikes> _RetweetLikesRepository;
        public TweetLikesServices(IRepository<int, TweetLikes> tweetLikesRepository, IRepository<int, RetweetLikes> reTweetLikesRepository)
        {
            _TweetLikesRepository = tweetLikesRepository;
            _RetweetLikesRepository = reTweetLikesRepository;
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
    }
}
