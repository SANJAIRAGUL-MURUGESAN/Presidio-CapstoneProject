using Azure.Storage.Blobs;
using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using BloggingApp.Models.UserDTOs;
using BloggingApp.Repositories;
using BloggingApp.Repositories.CommentRequest;
using BloggingApp.Repositories.TweetRequest;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

namespace BloggingApp.Services
{
    public class TweetServices : ITweetServices
    {
        private readonly IRepository<int, Comment> _TweetCommentRepository;
        private readonly IRepository<int, Reply> _TweetReplyRepository;
        private readonly IRepository<int, RetweetComment> _RetweetCommentRepository;
        private readonly IRepository<int, RetweetCommentReply> _RetweetCommentReplyRepository;
        private readonly IRepository<int, Tweet> _TweetRepository;
        private readonly IRepository<int, TweetMentions> _TweetMentionsRepository;
        private readonly IRepository<int, TweetHashTags> _TweetHashTagsRepository;
        private readonly IRepository<int, HashTags> _HashTagsRepository;
        private readonly IRepository<int, User> _UserRepository;
        private readonly IRepository<int, TweetFiles> _TweetFilesRepository;
        private readonly IRepository<int, Retweet> _RetweetRepository;
        private readonly IRepository<int, TweetLikes> _TweetLikesRepository;
        private readonly IRepository<int, RetweetLikes> _RetweetLikesRepository;
        private readonly IRepository<int, RetweetMentions> _RetweetMentionsRepository;
        private readonly IRepository<int, RetweetHashTags> _RetweetHashTagsRepository;
        private readonly IRepository<int, UserNotification> _UserNotificationRepository;
        private readonly IRepository<int, Follow> _FollowRepository;
        private readonly TweetRequestForTweetFilesRepository _TweetRequestForTweetFilesRepository; //To get Tweet Files for a Tweet

        public TweetServices(IRepository<int, Tweet> tweetRepository, IRepository<int, TweetMentions> tweetMentionsRepository, IRepository<int, User> userRepository,
            IRepository<int, TweetHashTags> tweetHashTagsRepository, IRepository<int,HashTags> hashTagsRepository,IRepository<int,TweetFiles> tweetFilesRepository,
            TweetRequestForTweetFilesRepository tweetRequestForTweetFiles, IRepository<int, Retweet> retweetRepository, IRepository<int, TweetLikes> tweetLikesRepository,
            IRepository<int, RetweetLikes> reTweetLikesRepository, IRepository<int, RetweetMentions> retweetMentionsRepository,IRepository<int, RetweetHashTags> retweetHashTagsRepository,
            IRepository<int, UserNotification> userNotificationRepository, IRepository<int, Follow> followRepository, IRepository<int, Comment> commentRepository, IRepository<int, Reply> replyRepository,
            IRepository<int, RetweetComment> retweetCommentRepository, IRepository<int, RetweetCommentReply> retweetCommentReplyRepository)
        {
            _TweetRepository = tweetRepository;
            _TweetMentionsRepository = tweetMentionsRepository;
            _UserRepository = userRepository;
            _TweetHashTagsRepository = tweetHashTagsRepository;
            _HashTagsRepository = hashTagsRepository;
            _TweetFilesRepository = tweetFilesRepository;
            _RetweetRepository = retweetRepository;
            _TweetRequestForTweetFilesRepository = tweetRequestForTweetFiles;
            _TweetLikesRepository = tweetLikesRepository;
            _RetweetLikesRepository = reTweetLikesRepository;
            _RetweetMentionsRepository = retweetMentionsRepository;
            _RetweetHashTagsRepository = retweetHashTagsRepository;
            _UserNotificationRepository = userNotificationRepository;
            _FollowRepository = followRepository;
            _TweetCommentRepository = commentRepository;
            _TweetReplyRepository = replyRepository;
            _RetweetCommentRepository = retweetCommentRepository;
            _RetweetCommentReplyRepository = retweetCommentReplyRepository;
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

                        var userdetails = await _UserRepository.GetbyKey(tweet.UserId);
                        var username = userdetails.UserName;
                        UserNotification userNotificationm = new UserNotification();
                        userNotificationm.UserId = user.Id;
                        userNotificationm.NotificationPost = userdetails.UserProfileImgLink;
                        userNotificationm.IsUserSeen = "No";
                        userNotificationm.ContentDateTime = DateTime.Now;
                        userNotificationm.TweetType = "Tweet";
                        userNotificationm.TweetId = tweet.Id;
                        userNotificationm.NotificatioContent = username + " Mentioned you in a Post";
                        var addedNotification = await _UserNotificationRepository.Add(userNotificationm);
                    }
                }
            }
            return "success";
           
        }

        public async Task<string> AddHashtags(AddUserTweetContent addUserTweetContent, Tweet tweet)
        {
            List<string> hashtags = addUserTweetContent.TweetHashtags;
            foreach (string hashtag in hashtags)
            {
                HashTags hashtag1 = new HashTags();
                hashtag1.HashTagTitle = hashtag;
                hashtag1.CountInPosts = 1;
                hashtag1.CountInComments = 0;
                hashtag1.TweetLikes = 0;
                hashtag1.HashTagCreatedDateTime = tweet.TweetDateTime;
                hashtag1.UserId = addUserTweetContent.UserId;
                var AddedHastag = await _HashTagsRepository.Add(hashtag1);

                TweetHashTags tweetHashTags = new TweetHashTags();
                tweetHashTags.TweetId = tweet.Id;
                tweetHashTags.HashTagTitle = hashtag;
                var AddedTweetHashtags = await _TweetHashTagsRepository.Add(tweetHashTags);
            }
            return "success";
        }
        public async Task<string> AddTweetNotification(AddUserTweetContent addUserTweetContent, Tweet tweet)
        {
            var followers = (await _FollowRepository.Get()).Where(f => f.FollowerId == tweet.UserId);
            var userdetails = await _UserRepository.GetbyKey(tweet.UserId);
            var username = userdetails.UserName;
            foreach (var followeruser in followers) {
                UserNotification userNotification = new UserNotification();
                userNotification.UserId = followeruser.UserId;
                userNotification.NotificationPost = userdetails.UserProfileImgLink;
                userNotification.IsUserSeen = "No";
                userNotification.ContentDateTime = tweet.TweetDateTime;
                userNotification.TweetType = "Tweet";
                userNotification.TweetId = tweet.Id;
                userNotification.NotificatioContent = username + " Posted a Tweet";
                var addedNotification = await _UserNotificationRepository.Add(userNotification);
            }
            return "success";
        }
        public async Task<AddTweetContentReturnDTO> AddTweetContentByUser(AddUserTweetContent addUserTweetContent)
        {
            try
            {
                var MappedTweet = MapAddTweetContentByUserToTweet(addUserTweetContent);
                Console.WriteLine("counts",addUserTweetContent.TweetMentions.Count);
                var AddedTweet = await _TweetRepository.Add(MappedTweet);
                if (addUserTweetContent.TweetMentions.Count > 0)
                {
                    string result1 = await AddMentionsHashTags(addUserTweetContent, AddedTweet);
                }
                if (addUserTweetContent.TweetHashtags.Count > 0)
                {
                    string result1 = await AddHashtags(addUserTweetContent, AddedTweet);
                }
                string addednotification = await AddTweetNotification(addUserTweetContent, AddedTweet);
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

        public async Task<RetweetsFeederResponseDTO> MapRetweetswihtLargeValues(Tweet tweet1, Retweet retweet, int userid)
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
            var RetweetLikes = ((await _RetweetLikesRepository.Get()).Where(l => l.RetweetId == retweet.Id)).ToList();

            var retweetcomments = ((await _RetweetCommentRepository.Get()).Where(c => c.RetweetId == retweet.Id)).ToList();
            retweetsFeederResponseDTO.CommentsCount = retweetcomments.Count;

            foreach (var c in retweetcomments)
            {
                var replies = ((await _RetweetCommentReplyRepository.Get()).Where(r => r.RetweetCommentId == c.Id)).ToList();
                retweetsFeederResponseDTO.CommentsCount = retweetsFeederResponseDTO.CommentsCount + replies.Count;

            }

            var IsUSerLikedRetweet = (await _RetweetLikesRepository.Get()).Where(l => l.RetweetId == retweet.Id && l.LikedUserId == userid).ToList();
            if(IsUSerLikedRetweet.Count > 0)
            {
                retweetsFeederResponseDTO.IsRetweetLikedByUser = "Yes";
            }
            else
            {
                retweetsFeederResponseDTO.IsRetweetLikedByUser = "No";
            }
            retweetsFeederResponseDTO.RetweetLikesCount = RetweetLikes.Count;

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
                    var TweetLikes = ((await _TweetLikesRepository.Get()).Where(l => l.TweetId == tweet.Id)).ToList();
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
                    tweetsFeederDTO.TweetLikesCount = TweetLikes.Count;

                    var tweetcomments = ((await _TweetCommentRepository.Get()).Where(c => c.TweetId == tweet.Id)).ToList();
                    tweetsFeederDTO.CommentsCount = tweetcomments.Count;

                    foreach(var c in tweetcomments)
                    {
                        var replies = ((await _TweetReplyRepository.Get()).Where(r=>r.CommentId==c.Id)).ToList();
                        tweetsFeederDTO.CommentsCount = tweetsFeederDTO.CommentsCount + replies.Count;

                    }


                    var Retweetscount = ((await _RetweetRepository.Get()).Where(r => r.ActualTweetId == tweet.Id)).ToList();
                    tweetsFeederDTO.RetweetsCount = Retweetscount.Count;

                    var IsTweetLikedByUser = ((await _TweetLikesRepository.Get()).Where(l => l.TweetId == tweet.Id && l.LikedUserId == userid)).ToList();
                    if(IsTweetLikedByUser.Count > 0)
                    {
                        tweetsFeederDTO.IsTweetLikedByUser = "Yes";
                    }
                    else
                    {
                        tweetsFeederDTO.IsTweetLikedByUser = "No";
                    }

                    if (Files.Count == 1)
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
                    var mappedretweets = await MapRetweetswihtLargeValues(actutaltweetdetails,retweet,userid);
                    retweetsFeederResponseDTOs.Add(mappedretweets);
                }

                FeedsPageReturnDTO feedsPageReturnDTO = new FeedsPageReturnDTO();
                feedsPageReturnDTO.tweets = tweetsFeederDTOs;
                feedsPageReturnDTO.retweets = retweetsFeederResponseDTOs;


                if (tweetsFeederDTOs.Count > 0 || retweetsFeederResponseDTOs.Count > 0)
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
        public async Task<string> AddRetweetMentions(AddRetweetDTO addUserRetweetContent, Retweet retweet)
        {
            List<string> mentions = addUserRetweetContent.RetweetMentions;
            foreach (string mention in mentions)
            {
                var UserResult = await _UserRepository.Get();
                foreach (var user in UserResult)
                {
                    if (user.UserId == mention)
                    {
                        RetweetMentions retweetMentions = new RetweetMentions();
                        retweetMentions.MentionerId = user.Id;
                        retweetMentions.MentionedByUserId = addUserRetweetContent.UserId;
                        retweetMentions.RetweetId = retweet.Id;
                        retweetMentions.MentionedDateTime = DateTime.Now;
                        var AddedTweetMentions = await _RetweetMentionsRepository.Add(retweetMentions);

                        var userdetails = await _UserRepository.GetbyKey(retweet.UserId);
                        var username = userdetails.UserName;
                        UserNotification userNotificationrm = new UserNotification();
                        userNotificationrm.UserId = user.Id;
                        userNotificationrm.NotificationPost = userdetails.UserProfileImgLink;
                        userNotificationrm.IsUserSeen = "No";
                        userNotificationrm.ContentDateTime = retweet.RetweetDateTime;
                        userNotificationrm.TweetType = "Retweet";
                        userNotificationrm.TweetId = retweet.Id;
                        userNotificationrm.NotificatioContent = username + " Mentioned you in a Retweet";
                        var addedNotification = await _UserNotificationRepository.Add(userNotificationrm);
                    }
                }
            }
            return "success";

        }

        public async Task<string> AddRetweetHashtags(AddRetweetDTO addUserTweetContent, Retweet retweet)
        {
            List<string> hashtags = addUserTweetContent.RetweetHashTags;
            foreach (string hashtag in hashtags)
            {
                HashTags hashtag1 = new HashTags();
                hashtag1.HashTagTitle = hashtag;
                hashtag1.CountInPosts = 1;
                hashtag1.CountInComments = 0;
                hashtag1.TweetLikes = 0;
                hashtag1.HashTagCreatedDateTime = DateTime.Now;
                hashtag1.UserId = addUserTweetContent.UserId;
                var AddedHastag = await _HashTagsRepository.Add(hashtag1);

                RetweetHashTags retweetHashTags = new RetweetHashTags();
                retweetHashTags.RetweetId = retweet.Id;
                retweetHashTags.HashTagTitle = hashtag;
                var AddedTweetHashtags = await _RetweetHashTagsRepository.Add(retweetHashTags);
            }
            return "success";
        }

        public async Task<string> AddRetweetNotification(AddRetweetDTO addRetweetDTO, Retweet AddedRetweet)
        {
            var followers = (await _FollowRepository.Get()).Where(f => f.FollowerId == AddedRetweet.UserId);
            var userdetails = await _UserRepository.GetbyKey(AddedRetweet.UserId);
            var username = userdetails.UserName;
            foreach (var followeruser in followers)
            {
                UserNotification userNotification = new UserNotification();
                userNotification.UserId = followeruser.UserId;
                userNotification.NotificationPost = userdetails.UserProfileImgLink;
                userNotification.IsUserSeen = "No";
                userNotification.ContentDateTime = AddedRetweet.RetweetDateTime;
                userNotification.TweetType = "Retweet";
                userNotification.TweetId = AddedRetweet.Id;
                userNotification.NotificatioContent = username + " Reposted a Tweet";
                var addedNotification = await _UserNotificationRepository.Add(userNotification);
            }
            return "success";

        }

        public async Task<string> AddRetweetContent(AddRetweetDTO addRetweetDTO)
        {
            try
            {
                var retweet = MapAddRetweetDTOtoRetweet(addRetweetDTO);
                var AddedRetweet = await _RetweetRepository.Add(retweet);
                if(addRetweetDTO.RetweetMentions.Count > 0)
                {
                    var addedmention = await AddRetweetMentions(addRetweetDTO,AddedRetweet);
                }
                if(addRetweetDTO.RetweetHashTags.Count > 0)
                {
                    var addedmention = await AddRetweetHashtags(addRetweetDTO, AddedRetweet);
                }
                var addednoti = await AddRetweetNotification(addRetweetDTO, AddedRetweet);
                if (AddedRetweet != null)
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

        // Function to Return TweetDetails - Starts

        public async Task<TweetDetailsReturnDTO> TweetDetailsFeeder(TweetDetailsDTO tweetDetailsDTO)
        {
            try
            {
                if(tweetDetailsDTO.TweetType == "Tweet")
                {
                    var tweet = await _TweetRepository.GetbyKey(tweetDetailsDTO.TweetId);
                    var user = await _UserRepository.GetbyKey(tweet.UserId);
                    if(tweet != null)
                    {
                        var TweetFiles = await _TweetRequestForTweetFilesRepository.GetbyKey(tweetDetailsDTO.TweetId);
                        var TweetLikes = ((await _TweetLikesRepository.Get()).Where(l => l.TweetId == tweet.Id)).ToList();
                        var Files = TweetFiles.TweetFiles.ToList();
                        TweetDetailsReturnDTO tweetDetailsReturnDTO = new TweetDetailsReturnDTO();
                        tweetDetailsReturnDTO.TweetId = tweet.Id;
                        tweetDetailsReturnDTO.TweetContent = tweet.TweetContent;
                        tweetDetailsReturnDTO.TweetDateTime = tweet.TweetDateTime;
                        tweetDetailsReturnDTO.IsCommentEnable = tweet.IsCommentEnable;
                        tweetDetailsReturnDTO.UserId = tweet.UserId;
                        tweetDetailsReturnDTO.TweetOwnerUserName = user.UserName;
                        tweetDetailsReturnDTO.TweetOwnerUserId = user.UserId;
                        tweetDetailsReturnDTO.TweetOwnerProfileImgLink = user.UserProfileImgLink;
                        tweetDetailsReturnDTO.TweetLikesCount = TweetLikes.Count;

                        var tweetcomments = ((await _TweetCommentRepository.Get()).Where(c => c.TweetId == tweet.Id)).ToList();
                        tweetDetailsReturnDTO.CommentsCount = tweetcomments.Count;

                        foreach (var c in tweetcomments)
                        {
                            var replies = ((await _TweetReplyRepository.Get()).Where(r => r.CommentId == c.Id)).ToList();
                            tweetDetailsReturnDTO.CommentsCount = tweetDetailsReturnDTO.CommentsCount + replies.Count;

                        }

                        var Retweetscount = ((await _RetweetRepository.Get()).Where(r => r.ActualTweetId == tweet.Id)).ToList();
                        tweetDetailsReturnDTO.RetweetsCount = Retweetscount.Count;

                        var IsTweetLikedByUser = ((await _TweetLikesRepository.Get()).Where(l => l.TweetId == tweet.Id && l.LikedUserId == tweetDetailsDTO.UserId)).ToList();
                        if (IsTweetLikedByUser.Count > 0)
                        {
                            tweetDetailsReturnDTO.IsTweetLikedByUser = "Yes";
                        }
                        else
                        {
                            tweetDetailsReturnDTO.IsTweetLikedByUser = "No";
                        }

                        if (Files.Count == 1)
                        {
                            foreach (TweetFiles tweetfile in Files)
                            {
                                tweetDetailsReturnDTO.TweetFile1 = tweetfile.File1;
                                tweetDetailsReturnDTO.TweetFile2 = "null";

                            }
                        }
                        else if (Files.Count == 2)
                        {
                            foreach (TweetFiles tweetfile in Files)
                            {
                                tweetDetailsReturnDTO.TweetFile1 = tweetfile.File1;
                                tweetDetailsReturnDTO.TweetFile2 = tweetfile.File2;
                            }
                        }
                        else
                        {
                            tweetDetailsReturnDTO.TweetFile1 = "null";
                            tweetDetailsReturnDTO.TweetFile2 = "null";
                        }
                        return tweetDetailsReturnDTO;
                    }
                    else
                    {
                        throw new NoTweetsFoundException();
                    }
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
        // Function to Return TweetDetails - Ends


        // Function to Return RetweetDetails - Starts

        public async Task<RetweetDetailsReturnDTO> RetweetDetailsFeeder(RetweetDetailsDTO retweetDetailsDTO)
        {
            try
            {
                var retweet = await _RetweetRepository.GetbyKey(retweetDetailsDTO.RetweetId);
                var actutaltweetdetails = await _TweetRepository.GetbyKey(retweet.ActualTweetId);
                var TweetFiles1 = await _TweetRequestForTweetFilesRepository.GetbyKey(actutaltweetdetails.Id);
                var Files1 = TweetFiles1.TweetFiles.ToList();
                var user = await _UserRepository.GetbyKey(actutaltweetdetails.UserId);
                RetweetDetailsReturnDTO retweetDetailsReturnDTO = new RetweetDetailsReturnDTO();
                retweetDetailsReturnDTO.TweetId = actutaltweetdetails.Id;
                retweetDetailsReturnDTO.TweetContent = actutaltweetdetails.TweetContent;
                retweetDetailsReturnDTO.TweetDateTime = actutaltweetdetails.TweetDateTime;
                retweetDetailsReturnDTO.IsCommentEnable = actutaltweetdetails.IsCommentEnable;
                retweetDetailsReturnDTO.UserId = actutaltweetdetails.UserId;
                retweetDetailsReturnDTO.TweetOwnerUserName = user.UserName;
                retweetDetailsReturnDTO.TweetOwnerUserId = user.UserId;
                retweetDetailsReturnDTO.TweetOwnerProfileImgLink = user.UserProfileImgLink;
                if (Files1.Count == 1)
                {
                    foreach (TweetFiles tweetfile in Files1)
                    {
                        retweetDetailsReturnDTO.TweetFile1 = tweetfile.File1;
                        retweetDetailsReturnDTO.TweetFile2 = "null";

                    }
                }
                else if (Files1.Count == 2)
                {
                    foreach (TweetFiles tweetfile in Files1)
                    {
                        retweetDetailsReturnDTO.TweetFile1 = tweetfile.File1;
                        retweetDetailsReturnDTO.TweetFile2 = tweetfile.File2;
                    }
                }
                else
                {
                    retweetDetailsReturnDTO.TweetFile1 = "null";
                    retweetDetailsReturnDTO.TweetFile1 = "null";
                }
                retweetDetailsReturnDTO.RetweetId = retweet.Id;
                retweetDetailsReturnDTO.RetweetContent = retweet.RetweetContent;
                retweetDetailsReturnDTO.RetweetDateTime = retweet.RetweetDateTime;
                retweetDetailsReturnDTO.IsRetweetCommentEnable = retweet.IsCommentEnable;
                var retweetuserdetails = await _UserRepository.GetbyKey(retweet.UserId);
                retweetDetailsReturnDTO.RetweetUserName = retweetuserdetails.UserName;
                retweetDetailsReturnDTO.RetweetUserProfileImgLink = retweetuserdetails.UserProfileImgLink;
                retweetDetailsReturnDTO.RetweetUserId = retweetuserdetails.UserId;
                var RetweetLikes = ((await _RetweetLikesRepository.Get()).Where(l => l.RetweetId == retweet.Id)).ToList();
                var IsUSerLikedRetweet = (await _RetweetLikesRepository.Get()).Where(l => l.RetweetId == retweet.Id && l.LikedUserId == retweetDetailsDTO.UserId).ToList();

                var retweetcomments = ((await _RetweetCommentRepository.Get()).Where(c => c.RetweetId == retweet.Id)).ToList();
                retweetDetailsReturnDTO.CommentsCount = retweetcomments.Count;

                foreach (var c in retweetcomments)
                {
                    var replies = ((await _RetweetCommentReplyRepository.Get()).Where(r => r.RetweetCommentId == c.Id)).ToList();
                    retweetDetailsReturnDTO.CommentsCount = retweetDetailsReturnDTO.CommentsCount + replies.Count;

                }

                if (IsUSerLikedRetweet.Count > 0)
                {
                    retweetDetailsReturnDTO.IsRetweetLikedByUser = "Yes";
                }
                else
                {
                    retweetDetailsReturnDTO.IsRetweetLikedByUser = "No";
                }
                retweetDetailsReturnDTO.RetweetLikesCount = RetweetLikes.Count;
                return retweetDetailsReturnDTO;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // Function to Return RetweetDetails - Ends

        // Function to Update Tweet Content - Starts

        public async Task<string> UpdateTweetContent(UpdateTweetContentDTO updateTweetContentDTO)
        {
            try
            {
                var tweet = await _TweetRepository.GetbyKey(updateTweetContentDTO.TweetId);
                if(tweet != null)
                {
                    tweet.TweetContent = updateTweetContentDTO.TweetContent;
                    var updatedTwee = await _TweetRepository.Update(tweet);
                    return "success";
                }
                throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // Function to Update Tweet Content - Ends

        // Function to Update Retweet Content - Starts

        public async Task<string> UpdateRetweetContent(UpdateRetweetContentDTO updateRetweetContentDTO)
        {
            try
            {
                var retweet = await _RetweetRepository.GetbyKey(updateRetweetContentDTO.RetweetId);
                if (retweet != null)
                {
                    retweet.RetweetContent = updateRetweetContentDTO.RetweetContent;
                    var updatedTwee = await _RetweetRepository.Update(retweet);
                    return "success";
                }
                throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // Function to Update Retweet Content - Ends
    }
}
