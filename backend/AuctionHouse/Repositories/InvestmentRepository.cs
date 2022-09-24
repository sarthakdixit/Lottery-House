using AuctionHouse.Models;
using System.Data.SqlClient;
using System.Data;

namespace AuctionHouse.Repositories
{
    public class InvestmentRepository : IInvestmentRespository
    {
        private readonly IDatabaseConnection _connString;

        public InvestmentRepository(IDatabaseConnection databaseConnection)
        {
            _connString = databaseConnection;
        }

        public async Task<List<Investment>> GetInvestmentsOfUniqueItemId(int itemid)
        {
            List<Investment> list = new List<Investment>();
            using (SqlConnection conn = new SqlConnection(_connString.GetConnectionString()))
            {
                conn.Open();
                string query = "[dbo].[GetInvestmentsOfUniqueItemId]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@itemid", itemid);
                SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                while (sdr.Read())
                {
                    list.Add(new Investment()
                    {
                        Id = Convert.ToInt32(sdr["Id"]),
                        UserId = Convert.ToInt32(sdr["UserId"]),
                        ItemId = Convert.ToInt32(sdr["ItemId"]),
                        CryptId = Convert.ToString(sdr["CryptId"]),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"])
                    });
                }
                return list;
            }
        }

        public async Task<Investment> GetInvestmentUsingUserIdAndItemId(int userid, int itemid)
        {
            Investment investment = null;
            using (SqlConnection conn = new SqlConnection(_connString.GetConnectionString()))
            {
                conn.Open();
                string query = "[dbo].[GetInvestmentUsingUserIdAndItemId]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.Parameters.AddWithValue("@itemid", itemid);
                SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                while (sdr.Read())
                {
                    investment = new Investment()
                    {
                        Id = Convert.ToInt32(sdr["Id"]),
                        UserId = Convert.ToInt32(sdr["UserId"]),
                        ItemId = Convert.ToInt32(sdr["ItemId"]),
                        CryptId = Convert.ToString(sdr["CryptId"]),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"])
                    };
                }
                return investment;
            }
        }

        public async Task InsertAnInvestment(Investment investment)
        {
            using (SqlConnection conn = new SqlConnection(_connString.GetConnectionString()))
            {
                conn.Open();
                string query = "[dbo].[InsertAnInvestment]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid", investment.UserId);
                cmd.Parameters.AddWithValue("@itemid", investment.ItemId);
                cmd.Parameters.AddWithValue("@cryptid", investment.CryptId);
                cmd.Parameters.AddWithValue("@createdat", investment.CreatedAt);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
