namespace AuctionHouse.Services
{
    public interface IHashService
    {
        public string EncodeToBase64(string password);
        public bool VerifyEncodedString(string encodedString, string password);
    }
}
