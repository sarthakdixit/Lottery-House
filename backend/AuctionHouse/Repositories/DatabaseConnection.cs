namespace AuctionHouse.Repositories
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private string _connectionString = "Data Source=DESKTOP-4IQU6MU;Initial Catalog=AuctionHouse;Integrated Security=True";

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
