using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Interfaces;
using BloggingApp.Models;
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

    }
}
