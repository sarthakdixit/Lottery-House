using AuctionHouse.Models;
using System.Data.SqlClient;
using System.Data;

namespace AuctionHouse.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IDatabaseConnection _connString;

        public ItemRepository(IDatabaseConnection databaseConnection)
        {
            _connString = databaseConnection;
        }

        public async Task<List<Item>> GetActiveItems()
        {
            List<Item> items = new List<Item>();
            using (SqlConnection conn = new SqlConnection(_connString.GetConnectionString()))
            {
                conn.Open();
                string query = "[dbo].[GetActiveItems]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@currentdate", DateTime.UtcNow);
                using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                {
                    while (sdr.Read())
                    {
                        items.Add(new Item
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Name = Convert.ToString(sdr["Name"]),
                            Description = Convert.ToString(sdr["Description"]),
                            Amount = Convert.ToDouble(sdr["Amount"]),
                            ExpiredAt = Convert.ToDateTime(sdr["ExpiredAt"]),
                            CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                            UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"]),
                        });
                    }
                }
                return items;
            }
        }

        public async Task<Item> GetAnItem(int id)
        {
            Item item = null;
            using (SqlConnection conn = new SqlConnection(_connString.GetConnectionString()))
            {
                conn.Open();
                string query = "[dbo].[GetAnItemUsingId]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                {
                    while (sdr.Read())
                    {
                        item = new Item
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Name = Convert.ToString(sdr["Name"]),
                            Description = Convert.ToString(sdr["Description"]),
                            Amount = Convert.ToDouble(sdr["Amount"]),
                            ExpiredAt = Convert.ToDateTime(sdr["ExpiredAt"]),
                            CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                            UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"]),
                        };
                    }
                }
                return item;
            }
        }

        public async Task<List<Item>> GetInactiveItems()
        {
            List<Item> items = new List<Item>();
            using (SqlConnection conn = new SqlConnection(_connString.GetConnectionString()))
            {
                conn.Open();
                string query = "[dbo].[GetInactiveItems]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@currentdate", DateTime.UtcNow);
                using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                {
                    while (sdr.Read())
                    {
                        items.Add(new Item
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Name = Convert.ToString(sdr["Name"]),
                            Description = Convert.ToString(sdr["Description"]),
                            Amount = Convert.ToDouble(sdr["Amount"]),
                            ExpiredAt = Convert.ToDateTime(sdr["ExpiredAt"]),
                            CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                            UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"]),
                        });
                    }
                }
                return items;
            }
        }

        public async Task<List<Item>> GetItemsToChooseWinner()
        {
            List<Item> items = new List<Item>();
            using (SqlConnection conn = new SqlConnection(_connString.GetConnectionString()))
            {
                conn.Open();
                string query = "[dbo].[GetItemsToChooseWinner]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@currentdate", DateTime.UtcNow);
                using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                {
                    while (sdr.Read())
                    {
                        items.Add(new Item
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Name = Convert.ToString(sdr["Name"]),
                            Description = Convert.ToString(sdr["Description"]),
                            Amount = Convert.ToDouble(sdr["Amount"]),
                            ExpiredAt = Convert.ToDateTime(sdr["ExpiredAt"]),
                            CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                            UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"]),
                        });
                    }
                }
                return items;
            }
        }

        public async Task InsertAnItem(Item item)
        {
            using (SqlConnection conn = new SqlConnection(_connString.GetConnectionString()))
            {
                conn.Open();
                string query = "[dbo].[InsertAnItem]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.Parameters.AddWithValue("@description", item.Description);
                cmd.Parameters.AddWithValue("@amount", item.Amount);
                cmd.Parameters.AddWithValue("@expiredat", item.ExpiredAt);
                cmd.Parameters.AddWithValue("@createdat", item.CreatedAt);
                cmd.Parameters.AddWithValue("@updatedat", item.UpdatedAt);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
