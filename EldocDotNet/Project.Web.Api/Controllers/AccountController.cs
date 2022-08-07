using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.User;
using Project.Application.Features.Interfaces;
using Project.Application.Responses;
using Project.Web.Api.Extensions;
using System.Net;

namespace Project.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.BadRequest)]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [Produces(typeof(Response<UserDTO>))]
        public async Task<JsonResult> Login([FromBody] LoginUser input)
        {
            var res = await _userService.Login(input);

            return new Response<UserDTO>(res).ToJsonResult();
        }

        [HttpPost("signup")]
        [Produces(typeof(Response<string>))]
        public async Task<JsonResult> Signup([FromBody] SignupUser input)
        {
            await _userService.Signup(input.Phone);

            return Response<string>.Succeed();
        }

        [HttpPost("verify")]
        [Produces(typeof(Response<string>))]
        public async Task<JsonResult> Verify([FromBody] VerifyUser input)
        {
            var res = await _userService.Verify(input);

            return new Response<string>(res).ToJsonResult();
        }

        [UserAuthorize]
        [HttpPost("set-password")]
        [Produces(typeof(Response<string>))]
        public async Task<JsonResult> SetPassword([FromBody] SetPasswordUser input)
        {
            await _userService.SetPassword(input.NewPassword);

            return Response<string>.Succeed();
        }

        [UserAuthorize]
        [HttpGet("profile")]
        [Produces(typeof(Response<UserDTO>))]
        public async Task<JsonResult> GetProfile()
        {
            var res = await _userService.GetProfile();

            return new Response<UserDTO>(res).ToJsonResult();
        }

        [UserAuthorize]
        [HttpPost("profile")]
        [Produces(typeof(Response<UserDTO>))]
        public async Task<JsonResult> EditProfile([FromBody] EditUserProfile input)
        {
            var res = await _userService.EditProfile(input);

            return new Response<UserDTO>(res).ToJsonResult();
        }
    }
}
