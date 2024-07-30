using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using BloggingApp.Models.CRLikesDTOs;
using BloggingApp.Models.NewFolder;
using BloggingApp.Models.UserDTOs;
using BloggingApp.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BloggingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class TweetController : ControllerBase
    {
        private readonly ITweetServices _TweetServices;
        private readonly ITweetLikesServices _TweetLikesServices;
        private readonly IAzureBlobService _blobService;
        public TweetController(ITweetServices tweetService, IAzureBlobService blobService, ITweetLikesServices tweetLikesServices)
        {
            _TweetServices = tweetService;
            _TweetLikesServices = tweetLikesServices;
            _blobService = blobService;
        }


        // Function to Add Tweet Image - Starts 

        [Route("AddTweetImage")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddTweet([FromForm] UserAddTweetDTO userAddTweetDTO)
        {
            try
            {
                var imageUrls = new List<string>();
                Console.WriteLine("helo");
                foreach (var image in userAddTweetDTO.Images)
                {
                    if (image.Length > 0)
                    {
                        using (var stream = image.OpenReadStream())
                        {
                            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                            var imageUrl = await _blobService.UploadAsync(stream, fileName);
                            imageUrls.Add(imageUrl);
                        }
                    }
                }
                Console.WriteLine(imageUrls[0]);
                TweetFiles tweetFiles = new TweetFiles();
                Console.WriteLine("TweetID:", userAddTweetDTO.TweetId);
                if (imageUrls.Count == 1)
                {
                    tweetFiles.File1 = imageUrls[0];
                    tweetFiles.File2 = "Null";
                }
                else if (imageUrls.Count == 2)
                {
                    tweetFiles.File1 = imageUrls[0];
                    tweetFiles.File2 = imageUrls[1];
                }
                tweetFiles.TweetId = userAddTweetDTO.TweetId;
                var tweetFileAdded = await _TweetServices.AddTweetFiles(tweetFiles);
                return Ok("tweet");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        // Function to Add Tweet Image - Ends

        // Function to Add Tweet Content - Starts

        [Route("AddTweetContent")]
        [HttpPost]
        [ProducesResponseType(typeof(AddTweetContentReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<AddTweetContentReturnDTO>> AddTweetDetails([FromBody] AddUserTweetContent addUserTweetContent)
        {
            try
            {
                var TweetId = await _TweetServices.AddTweetContentByUser(addUserTweetContent);
                return Ok(TweetId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        // Function to Add Tweet Content - Ends

        // Function to Provide Tweet Content - Starts (Home Page)

        [Route("Feeds")]
        [HttpPost]
        [ProducesResponseType(typeof(FeedsPageReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<FeedsPageReturnDTO>> Tweets([FromBody] int userid)
        {
            Console.WriteLine(userid);
            try
            {
                var Tweets = await _TweetServices.TweetsFeeder(userid);
                return Ok(Tweets);
            }
            catch (NoTweetsFoundException ntfe)
            {
                Console.WriteLine(ntfe.Message);
                return StatusCode(StatusCodes.Status409Conflict, new ErrorModel(409, ntfe.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        // Function to Provide Tweet Content - Ends (Home Page)

        // Function to Add Retweet Content - Starts 

        [Route("AddRetweetContent")]
        [HttpPost]
        [ProducesResponseType(typeof(AddTweetContentReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddRetweetDetails([FromBody] AddRetweetDTO addRetweetDTO)
        {
            try
            {
                var Tweetstring = await _TweetServices.AddRetweetContent(addRetweetDTO);
                return Ok(Tweetstring);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }
        // Function to Add Retweet Content - Ends 

        // Function to Add Tweet Likes - Starts 

        [Route("AddTweetLike")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddTweetLike([FromBody] AddTweetLikesDTO addTweetLikesDTO)
        {
            try
            {
                var TweetLike = await _TweetLikesServices.AddTweetLikes(addTweetLikesDTO);
                return Ok(TweetLike);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        // Function to Add Tweet Likes - Ends 

        // Function to Add Retweet Likes to database - Starts

        [Route("AddReTweetLike")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddRetweetLike([FromBody] AddRetweekLikeDTO addRetweekLikeDTO)
        {
            try
            {
                var TweetLike = await _TweetLikesServices.AddRetweetLikes(addRetweekLikeDTO);
                return Ok(TweetLike);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        // Function to Add Retweet Likes to database - Ends

        // Function to Add Tweet Dislikes to database - Starts

        [Route("AddTweetDisLike")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddTweetDisLike([FromBody] AddTweetDislikeDTO addTweetDislikeDTO)
        {
            try
            {
                var TweetDisLike = await _TweetLikesServices.AddTweetDisLikes(addTweetDislikeDTO);
                return Ok(TweetDisLike);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        // Function to Add Tweet Dislikes to database - Ends

        // Function to Add Retweet Dislikes to database - Starts

        [Route("AddReTweetDisLike")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddRetweetDisLike([FromBody] AddRetweetDislikeDTO addRetweetDislikeDTO)
        {
            try
            {
                var RetweetDisLike = await _TweetLikesServices.AddRetweetDisLikes(addRetweetDislikeDTO);
                return Ok(RetweetDisLike);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        // Function to Add Retweet Dislikes to database - Ends

        // Function to Add Tweet Details - Starts

        [Route("TweetDetails")]
        [HttpPost]
        [ProducesResponseType(typeof(TweetDetailsReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<TweetDetailsReturnDTO>> TweetDetailsFeeder([FromBody] TweetDetailsDTO tweetDetailsDTO)
        {
            try
            {
                 var tweetDetails = await _TweetServices.TweetDetailsFeeder(tweetDetailsDTO);
                 return Ok(tweetDetails);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        // Function  Function to Add Tweet Details - Ends

        // Function to Return Retweet Details - Starts

        [Route("RetweetDetails")]
        [HttpPost]
        [ProducesResponseType(typeof(RetweetDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<RetweetDetailsDTO>> RetweetDetailsFeeder([FromBody] RetweetDetailsDTO retweetDetailsDTO)
        {
            try
            {
                var tweetDetails = await _TweetServices.RetweetDetailsFeeder(retweetDetailsDTO);
                return Ok(tweetDetails);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        // Function  Function to Return Retweet Details - Ends

        // Function to Add Tweet Comment Likes - Starts
        [Route("AddTweetCommentLike")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddTweetCommentLike([FromBody] AddTweetCommentLikesDTO addTweetCommentLikesDTO)
        {
            try
            {
                var tweetcommentlikeDetails = await _TweetLikesServices.AddTweetCommentLikes(addTweetCommentLikesDTO);
                return Ok(tweetcommentlikeDetails);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }
        // Function to Add Tweet Comment Like - Ends

        // Function to Add Tweet Comment Dislike - Starts
        [Route("AddTweetCommentDislike")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddTweetCommentDislikeLike([FromBody] AddTweetCommentDislikeDTO addTweetCommentDislikeDTO)
        {
            try
            {
                var tweetcommentlikeDetails = await _TweetLikesServices.AddTweetCommentDislike(addTweetCommentDislikeDTO);
                return Ok(tweetcommentlikeDetails);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }
        // Function to Add Tweet Comment Dislike - Ends

        // Function to Add Tweet Comment reply Likes - Starts
        [Route("AddTweetCommentReplyLike")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddTweetCommentReplyLike([FromBody] AddTweetCommentReplyLikeDTO addTweetCommentReplyLikeDTO)
        {
            try
            {
                var tweetcommentreplylikeDetails = await _TweetLikesServices.AddTweetCommentReplyLikes(addTweetCommentReplyLikeDTO);
                return Ok(tweetcommentreplylikeDetails);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }
        // Function to Add Tweet Comment reply Like - Ends

        // Function to Add Tweet Comment Reply Dislike - Starts
        [Route("AddTweetCommentReplyDislike")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddTweetCommentReplyDislikeLike([FromBody] AddTweetCommentReplyDislikeDTO addTweetCommentReplyDislikeDTO)
        {
            try
            {
                var tweetcommentlikeDetails = await _TweetLikesServices.AddTweetCommentReplyDislike(addTweetCommentReplyDislikeDTO);
                return Ok(tweetcommentlikeDetails);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }
        // Function to Add Tweet Comment Reply Dislike - Ends

        // Retweets comments and reply likes

        // Function to Add Retweet Comment Likes - Starts
        [Route("AddRetweetCommentLike")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddRetweetCommentLike([FromBody] AddRetweetCommentLikeDTO addRetweetCommentLikeDTO)
        {
            try
            {
                var RetweetcommentlikeDetails = await _TweetLikesServices.AddRetweetCommentLikes(addRetweetCommentLikeDTO);
                return Ok(RetweetcommentlikeDetails);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }
        // Function to Add Retweet Comment Like - Ends

        // Function to Add Retweet Comment Dislike - Starts
        [Route("AddRetweetCommentDislike")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddRetweetCommentDislikeLike([FromBody] AddRetweetCommentDislikeDTO addRetweetCommentDislikeDTO)
        {
            try
            {
                var RetweetcommentlikeDetails = await _TweetLikesServices.AddRetweetCommentDislike(addRetweetCommentDislikeDTO);
                return Ok(RetweetcommentlikeDetails);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }
        // Function to Add Retweet Comment Dislike - Ends

        // Function to Add Retweet Reply like - Starts
        [Route("AddRetweetCommentReplyLike")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddRetweetCommentReplyLike([FromBody] AddRetweetCommentReplyLikeDTO addRetweetCommentReplyLikeDTO)
        {
            try
            {
                var RetweetcommentreplylikeDetails = await _TweetLikesServices.AddRetweetCommentReplyLikes(addRetweetCommentReplyLikeDTO);
                return Ok(RetweetcommentreplylikeDetails);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }
        // Function to Add Retweet Reply like - Ends


        // Function to Add Retweet Reply Dislike - Starts
        [Route("AddRetweetCommentReplyDisLike")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddRetweetCommentReplyDisLike([FromBody] AddRetweetCommentReplyDislikeDTO addRetweetCommentReplyDislikeDTO)
        {
            try
            {
                var RetweetcommentreplylikeDetails = await _TweetLikesServices.AddRetweetCommentReplyDislike(addRetweetCommentReplyDislikeDTO);
                return Ok(RetweetcommentreplylikeDetails);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }
        // Function to Add Retweet Reply Dislike - Ends


    }
}
