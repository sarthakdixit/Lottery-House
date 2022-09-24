using AuctionHouse.Models;

namespace AuctionHouse.Services
{
    public interface IWinnerService
    {
        public Task<Investment> ChooseWinner(int itemid);
        public Task InsertAWinner(Investment investment);
        public Task<List<ItemDetail>> GetWinsUsingUserId(string token);
    }
}
