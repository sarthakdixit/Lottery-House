namespace AuctionHouse.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RecoveryToken { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
