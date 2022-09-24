using AuctionHouse.Models;
using AuctionHouse.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace AuctionHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WinnerController : ControllerBase
    {
        private readonly IWinnerService _winnerService;

        public WinnerController(IWinnerService winnerService)
        {
            _winnerService = winnerService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Investment>> ChooseWinner(int itemid)
        {
            Investment investment = await _winnerService.ChooseWinner(itemid);
            return Ok(investment);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> InsertAWinner([FromBody] Investment investment)
        {
            await _winnerService.InsertAWinner(investment);
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<ItemDetail>>> GetWinsUsingUserId()
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            List<ItemDetail> list = await _winnerService.GetWinsUsingUserId(token);
            return Ok(list);
        }
    }
}
