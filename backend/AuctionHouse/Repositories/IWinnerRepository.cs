using AuctionHouse.Models;

namespace AuctionHouse.Repositories
{
    public interface IWinnerRepository
    {
        public Task InsertAWinner(Investment investment);
        public Task<Investment> GetWinnerUsingItemId(int itemid);
        public Task<List<Investment>> GetWinsUsingUserId(int userid);
    }
}
