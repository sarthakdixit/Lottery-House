
namespace AuctionHouse.Services
{
    public class HashService : IHashService
    {
        public string EncodeToBase64(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            return passwordHash;
        }

        public bool VerifyEncodedString(string encodedString, string password)
        {
            bool verified = BCrypt.Net.BCrypt.Verify(password, encodedString);
            return verified;
        }
    }
}
