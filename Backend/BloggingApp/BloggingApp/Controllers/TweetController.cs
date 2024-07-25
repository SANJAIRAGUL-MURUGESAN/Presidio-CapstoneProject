using BloggingApp.Interfaces;
using BloggingApp.Models;
using BloggingApp.Models.UserDTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                return Ok("tweet");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

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
    }
}
