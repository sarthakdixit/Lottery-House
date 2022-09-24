using AuctionHouse.Models;
using System.Data.SqlClient;
using System.Data;

namespace AuctionHouse.Repositories
{
    public class WinnerRepository : IWinnerRepository
    {
        private readonly IDatabaseConnection _connString;

        public WinnerRepository(IDatabaseConnection databaseConnection)
        {
            _connString = databaseConnection;
        }

        public async Task<Investment> GetWinnerUsingItemId(int itemid)
        {
            Investment investment = null;
            using (SqlConnection conn = new SqlConnection(_connString.GetConnectionString()))
            {
                conn.Open();
                string query = "[dbo].[GetWinnerUsingItemId]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@itemid", itemid);
                SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                while (sdr.Read())
                {
                    investment = new Investment()
                    {
                        Id = Convert.ToInt32(sdr["Id"]),
                        UserId = Convert.ToInt32(sdr["UserId"]),
                        ItemId = Convert.ToInt32(sdr["ItemId"]),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"])
                    };
                }
                return investment;
            }
        }

        public async Task<List<Investment>> GetWinsUsingUserId(int userid)
        {
            List<Investment> list = new List<Investment>();
            using (SqlConnection conn = new SqlConnection(_connString.GetConnectionString()))
            {
                conn.Open();
                string query = "[dbo].[GetWinnerUsingUserId]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid", userid);
                SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                while (sdr.Read())
                {
                    list.Add(new Investment()
                    {
                        Id = Convert.ToInt32(sdr["Id"]),
                        UserId = Convert.ToInt32(sdr["UserId"]),
                        ItemId = Convert.ToInt32(sdr["ItemId"]),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"])
                    });
                }
                return list;
            }
        }

        public async Task InsertAWinner(Investment investment)
        {
            using (SqlConnection conn = new SqlConnection(_connString.GetConnectionString()))
            {
                conn.Open();
                string query = "[dbo].[InsertAWinner]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid", investment.UserId);
                cmd.Parameters.AddWithValue("@itemid", investment.ItemId);
                cmd.Parameters.AddWithValue("@createdat", investment.CreatedAt);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
