using BloggingApp.Interfaces;
using BloggingApp.Models.UserDTOs;
using BloggingApp.Models;
using BloggingApp.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BloggingApp.Models.FollowDTOs;

namespace BloggingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _UserServices;

        public UserController(IUserServices userServices)
        {
            _UserServices = userServices;
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
    }
}
