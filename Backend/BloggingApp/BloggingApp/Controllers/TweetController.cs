using BloggingApp.Exceptions.TweetExceptions;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using BloggingApp.Models.UserDTOs;
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
        private readonly IAzureBlobService _blobService;
        public TweetController(ITweetServices tweetService, IAzureBlobService blobService)
        {
            _TweetServices = tweetService;
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
                else if(imageUrls.Count == 2)
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
        public async Task<ActionResult<FeedsPageReturnDTO>> Tweets(int userid)
        {
            try
            {
                var Tweets = await _TweetServices.TweetsFeeder(userid);
                return Ok(Tweets);
            }
            catch (NoTweetsFoundException ntfe)
            {
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
    }
}
