using AuctionHouse.Models;
using AuctionHouse.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace AuctionHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<RegisterationResponse>> Regsiter([FromBody] User user)
        {
            RegisterationResponse response = await _userService.Register(user);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<TokenApi>> Login([FromBody] User user)
        {
            TokenApi res = await _userService.Login(user);
            return Ok(res);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult<User>> GetUserWithId()
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            User user = await _userService.GetUserWithId(token);
            return Ok(user);
        }
    }
}
