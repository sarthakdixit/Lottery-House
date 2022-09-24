using AuctionHouse.Models;

namespace AuctionHouse.Services
{
    public interface IInvestmentService
    {
        public Task InsertAnInvestment(Investment investment, string token);
    }
}
