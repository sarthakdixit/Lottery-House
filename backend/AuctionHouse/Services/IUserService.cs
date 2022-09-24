using AuctionHouse.Models;

namespace AuctionHouse.Services
{
    public interface IUserService
    {
        public Task<TokenApi> Login(User user);
        public Task<RegisterationResponse> Register(User user);
        public Task<User> GetUserWithId(string token);
    }
}
