using AuctionHouse.Models;

namespace AuctionHouse.Repositories
{
    public interface IInvestmentRespository
    {
        public Task<Investment> GetInvestmentUsingUserIdAndItemId(int userid, int itemid);
        public Task<List<Investment>> GetInvestmentsOfUniqueItemId(int itemid);
        public Task InsertAnInvestment(Investment investment);
    }
}
