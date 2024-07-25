using Azure.Storage.Blobs;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using BloggingApp.Models.UserDTOs;
using BloggingApp.Repositories;
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

        public TweetServices(IRepository<int, Tweet> tweetRepository, IRepository<int, TweetMentions> tweetMentionsRepository, IRepository<int, User> userRepository,
            IRepository<int, TweetHashTags> tweetHashTagsRepository, IRepository<int,HashTags> hashTagsRepository)
        {
            _TweetRepository = tweetRepository;
            _TweetMentionsRepository = tweetMentionsRepository;
            _UserRepository = userRepository;
            _TweetHashTagsRepository = tweetHashTagsRepository;
            _HashTagsRepository = hashTagsRepository;
        }

        // Function to Add Tweet to database - starts

        public Tweet MapAddTweetContentByUserToTweet(AddUserTweetContent addUserTweetContent)
        {
            Tweet tweet = new Tweet();
            tweet.TweetContent = addUserTweetContent.TweetContent;
            tweet.TweetDateTime = DateTime.Now;
            tweet.IsCommentEnable = addUserTweetContent.IsCommentEnable;
            tweet.RepostTweetId = 0;
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

        // Function to Add Tweet to database - ends
    }
}
