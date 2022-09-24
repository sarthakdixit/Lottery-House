using AuctionHouse.Models;
using System.Security.Claims;

namespace AuctionHouse.Services
{
    public interface ITokenService
    {
        public string GenerateAccessToken(User user);
        public string GenerateRefreshToken();
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        public Task<TokenApi> ValidateToken(TokenApi tokenApi);
        public void RevokeToken(string token);
    }
}
