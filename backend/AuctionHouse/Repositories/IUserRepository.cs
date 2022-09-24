using AuctionHouse.Models;

namespace AuctionHouse.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUserWithNameAndEmail(User user);
        public Task<User> GetUserWithId(int id);
        public Task<RegisterationResponse> Register(User user);
        public Task<UserDetail> GetUserDetailUsingUserId(User user);
    }
}
