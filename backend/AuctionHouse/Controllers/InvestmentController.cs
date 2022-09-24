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
    public class InvestmentController : ControllerBase
    {
        private readonly IInvestmentService _investmentService;

        public InvestmentController(IInvestmentService investmentService)
        {
            _investmentService = investmentService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> InsertAnInvestment(Investment investment)
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            await _investmentService.InsertAnInvestment(investment, token);
            return Ok();
        }
    }
}
