namespace AuctionHouse.Models
{
    public class AuthenticateResponse
    {
        public TokenApi Tokens { get; set; }
        public User User { get; set; }
        public UserDetail UserDetails { get; set; }
    }
}
