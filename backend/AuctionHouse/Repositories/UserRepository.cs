using AuctionHouse.Models;
using System.Data;
using System.Data.SqlClient;

namespace AuctionHouse.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseConnection _connString;

        public UserRepository(IDatabaseConnection databaseConnection)
        {
            _connString = databaseConnection;
        }

        public async Task<UserDetail> GetUserDetailUsingUserId(User user)
        {
            UserDetail userDetail = null;
            using (SqlConnection conn = new SqlConnection(_connString.GetConnectionString()))
            {
                conn.Open();
                string query = "[dbo].[GetUserDetailUsingUserId]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid", user.Id);
                SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                while (sdr.Read())
                {
                    userDetail = new UserDetail()
                    {
                        PhoneNumber = Convert.ToString(sdr["PhoneNumber"]),
                        Address = Convert.ToString(sdr["Address"]),
                        Pincode = Convert.ToString(sdr["Pincode"]),
                        State = Convert.ToString(sdr["State"])
                    };
                }
                return userDetail;
            }
        }

        public async Task<User> GetUserWithId(int id)
        {
            User u = null;
            using (SqlConnection conn = new SqlConnection(_connString.GetConnectionString()))
            {
                conn.Open();
                string query = "[dbo].[GetUserWithId]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                while (sdr.Read())
                {
                    u = new User()
                    {
                        Id = Convert.ToInt32(sdr["Id"]),
                        Username = Convert.ToString(sdr["Username"]),
                        Email = Convert.ToString(sdr["Email"]),
                        Password = Convert.ToString(sdr["Password"]),
                        IsAdmin = Convert.ToBoolean(sdr["IsAdmin"])
                    };
                }
                return u;
            }
        }

        public async Task<User> GetUserWithNameAndEmail(User user)
        {
            User u = null;
            using (SqlConnection conn = new SqlConnection(_connString.GetConnectionString()))
            {
                conn.Open();
                string query = "[dbo].[GetUserWithNameAndEmail]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@email", user.Email);
                SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                while (sdr.Read())
                {
                    u = new User()
                    {
                        Id = Convert.ToInt32(sdr["Id"]),
                        Username = Convert.ToString(sdr["Username"]),
                        Email = Convert.ToString(sdr["Email"]),
                        Password = Convert.ToString(sdr["Password"]),
                    };
                }
            }
            return u;
        }


        public async Task<RegisterationResponse> Register(User user)
        {
            RegisterationResponse response = null;
            using (SqlConnection conn = new SqlConnection(_connString.GetConnectionString()))
            {
                conn.Open();
                string query = "[dbo].[RegisterUser]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@emailVerified", 0);
                cmd.Parameters.AddWithValue("@isAdmin", 0);
                cmd.Parameters.AddWithValue("@createdAt", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@updatedAt", DateTime.UtcNow);
                int i = await cmd.ExecuteNonQueryAsync();
                if (i > 0)
                    response = new RegisterationResponse()
                    {
                        Status = true,
                        Message = "Registeration successfull"
                    };
                return response;
            }
        }
    }
}
