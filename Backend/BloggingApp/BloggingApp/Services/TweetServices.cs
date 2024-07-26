using Azure.Storage.Blobs;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using BloggingApp.Models.UserDTOs;
using BloggingApp.Repositories;
using BloggingApp.Repositories.TweetRequest;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;

namespace BloggingApp.Services
{
    public class TweetServices : ITweetServices
    {

        private readonly IRepository<int, Tweet> _TweetRepository;
        private readonly IRepository<int, TweetMentions> _TweetMentionsRepository;
        private readonly IRepository<int, TweetHashTags> _TweetHashTagsRepository;
        private readonly IRepository<int, HashTags> _HashTagsRepository;
        private readonly IRepository<int, User> _UserRepository;
        private readonly IRepository<int, TweetFiles> _TweetFilesRepository;
        private readonly IRepository<int, Retweet> _RetweetRepository;
        private readonly TweetRequestForTweetFilesRepository _TweetRequestForTweetFilesRepository; //To get Tweet Files for a Tweet

        public TweetServices(IRepository<int, Tweet> tweetRepository, IRepository<int, TweetMentions> tweetMentionsRepository, IRepository<int, User> userRepository,
            IRepository<int, TweetHashTags> tweetHashTagsRepository, IRepository<int,HashTags> hashTagsRepository,IRepository<int,TweetFiles> tweetFilesRepository,
            TweetRequestForTweetFilesRepository tweetRequestForTweetFiles, IRepository<int, Retweet> retweetRepository)
        {
            _TweetRepository = tweetRepository;
            _TweetMentionsRepository = tweetMentionsRepository;
            _UserRepository = userRepository;
            _TweetHashTagsRepository = tweetHashTagsRepository;
            _HashTagsRepository = hashTagsRepository;
            _TweetFilesRepository = tweetFilesRepository;
            _RetweetRepository = retweetRepository;
            _TweetRequestForTweetFilesRepository = tweetRequestForTweetFiles;
        }

        // Function to Add Tweet to database - Starts

        public Tweet MapAddTweetContentByUserToTweet(AddUserTweetContent addUserTweetContent)
        {
            Tweet tweet = new Tweet();
            tweet.TweetContent = addUserTweetContent.TweetContent;
            tweet.TweetDateTime = DateTime.Now;
            tweet.IsCommentEnable = addUserTweetContent.IsCommentEnable;
            tweet.UserId = addUserTweetContent.UserId;
            return tweet;
        }

        public async Task<string> AddMentionsHashTags(AddUserTweetContent addUserTweetContent,Tweet tweet)
        {
            List<string> mentions = addUserTweetContent.TweetMentions;
            foreach( string mention in mentions)
            {
                var UserResult = await _UserRepository.Get();
                foreach(var user in UserResult)
                {
                    if (user.UserId == mention) {
                        TweetMentions tweetMentions = new TweetMentions();
                        tweetMentions.MentionerId = user.Id;
                        tweetMentions.MentionedByUserId = addUserTweetContent.UserId;
                        tweetMentions.TweetId = tweet.Id;
                        tweetMentions.MentionedDateTime = DateTime.Now;
                        var AddedTweetMentions = await _TweetMentionsRepository.Add(tweetMentions);
                    }
                }
            }

            List<string> hashtags = addUserTweetContent.TweetHashtags;
            foreach( string hashtag in hashtags)
            {
                HashTags hashtag1 = new HashTags();
                hashtag1.HashTagTitle = hashtag;
                hashtag1.CountInPosts = 1;
                hashtag1.CountInComments = 0;
                hashtag1.TweetLikes = 0;
                hashtag1.HashTagCreatedDateTime = DateTime.Now;
                hashtag1.UserId = addUserTweetContent.UserId;
                var AddedHastag = await _HashTagsRepository.Add(hashtag1);

                TweetHashTags tweetHashTags = new TweetHashTags();
                tweetHashTags.TweetId = tweet.Id;
                tweetHashTags.HashTagTitle = hashtag;
                var AddedTweetHashtags = await _TweetHashTagsRepository.Add(tweetHashTags);
            }
            return "success";
        }

        public async Task<AddTweetContentReturnDTO> AddTweetContentByUser(AddUserTweetContent addUserTweetContent)
        {
            try
            {
                var MappedTweet = MapAddTweetContentByUserToTweet(addUserTweetContent);
                var AddedTweet = await _TweetRepository.Add(MappedTweet);
                if(addUserTweetContent.TweetMentions.Count > 0)
                {
                    string result1 = await AddMentionsHashTags(addUserTweetContent, AddedTweet);
                }
                AddTweetContentReturnDTO addTweetContentReturnDTO = new AddTweetContentReturnDTO();
                addTweetContentReturnDTO.TweetId = AddedTweet.Id;
                return addTweetContentReturnDTO;
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }

        // Function to Add Tweet to database - Ends

        // Function to Add TweetFiles to database - Starts

        public async Task<string> AddTweetFiles(TweetFiles tweetFiles)
        {
            try
            {
                var TweetFilesAdded = await _TweetFilesRepository.Add(tweetFiles);
                if(TweetFilesAdded != null)
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

        // Function to Add TweetFiles to database - Ends

        // Function to Provide Tweets - Starts(dummy)

        public async Task<RetweetsFeederResponseDTO> MapRetweetswihtLargeValues(Tweet tweet1, Retweet retweet)
        {
            var TweetFiles1 = await _TweetRequestForTweetFilesRepository.GetbyKey(tweet1.Id);
            var Files1 = TweetFiles1.TweetFiles.ToList();
            var user = await _UserRepository.GetbyKey(tweet1.UserId);
            RetweetsFeederResponseDTO retweetsFeederResponseDTO = new RetweetsFeederResponseDTO();
            retweetsFeederResponseDTO.TweetId = tweet1.Id;
            retweetsFeederResponseDTO.TweetContent = tweet1.TweetContent;
            retweetsFeederResponseDTO.TweetDateTime = tweet1.TweetDateTime;
            retweetsFeederResponseDTO.IsCommentEnable = tweet1.IsCommentEnable;
            retweetsFeederResponseDTO.UserId = tweet1.UserId;
            retweetsFeederResponseDTO.TweetOwnerUserName = user.UserName;
            retweetsFeederResponseDTO.TweetOwnerUserId = user.UserId;
            retweetsFeederResponseDTO.TweetOwnerProfileImgLink = user.UserProfileImgLink;
            if (Files1.Count == 1)
            {
                foreach (TweetFiles tweetfile in Files1)
                {
                    retweetsFeederResponseDTO.TweetFile1 = tweetfile.File1;
                    retweetsFeederResponseDTO.TweetFile2 = "null";

                }
            }
            else if (Files1.Count == 2)
            {
                foreach (TweetFiles tweetfile in Files1)
                {
                    retweetsFeederResponseDTO.TweetFile1 = tweetfile.File1;
                    retweetsFeederResponseDTO.TweetFile2 = tweetfile.File2;
                }
            }
            else
            {
                retweetsFeederResponseDTO.TweetFile1 = "null";
                retweetsFeederResponseDTO.TweetFile1 = "null";
            }
            retweetsFeederResponseDTO.RetweetId = retweet.Id;
            retweetsFeederResponseDTO.RetweetContent = retweet.RetweetContent;
            retweetsFeederResponseDTO.RetweetDateTime = retweet.RetweetDateTime;
            retweetsFeederResponseDTO.IsRetweetCommentEnable = retweet.IsCommentEnable;
            var retweetuserdetails = await _UserRepository.GetbyKey(retweet.UserId);
            retweetsFeederResponseDTO.RetweetUserName = retweetuserdetails.UserName;
            retweetsFeederResponseDTO.RetweetUserProfileImgLink = retweetuserdetails.UserProfileImgLink;
            retweetsFeederResponseDTO.RetweetUserId = retweetuserdetails.UserId;

            return retweetsFeederResponseDTO;

        }

        public async Task<FeedsPageReturnDTO> TweetsFeeder(int userid)
        {
            try
            {
                List<TweetsFeederDTO> tweetsFeederDTOs = new List<TweetsFeederDTO>();

                var Tweets = await _TweetRepository.Get();

                foreach(Tweet tweet in Tweets)
                {
                    var user = await _UserRepository.GetbyKey(tweet.UserId);
                    var TweetFiles = await _TweetRequestForTweetFilesRepository.GetbyKey(tweet.Id);
                    var Files = TweetFiles.TweetFiles.ToList();
                    TweetsFeederDTO tweetsFeederDTO = new TweetsFeederDTO();
                    tweetsFeederDTO.TweetId = tweet.Id;
                    tweetsFeederDTO.TweetContent = tweet.TweetContent;
                    tweetsFeederDTO.TweetDateTime = tweet.TweetDateTime;
                    tweetsFeederDTO.IsCommentEnable = tweet.IsCommentEnable;
                    tweetsFeederDTO.UserId = tweet.UserId;
                    tweetsFeederDTO.TweetOwnerUserName = user.UserName;
                    tweetsFeederDTO.TweetOwnerUserId = user.UserId;
                    tweetsFeederDTO.TweetOwnerProfileImgLink = user.UserProfileImgLink;

                    if(Files.Count == 1)
                    {
                        foreach (TweetFiles tweetfile in Files)
                        {
                            tweetsFeederDTO.TweetFile1 = tweetfile.File1;
                            tweetsFeederDTO.TweetFile2 = "null";

                        }
                    }else if(Files.Count == 2)
                    {
                        foreach (TweetFiles tweetfile in Files)
                        {
                            tweetsFeederDTO.TweetFile1 = tweetfile.File1;
                            tweetsFeederDTO.TweetFile2 = tweetfile.File2;
                        }
                    }
                    else
                    {
                        tweetsFeederDTO.TweetFile1 = "null";
                        tweetsFeederDTO.TweetFile2 = "null";
                    }
                    tweetsFeederDTOs.Add(tweetsFeederDTO);
                }

                // Retweets gathering starts
                List<RetweetsFeederResponseDTO> retweetsFeederResponseDTOs = new List<RetweetsFeederResponseDTO>();
                var Retweets = await _RetweetRepository.Get();

                foreach(var retweet in Retweets)
                {
                    var actutaltweetdetails = await _TweetRepository.GetbyKey(retweet.ActualTweetId);
                    var mappedretweets = await MapRetweetswihtLargeValues(actutaltweetdetails,retweet);
                    retweetsFeederResponseDTOs.Add(mappedretweets);
                }

                FeedsPageReturnDTO feedsPageReturnDTO = new FeedsPageReturnDTO();
                feedsPageReturnDTO.tweets = tweetsFeederDTOs;
                feedsPageReturnDTO.retweets = retweetsFeederResponseDTOs;


                if (tweetsFeederDTOs.Count > 0 && retweetsFeederResponseDTOs.Count>0)
                {
                    return feedsPageReturnDTO;
                }
                else
                {
                    throw new NoTweetsFoundException();
                }
            }
            catch (NoTweetsFoundException)
            {
                throw new NoTweetsFoundException();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // Function to Provide Tweets - Ends

        // Function to Add Retweets - Starts

        public Retweet MapAddRetweetDTOtoRetweet(AddRetweetDTO addRetweetDTO)
        {
            Retweet retweet = new Retweet();
            retweet.RetweetContent = addRetweetDTO.RetweetContent;
            retweet.RetweetDateTime = DateTime.Now;
            retweet.IsCommentEnable = addRetweetDTO.IsCommentEnable;
            retweet.ActualTweetId = addRetweetDTO.ActualTweetId;
            retweet.UserId = addRetweetDTO.UserId;
            return retweet;
        }

        public async Task<string> AddRetweetContent(AddRetweetDTO addRetweetDTO)
        {
            try
            {
                var retweet = MapAddRetweetDTOtoRetweet(addRetweetDTO);
                var AddedRetweet = await _RetweetRepository.Add(retweet);
                if(AddedRetweet != null)
                {
                    return "success";
                }
                throw new Exception();
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }

        // Function to Add Retweets - Ends
    }
}
