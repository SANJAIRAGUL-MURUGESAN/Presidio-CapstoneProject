using BloggingApp.Interfaces;
using BloggingApp.Models.UserDTOs;
using BloggingApp.Models;
using BloggingApp.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BloggingApp.Models.FollowDTOs;
using BloggingApp.Exceptions.UserNotifications;

namespace BloggingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _UserServices;
        private readonly IAzureBlobService _blobService;

        public UserController(IUserServices userServices, IAzureBlobService blobService)
        {
            _UserServices = userServices;
            _blobService = blobService;
        }

        [Route("AddTweetImage")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<User>> AddProfileImage([FromForm] AddProfileImageDTO userAddTweetDTO)
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
                Console.WriteLine(userAddTweetDTO.UserId);
                UpdateUserProfileImageDTO userimage = new UpdateUserProfileImageDTO();
                userimage.ProfileImageUrl = imageUrls[0];
                userimage.UserId = userAddTweetDTO.UserId;
                var Addeduserimage = await _UserServices.UserProfileImageUpdate(userimage);
                return Addeduserimage;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Route("RegisterUser")]
        [HttpPost]
        [ProducesResponseType(typeof(RegisterUserReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<int>> AddUser([FromBody] RegisterUserDTO registerUserDTO)
        {
            try
            {
                var user = await _UserServices.RegisterUser(registerUserDTO);
                return Ok(user);
            }
            catch (UserEmailAlreadyExistsException utre)
            {
                return StatusCode(StatusCodes.Status409Conflict, new ErrorModel(409, utre.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Route("UserLogin")]
        [HttpPost]
        [ProducesResponseType(typeof(UserLoginReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<UserLoginReturnDTO>> UserLogin([FromBody] UserLoginDTO userLoginDTO)
        {
            try
            {
                var user = await _UserServices.UserLogin(userLoginDTO);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }
       
        // Function to show trending users - Starts

        [Route("TopUsers")]
        [HttpPost]
        [ProducesResponseType(typeof(List<TopUsersReturnDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<List<TopUsersReturnDTO>>> ReturnTop5Users([FromBody] int userid)
        {
            try
            {
                var user = await _UserServices.ShowFollowers(userid);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        // Function to show trending users - Ends

        // Function to Add Follow Request - Starts


        [Route("AddFollow")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> AddFollow([FromBody] AddFollowerDTO addFollowerDTO)
        {
            try
            {
                var user = await _UserServices.AddFollower(addFollowerDTO);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        // Function to Add Follow Request - Starts

        // Function to Add Remove Request - Starts

        [Route("RemoveFollow")]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> RemoveFollow([FromBody] RemoveFollowerDTO removeFollowerDTO)
        {
            try
            {
                var user = await _UserServices.RemoveFollower(removeFollowerDTO);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        // Function to Add Remove Request - Ends

        [Route("UserSideBarInfo")]
        [HttpPost]
        [ProducesResponseType(typeof(SideBarUserInfoReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<SideBarUserInfoReturnDTO>> UserSideBarInfoReturn([FromBody] int userid)
        {
            try
            {
                var user = await _UserServices.ReturnSideBarUserInfo(userid);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        // Function to Add Remove Request - Ends

        // Function to Add Send Notifications - Starts

        [Route("UserNotifications")]
        [HttpPost]
        [ProducesResponseType(typeof(List<NotificationUserDetailsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<List<NotificationUserDetailsDTO>>> NotificationSender([FromBody] NotificationUserDetailsDTO notificationUserDetailsDTO)
        {
            try
            {
                var notification = await _UserServices.NotificationSender(notificationUserDetailsDTO);
                return Ok(notification);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        // Function to Add Send Notifications - Starts

        // Function to Add Update Notifications - Starts

        [Route("UpdateUserNotifications")]
        [HttpPost]
        [ProducesResponseType(typeof(List<NotificationUserDetailsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> UpdateNotification([FromBody] int UserId)
        {
            try
            {
                var notification = await _UserServices.UpdateNotification(UserId);
                return Ok(notification);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }
        // Function to Add Update Notifications - Ends

        // Function to Add Provide User Profile Details - Starts

        [Route("UserProfileDetails")]
        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<User>> UserProfile([FromBody] int UserId)
        {
            try
            {
                var notification = await _UserServices.UserProfile(UserId);
                return Ok(notification);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        // Function to Add Provide User Profile Details - Ends


        [Route("UserSerachProfiles")]
        [HttpPost]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<List<User>>> UserSerachProfile([FromBody] string username)
        {
            try
            {
                var users = await _UserServices.UserProfileSearch(username);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

    }
}
