using AuctionHouse.Models;
using AuctionHouse.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace AuctionHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<TokenApi>> ValidateToken(TokenApi tokenApi)
        {
            TokenApi api = await _tokenService.ValidateToken(tokenApi);
            return Ok(api);
        }

        [HttpPost("[action]")]
        [Authorize]
        public ActionResult RevokeToken()
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            _tokenService.RevokeToken(token);
            return Ok();
        }
    }
}
