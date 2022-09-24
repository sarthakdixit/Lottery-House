using AuctionHouse.Models;
using AuctionHouse.Repositories;
using System.Security.Claims;

namespace AuctionHouse.Services
{
    public class InvestmentService : IInvestmentService
    {
        private readonly ITokenService _tokenService;
        private readonly IInvestmentRespository _investmentRepository;

        public InvestmentService(ITokenService tokenService, IInvestmentRespository investmentRepository)
        {
            _tokenService = tokenService;
            _investmentRepository = investmentRepository;
        }

        public async Task InsertAnInvestment(Investment investment, string token)
        {
            ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(token);
            int userid = Convert.ToInt32(principal.FindFirstValue("Id"));

            investment.UserId = userid;
            investment.CreatedAt = DateTime.UtcNow;

            await _investmentRepository.InsertAnInvestment(investment);
        }
    }
}
