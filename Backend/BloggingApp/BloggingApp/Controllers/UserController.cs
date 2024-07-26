using BloggingApp.Interfaces;
using BloggingApp.Models.UserDTOs;
using BloggingApp.Models;
using BloggingApp.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
