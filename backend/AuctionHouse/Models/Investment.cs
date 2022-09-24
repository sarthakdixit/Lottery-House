namespace AuctionHouse.Models
{
    public class Investment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public string CryptId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
