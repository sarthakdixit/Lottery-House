using AuctionHouse.Models;

namespace AuctionHouse.Repositories
{
    public interface ITokenRepository
    {
        public Task InsertRecoveryToken(RefreshToken refreshToken);
        public Task<RefreshToken> GetTokenUsingUserIdAndRecoveryToken(int userId, string recoveryToken);
        public Task RevokeToken(int userId);
    }
}
