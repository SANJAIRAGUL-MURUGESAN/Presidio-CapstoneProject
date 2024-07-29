using BloggingApp.Interfaces;
using BloggingApp.Models.UserDTOs;
using BloggingApp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BloggingApp.Models.CommentDTOs;
using BloggingApp.Models.ReplyDTOs;

namespace BloggingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentServices _CommentServices;
        private readonly ITweetServices _TweetServices;
        private readonly ITweetLikesServices _TweetLikesServices;
        public CommentsController(ICommentServices commentService ,ITweetServices tweetService, IAzureBlobService blobService, ITweetLikesServices tweetLikesServices)
        {
            _TweetServices = tweetService;
            _TweetLikesServices = tweetLikesServices;
            _CommentServices = commentService;
        }

        [Route("AddTweetComment")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddComment([FromBody] AddCommentDTO addCommentDTO)
        {
            try
            {
                var comment = await _CommentServices.AddComment(addCommentDTO);
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Route("CommentDetails")]
        [HttpPost]
        [ProducesResponseType(typeof(List<CommentDetailsReturnDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<List<CommentDetailsReturnDTO>>> ReturnComments([FromBody] TweetCommentDetails tweetCommentDetails)
        {
            try
            {
                var comments = await _CommentServices.ReturnComments(tweetCommentDetails);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }


        [Route("AddCommentReply")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddCommentReply([FromBody] AddReplyDTO addReplyDTO)
        {
            try
            {
                var commentreply = await _CommentServices.AddCommentReply(addReplyDTO);
                return Ok(commentreply);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }


        [Route("AddReplytoReply")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddReplytoReply([FromBody] AddReplytoRelpyDTO addReplyDTO)
        {
            try
            {
                var commentreply = await _CommentServices.AddReplyTOReply(addReplyDTO);
                return Ok(commentreply);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Route("AddRetweetComment")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddRetweetComment([FromBody] AddRetweetCommentDTO addRetweetCommentDTO)
        {
            try
            {
                var retweetcomment = await _CommentServices.AddCommenttoRetweet(addRetweetCommentDTO);
                return Ok(retweetcomment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Route("AddRetweetCommentReply")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddRetweetCommentReply([FromBody] AddRetweetCommentReplyDTO addRetweetCommentReplyDTO)
        {
            try
            {
                var retweetcommentreply = await _CommentServices.AddRetweetCommentReply(addRetweetCommentReplyDTO);
                return Ok(retweetcommentreply);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Route("AddRetweetCommentReplytoReply")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddRetweetCommentReplytoReply([FromBody] AddRetweetCommentReplytoRelpy addRetweetCommentReplytoRelpy)
        {
            try
            {
                var retweetcommentreplytoreply = await _CommentServices.AddRetweetCommentReplyTOReply(addRetweetCommentReplytoRelpy);
                return Ok(retweetcommentreplytoreply);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Route("RetweetCommentDetails")]
        [HttpPost]
        [ProducesResponseType(typeof(List<RetweetCommentDetailsReturnDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<List<RetweetCommentDetailsReturnDTO>>> ReturnRetweetComments([FromBody] RetweetCommentDetails retweetCommentDetails)
        {
            try
            {
                var comments = await _CommentServices.ReturnRetweetComments(retweetCommentDetails);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

    }
}
