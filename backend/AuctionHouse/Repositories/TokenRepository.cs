using AuctionHouse.Models;
using System.Data.SqlClient;
using System.Data;

namespace AuctionHouse.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IDatabaseConnection _connString;

        public TokenRepository(IDatabaseConnection databaseConnection)
        {
            _connString = databaseConnection;
        }

        public async Task<RefreshToken> GetTokenUsingUserIdAndRecoveryToken(int userId, string recoveryToken)
        {
            RefreshToken refreshToken = null;
            using (SqlConnection conn = new SqlConnection(_connString.GetConnectionString()))
            {
                conn.Open();
                string query = "[dbo].[GetTokenUsingUserIdAndRecoveryToken]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid", userId);
                cmd.Parameters.AddWithValue("@recoverytoken", recoveryToken);
                SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                while (sdr.Read())
                {
                    refreshToken = new RefreshToken()
                    {
                        RecoveryToken = Convert.ToString(sdr["RecoveryToken"]),
                        ExpiredAt = Convert.ToDateTime(Convert.ToString(sdr["ExpiredAt"]))
                    };
                }
            }
            return refreshToken;
        }

        public async Task InsertRecoveryToken(RefreshToken refreshToken)
        {
            using (SqlConnection conn = new SqlConnection(_connString.GetConnectionString()))
            {
                conn.Open();
                string query = "[dbo].[InsertRecoveryToken]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid", refreshToken.UserId);
                cmd.Parameters.AddWithValue("@recoverytoken", refreshToken.RecoveryToken);
                cmd.Parameters.AddWithValue("@expiredat", refreshToken.ExpiredAt);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task RevokeToken(int userId)
        {
            using (SqlConnection conn = new SqlConnection(_connString.GetConnectionString()))
            {
                conn.Open();
                string query = "[dbo].[DeleteTokenUsingUserId]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid", userId);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
