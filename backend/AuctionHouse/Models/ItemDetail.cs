namespace AuctionHouse.Models
{
    public class ItemDetail
    {
        public Item Item { get; set; }
        public int PaymentCount { get; set; }
        public bool UserPaid { get; set; }
    }
}
